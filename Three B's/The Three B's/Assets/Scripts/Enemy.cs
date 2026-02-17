using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public float speed = 2f;

    private Rigidbody2D rb;
    private bool isFacingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Destroy this enemy after 30 seconds
        Destroy(gameObject, 30f);
    }

    void FixedUpdate()
    {
        if (player == null) return;

        Vector2 direction = (player.position - transform.position).normalized;
        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);

        HandleFlip(direction.x);

        if (transform.position.y < -20)
        {
            Destroy(gameObject);
        }
    }

    void HandleFlip(float moveX)
    {
        if (moveX > 0 && !isFacingRight)
            Flip();
        else if (moveX < 0 && isFacingRight)
            Flip();
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
