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
    [SerializeField] private float groundRadius = 0.2f;
    private bool isGrounded;

    [Header("Shooting")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;
    [Range(0.1f, 1f)]
    [SerializeField] private float fireRate = 0.5f;
    //[SerializeField] private float bulletSpeed = 10f;
    private float nextFire = 0f;

    private bool isFacingRight = true;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        move = Input.GetAxisRaw("Horizontal");


        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);



        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            Jump();


        if (Input.GetMouseButtonDown(0) && Time.time >= nextFire)
        {
            Shoot();
            nextFire = Time.time + fireRate;
        }


        if (move < 0 && !isFacingRight)
            Flip();
        else if (move > 0 && isFacingRight)
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


        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;


        Vector3 firePos = firingPoint.localPosition;
        firePos.x *= -1;
        firingPoint.localPosition = firePos;
    }

    private void Shoot()
    {
        GameObject bottle = Instantiate(bulletPrefab, firingPoint.position, firingPoint.rotation);
        BottleThrow bt = bottle.GetComponent<BottleThrow>();

        if (isFacingRight)
            bt.SetDirection(-1f);
        else
            bt.SetDirection(1f);
    }
}
