using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float KnockBackTime = 1.0f;
    [SerializeField] private float KnockBackPower = 0.0f;
    [SerializeField] private float MovementSpeed = 0.0f;
    [SerializeField] private float JumpPower = 0.0f;
    [SerializeField] private float WallJumpCooldownTime = 1.0f;
    [SerializeField] private LayerMask GroundLayer;
    [SerializeField] private LayerMask WallLayer;
    [SerializeField] private GameObject PauseMenu;
    [SerializeField] private MainMenu Menu;
    [SerializeField] private AudioClip JumpSound;
    [SerializeField] private AudioClip KnockbackSound;

    private AudioSource PlayerAudio;
    private Rigidbody2D Rigidbody2DReference;
    private Animator AnimatorReference;
    private BoxCollider2D BoxColliderReference;

    private float WallJumpCooldown = 0.0f;
    private float HorizontalInput = 0.0f;
    private float VerticalInput = 0.0f;
    private float DoubleJumpCounter = 0.0f;
    private int JumpCount = 0;
    private bool CanDoubleJump = false;
    private bool WallState = false;
    private bool DisableInput = false;
    private bool OnLadder = false;


    // Setters

    public void SetInput()
    {
        DisableInput = true;
        Rigidbody2DReference.velocity = new Vector2(0.0f,0.0f);
        AnimatorReference.SetBool("IsRunning", false);
    }

    public void SetDoubleJump(bool CanDoubleJump)
    {
        this.CanDoubleJump = CanDoubleJump;
    }

    public void SetDoubleJumpCounter(float DoubleJumpCounter)
    {
        this.DoubleJumpCounter = DoubleJumpCounter;
    }

    public void SetJumpPower(float JumpPower)
    {
        this.JumpPower = JumpPower;
    }

    public void SetOnLadder(bool OnLadder)
    {
        this.OnLadder = OnLadder;
        Rigidbody2DReference.gravityScale = (OnLadder) ? 0.0f : 1.0f;
    }

    // Getters
    public float GetJumpPower()
    {
        return JumpPower;
    }
    

    void Start()
    {
        // Obtain component references
        Rigidbody2DReference = GetComponent<Rigidbody2D>();
        AnimatorReference = GetComponent<Animator>();  
        BoxColliderReference = GetComponent<BoxCollider2D>();
        WallJumpCooldown = WallJumpCooldownTime;
        PlayerAudio = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    void Update()
    {
        CheckPause();

        if(WallState)
        {
            WallJumpCooldown -= Time.deltaTime;
        }

        if (!OnLadder)
        {
            HandleJump();
        }

        if(OnWall() && !IsGrounded() && WallJumpCooldown > 0.0f)
        {
            WallState = true;
            Rigidbody2DReference.gravityScale = 0.0f;
            Rigidbody2DReference.velocity = Vector2.zero;
        }
        else if (!OnLadder) 
        {
            ResetGravity();
        }

        if(IsGrounded())
        {
            WallState = false;
            WallJumpCooldown  = WallJumpCooldownTime;
        }
    }

    private void ResetGravity()
    {
        Rigidbody2DReference.gravityScale = 1.0f;
    }

    private void HandleMovement()
    {
        if(DisableInput) return;

        if (OnLadder)
        {
            VerticalInput = Input.GetAxis("Vertical");
            Rigidbody2DReference.velocity = new Vector2(0, VerticalInput * MovementSpeed / 2.0f);
        }

        if ((OnLadder && IsGrounded()) || !OnLadder)
        {
            HorizontalInput = Input.GetAxis("Horizontal");

            Rigidbody2DReference.velocity = new Vector2(HorizontalInput * MovementSpeed, Rigidbody2DReference.velocity.y);

            // Flip player based on input direction
            if (HorizontalInput > 0.0f)
            {
                transform.localScale = new Vector2(-1.0f, 1.0f);
            }
            else if (HorizontalInput < 0.0f)
            {
                transform.localScale = new Vector2(1.0f, 1.0f);
            }

            // Animation variables
            AnimatorReference.SetBool("IsRunning", HorizontalInput != 0);
            AnimatorReference.SetBool("IsGrounded", IsGrounded());
        }
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
        if(Input.GetKeyDown(KeyCode.Space) && !DisableInput)
        {
            if(CanDoubleJump && JumpCount < 2) 
            {
                Rigidbody2DReference.velocity = new Vector2(Rigidbody2DReference.velocity.x, JumpPower);
                JumpCount++;
                PlayerAudio.PlayOneShot(JumpSound);
            }
            else if(IsGrounded())
            {
                Rigidbody2DReference.velocity = new Vector2(Rigidbody2DReference.velocity.x, JumpPower);
                PlayerAudio.PlayOneShot(JumpSound);
            }
            else if(!IsGrounded() && OnWall() && WallJumpCooldown > 0.0f)
            {
                transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
                Rigidbody2DReference.AddRelativeForce(new Vector2(-transform.localScale.x * 10.0f, JumpPower) * 50);
                PlayerAudio.PlayOneShot(JumpSound);
            }

            AnimatorReference.SetTrigger("JumpTrigger");
        }
        
    }

    private void CheckPause()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !Menu.InMenu())
        {
            Time.timeScale = 0.0f;
            PauseMenu.SetActive(true);
        }
    }

    private bool IsGrounded()
    {
        // Determine if player is on ground
        RaycastHit2D RaycastHit = Physics2D.BoxCast(BoxColliderReference.bounds.center, BoxColliderReference.bounds.size, 0, Vector2.down, 0.1f, GroundLayer);
        return RaycastHit.collider != null;
    }

    private bool OnWall()
    {
        // Determine if player is on wall
         RaycastHit2D RaycastHit = Physics2D.BoxCast(BoxColliderReference.bounds.center, BoxColliderReference.bounds.size, 0, new Vector2(-transform.localScale.x,0), 0.1f, WallLayer);
        return RaycastHit.collider != null;
    }

    public void Knock(Vector2 LaunchDirection)
    {
        // Knock player in direction of projectile and disable input
        Rigidbody2DReference.velocity = new Vector2(LaunchDirection.x * KnockBackPower, LaunchDirection.y * KnockBackPower);
        DisableInput = true;
        Invoke("ResetInput", KnockBackTime);
        SetKnockBackTrigger();
        PlayerAudio.PlayOneShot(KnockbackSound);
    }

    private void ResetInput()
    {
        DisableInput = false;
    }

    public void SetKnockBackTrigger()
    {
        AnimatorReference.SetTrigger("KnockBackTrigger");
    }
}
