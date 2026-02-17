using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5f;
    private float move;
    private Rigidbody2D rigidBody;

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
    public Sprite Throw;

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

        // Shoot
        if (Input.GetMouseButtonDown(0) && Time.time >= nextFire)
        {
            Shoot();
            nextFire = Time.time + fireRate;
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
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;

        // Flip firing point
        Vector3 firePos = firingPoint.localPosition;
        firePos.x *= -1;
        firingPoint.localPosition = firePos;
    }

    private void Shoot()
    {
        GameObject bottle = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
        BottleThrow bt = bottle.GetComponent<BottleThrow>();

        sr.sprite = Throw;

        if (isFacingRight)
            bt.SetDirection(1f);
        else
            bt.SetDirection(-1f);
    }

    private void HandleAnimations()
    {
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

