using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float MovementSpeed = 0.0f;
    [SerializeField] private float JumpPower = 0.0f;
    [SerializeField] private LayerMask GroundLayer;

    private Rigidbody2D Rigidbody2DReference;
    private Animator AnimatorReference;
    private BoxCollider2D BoxColliderReference;

    private float HorizontalInput = 0.0f;
    private float DoubleJumpCounter = 0.0f;
    private bool CanDoubleJump = false;
    private int JumpCount = 0;


    // Setters
    public void SetDoubleJump(bool CanDoubleJump)
    {
        this.CanDoubleJump = CanDoubleJump;
    }

    public void SetDoubleJumpCounter(float DoubleJumpCounter)
    {
        this.DoubleJumpCounter = DoubleJumpCounter;
    }

    // Getters
    

    void Start()
    {
        // Obtain component references
        Rigidbody2DReference = GetComponent<Rigidbody2D>();
        AnimatorReference = GetComponent<Animator>();  
        BoxColliderReference = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        HandleJump();
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        HorizontalInput = Input.GetAxis("Horizontal");

        Rigidbody2DReference.velocity = new Vector2(HorizontalInput * MovementSpeed, Rigidbody2DReference.velocity.y);
         
        // Flip player based on input direction
        if(HorizontalInput > 0.0f)
        {
            transform.localScale = new Vector2(-1.0f, 1.0f);
        }
        else if(HorizontalInput < 0.0f)
        {
            transform.localScale = new Vector2(1.0f, 1.0f);
        }
        
        // Animation variables
        AnimatorReference.SetBool("IsRunning",HorizontalInput != 0);
        AnimatorReference.SetBool("IsGrounded",IsGrounded());
    }

    private void HandleJump()
    {
        // Disable double jump / reset double jump
        if(CanDoubleJump)
        {
            DoubleJumpCounter -= Time.deltaTime;
            if(DoubleJumpCounter < 0.0f)
            {
                CanDoubleJump = false;
            }

            if(JumpCount == 2 && IsGrounded())
            {  
                JumpCount = 0;
            }
        }
       
        // Double jump logic
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(CanDoubleJump && JumpCount < 2) 
            {
                Rigidbody2DReference.velocity = new Vector2(Rigidbody2DReference.velocity.x, JumpPower);
                JumpCount++;
            }
            else if(IsGrounded())
            {
                Rigidbody2DReference.velocity = new Vector2(Rigidbody2DReference.velocity.x, JumpPower);
            }

            AnimatorReference.SetTrigger("JumpTrigger");
        }
        
    }

    private bool IsGrounded()
    {
        // Determine if player is on ground
        RaycastHit2D RaycastHit = Physics2D.BoxCast(BoxColliderReference.bounds.center, BoxColliderReference.bounds.size, 0, Vector2.down, 0.1f, GroundLayer);
        return RaycastHit.collider != null;
    }

    
}
