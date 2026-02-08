using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Player : MonoBehaviour
{
    public float speed = 5f;
    private float move;

    [SerializeField] public float jumpForce = 16f;
    private Rigidbody2D rigidBody;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    
    private bool isGrounded;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
       isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundLayer);
        move = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        
        rigidBody.linearVelocity = new Vector2(move * speed, rigidBody.linearVelocity.y);
    }

    private void Jump()
    {
        rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
}
