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


    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2DReference = GetComponent<Rigidbody2D>();
        AnimatorReference = GetComponent<Animator>();  
        BoxColliderReference = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
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
        
        AnimatorReference.SetBool("IsRunning",HorizontalInput!=0);
      

        AnimatorReference.SetBool("IsGrounded",IsGrounded());
    }

    private void HandleJump()
    {
        if(Input.GetKey(KeyCode.Space) && IsGrounded())
        {
            Rigidbody2DReference.velocity = new Vector2(Rigidbody2DReference.velocity.x, JumpPower);
            AnimatorReference.SetTrigger("JumpTrigger");
        }
    }

    private bool IsGrounded()
    {
        RaycastHit2D RaycastHit = Physics2D.BoxCast(BoxColliderReference.bounds.center, BoxColliderReference.bounds.size, 0, Vector2.down, 0.1f, GroundLayer);
        return RaycastHit.collider != null;
    }

    
}
