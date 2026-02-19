using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public float speed = 2f;

    private Rigidbody2D rb;
    private bool isFacingRight = true;
    public GameObject objectToDestroy;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        player = GameObject.FindWithTag("Player").transform;
        //player = GameObject.Ta;
        Destroy(gameObject, 30f);
        Invoke("FunctionToDestroy", 30f);
        StartCoroutine(DestroyCoroutine());
    }

    void FunctionToDestroy()
    {
        Destroy(objectToDestroy);
    }

    IEnumerator DestroyCoroutine()
    {
        yield return new WaitForSeconds(30f);
        Destroy(objectToDestroy);
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
