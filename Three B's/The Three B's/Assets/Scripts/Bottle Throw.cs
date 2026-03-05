using UnityEngine;

public class BottleThrow : MonoBehaviour
{
public float speed = 5f;
    public float life = 1f;
    public GameObject particles;
    public GameObject particles2;
    private float direction = 1f;
    private Rigidbody2D rb;
    public float rotationSpeed = 500f;

    // SFX for bullet


    void Awake()
    {
    
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        Destroy(gameObject, life);
    }
    
    private void Update()
    {
       // transform.position += Vector3.right * direction * speed * Time.deltaTime;
        
    }

    public void SetDirection(float dir)
    {
        direction = dir;
        rb.linearVelocity = new Vector2(direction * speed, speed * 0f);
        rb.AddTorque(-direction * rotationSpeed);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            Instantiate(particles, transform.position, Quaternion.identity);  
            Instantiate(particles2, transform.position, Quaternion.identity);
            particles.GetComponent<ParticleSystem>().Play();  
            particles2.GetComponent<ParticleSystem>().Play();
            
            Destroy(gameObject);
        } 
        
    }
    
}
