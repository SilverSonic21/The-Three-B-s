using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BottleThrow : MonoBehaviour
{
    public float speed = 5f;
    public float life = 1f;
    public GameObject particles;
    private float direction = 1f;
    private Rigidbody2D rb;

    // SFX for bullet


    void Start()
    {
        Destroy(gameObject, life);
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        transform.position += Vector3.right * direction * speed * Time.deltaTime;

    }

    public void SetDirection(float dir)
    {
        direction = dir;
        rb.linearVelocity = new Vector2(direction * speed, 0f);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {

            Destroy(collision.gameObject);

            Pop();


            GameObject particle = Instantiate(particles, transform.position, Quaternion.identity);
            particle.GetComponent<ParticleSystem>().Play();

            Destroy(gameObject);
        }
        void Pop()
        {
            //Instantiate(PopEffect, transform.position, transform.rotation);
        }
    }

}

