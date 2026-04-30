using UnityEngine;
public class SimplePlayer : MonoBehaviour
{
   private Rigidbody2D rigid;
   private Animator _anim;
   [Header("Ground And Wall Check")]
   [SerializeField] private float groundDistCheck = 1f;
   [SerializeField] private float wallDistCheck = 1f;
   [SerializeField] private LayerMask groundLayer;
   public bool isGrounded = false;
   public bool isWalled = false;
   [Header("Move")]
   [SerializeField] private float moveSpeed = 5f;
   public float X_input;
   public float Y_input;
   public int facing = 1;
   [Header("Jump")]
   [SerializeField] private float jumpForce = 20f;
   [SerializeField] private Vector2 wallJumpForce = new Vector2(10f, 15f);
   public bool isJumping = false;
   public bool isWallJumping = false;
   public bool isWallSliding = false;
   public bool canDoubleJump = false;
   [SerializeField] private float coyoteTimeLimit = .5f;
   [SerializeField] private float bufferTimeLimit = .5f;
   public float coyoteTime;
   public float bufferTime;
   private void Awake()
   {
       rigid = GetComponent<Rigidbody2D>();
       _anim = GetComponentInChildren<Animator>();
   }
   private void Update()
   {
       JumpState();
       Jump();
       WallSlide();
       InputVal();
       Move();
       Flip();
       GroundAndWallCheck();
       Animation();
   }
   private void JumpState()
   {
       if(!isGrounded && !isJumping)
       {
           isJumping = true;
           if(rigid.linearVelocityY <= 0f)
           {
               coyoteTime = Time.time;
           }
       }
       if(isGrounded && isJumping)
       {
           isJumping = false;
           isWallJumping = false;
           isWallSliding = false;
           canDoubleJump = false;
       }
       if (isWalled)
       {
           isJumping = false;
           isWallJumping = false;
           canDoubleJump = false;
           if (isGrounded)
           {
               isWallSliding = false;
           }
           else
           {
               isWallSliding = true;
           }
       }
       else
       {
           isWallSliding = false;
       }
   }
   private void Jump()
   {
       if (Input.GetKeyDown(KeyCode.Space))
       {
           if (!isWalled)
           {
               if(isGrounded)
               {
                   canDoubleJump = true;
                   rigid.linearVelocity = new Vector2(rigid.linearVelocityX, jumpForce);
               }
               else
               {
                   if(rigid.linearVelocityY > 0f && canDoubleJump)
                   {
                       canDoubleJump = false;
                       rigid.linearVelocity = new Vector2(rigid.linearVelocityX, jumpForce);
                   }
                   if(rigid.linearVelocityY <= 0f)
                   {
                       if(Time.time < coyoteTime + coyoteTimeLimit)
                       {
                           coyoteTime = 0f;
                           rigid.linearVelocity = new Vector2(rigid.linearVelocityX, jumpForce);
                       }
                       else
                       {
                           bufferTime = Time.time;
                       }
                   }
               }
           }
           else
           {
               isWallJumping = true;
               rigid.linearVelocity = new Vector2(wallJumpForce.x * facing, wallJumpForce.y);
           }
       }
       else
       {
           if (isGrounded && Time.time < bufferTime + bufferTimeLimit)
           {
               bufferTime = 0f;
               rigid.linearVelocity = new Vector2(rigid.linearVelocityX, jumpForce);
           }
       }
   }
   private void WallSlide()
   {
       if (!isWalled || isGrounded || isWallJumping || rigid.linearVelocityY > 0f )
           return;
       float Y_slide = Y_input < 0f ? 1f : 0.5f;
       rigid.linearVelocity = new Vector2(X_input * moveSpeed, rigid.linearVelocityY * Y_slide);
   }
   private void InputVal()
   {
       X_input = Input.GetAxisRaw("Horizontal");
       Y_input = Input.GetAxisRaw("Vertical");
   }
   private void Move()
   {
       if (isWallJumping)
           return;
       if (isGrounded)
       {
           rigid.linearVelocity = new Vector2(X_input * moveSpeed, rigid.linearVelocityY);
       }
       else
       {
           float X_airMove = X_input != 0f ? X_input * moveSpeed : rigid.linearVelocityX;
           rigid.linearVelocity = new Vector2(X_airMove, rigid.linearVelocityY);
       }
   }
   private void Flip()
   {
       if(rigid.linearVelocityX > 0.1f)
       {
           facing = -1;
           transform.rotation = Quaternion.Euler(0f, 0f, 0f);
       }
       if (rigid.linearVelocityX < -0.1f)
       {
           facing = 1;
           transform.rotation = Quaternion.Euler(0f, 180f, 0f);
       }
   }
   private void GroundAndWallCheck()
   {
       isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundDistCheck, groundLayer);
       isWalled = Physics2D.Raycast(transform.position, transform.right, wallDistCheck, groundLayer);
   }
   private void OnDrawGizmos()
   {
       Gizmos.color = Color.blue;
       Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundDistCheck);
       Gizmos.color = Color.red;
       Gizmos.DrawLine(transform.position, transform.position + transform.right * wallDistCheck);
   }
   private void Animation()
   {
       _anim.SetBool("IsGrounded", isGrounded);
       _anim.SetBool("IsWallSliding", isWallSliding);
       _anim.SetFloat("Speed", Mathf.Abs(X_input));
       _anim.SetFloat("YVelocity", rigid.linearVelocityY);
   }
}