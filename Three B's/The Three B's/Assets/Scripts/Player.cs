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

    [Header("Player Sprites")]
    public Sprite Idle;
    public Sprite Run;
    public Sprite Jumps;
    public Sprite Throws;

    private bool isFacingRight = true;
    private SpriteRenderer sr;

    private bool isShooting = false;
    private float shootTimer = 0f;

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
            
            anim.SetBool("Jump", true);
        }
        else{
            anim.SetBool("Jump", false);

        }
        

        // Shoot
        if (Input.GetMouseButtonDown(0) && Time.time >= nextFire)
        {
            Shoot();
            nextFire = Time.time + fireRate;
            anim.SetBool("Throw", true);
        }
        else{
            anim.SetBool("Throw", false);

        }
            
        

        if (move != 0){
            anim.SetBool("isRunning", true);
        }
        else{
            anim.SetBool("isRunning", false);

        }

        // Flip
        if (move > 0 && !isFacingRight)
            Flip();
        else if (move < 0 && isFacingRight)
            Flip();

        HandleAnimations();
    }

    void FixedUpdate()
    {
        rigidBody.linearVelocity = new Vector2(move * speed, rigidBody.linearVelocity.y);
    }

    private void Jump()
    {
        rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        sr.sprite = Jumps;
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        sr.flipX = !sr.flipX;
    }

    private void Shoot()
    {
        isShooting = true;
        shootTimer = 0.2f;

        sr.sprite = Throws;

        GameObject bottle = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
        BottleThrow bt = bottle.GetComponent<BottleThrow>();

        
        
        //anim.SetBool("Throw", true);
        if (isFacingRight)
            bt.SetDirection(1f);
        else
            bt.SetDirection(-1f);
    
        
    }

    private void HandleAnimations()
    {
        if (isShooting)
        {
            shootTimer -= Time.deltaTime;
            if (shootTimer <= 0)
                isShooting = false;
            return;
        }

        if (!isGrounded)
        {
            sr.sprite = Jumps;
        }
        else if (move != 0)
        {
            sr.sprite = Run;
        }
        else
        {
            sr.sprite = Idle;
        }
    }
}

