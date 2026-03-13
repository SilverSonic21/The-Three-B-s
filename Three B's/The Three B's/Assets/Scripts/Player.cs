using UnityEngine;

public class Player : MonoBehaviour
{
   [Header("Movement")]
    public float speed = 5f;
    private float move;
    private Rigidbody2D rigidBody;
    [SerializeField] private Animator anim; 

    [Header("Jump")]
    [SerializeField] private float jumpForce = 16f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    private bool isGrounded;

    [Header("Shooting")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;
    [Range(0.1f, 1f)]
    [SerializeField] private float fireRate = 0.5f;
    private float nextFire = 0f;

    private bool isFacingRight = true;
    private SpriteRenderer sr;


    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        
    }

    void Update()
    {
        move = Input.GetAxisRaw("Horizontal");

        // Ground Check
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
            
        }

        anim.SetBool("Jump", !isGrounded);
        anim.SetBool("isRunning", move != 0);

        // Shoot
        if (Input.GetMouseButtonDown(0) && Time.time >= nextFire)
        {
            Shoot();
            nextFire = Time.time + fireRate;
           anim.SetBool("Throw", true);
        }
        else
        {
            anim.SetBool("Throw", false);
        }

        // Flip
        if (move > 0 && !isFacingRight)
            Flip();
        else if (move < 0 && isFacingRight)
            Flip();

    }

    void FixedUpdate()
    {
        rigidBody.linearVelocity = new Vector2(move * speed, rigidBody.linearVelocity.y);
    }

    private void Jump()
    {
        rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
       
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        sr.flipX = !sr.flipX;
    }

    private void Shoot()
    {
        GameObject bottle = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
        BottleThrow bt = bottle.GetComponent<BottleThrow>();

        
        
        //anim.SetBool("Throw", true);
        if (isFacingRight)
            bt.SetDirection(1f);
        else
            bt.SetDirection(-1f);
    
        
    }
}

