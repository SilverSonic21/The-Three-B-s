using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public float speed = 5f;

    private Rigidbody2D rb;
    private bool isFacingRight = true;
    private Animator anime; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        player = GameObject.FindWithTag("Player").transform;
        Destroy(gameObject, 30f);
        anime = GetComponent<Animator>();
       
    }


    void FixedUpdate()
    {
        if (player == null) return;

            float moveX = Mathf.Sign(player.position.x - transform.position.x);
            
            anime.SetBool("Follow", moveX != 0);
        rb.linearVelocity = new Vector2(moveX * speed, rb.linearVelocity.y);

        HandleFlip(moveX);

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
