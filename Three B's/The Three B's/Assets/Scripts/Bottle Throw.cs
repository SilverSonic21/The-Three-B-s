using UnityEngine;

public class BottleThrow : MonoBehaviour
{
public float speed = 5f;
    public float life = 1f;
    public GameObject particles;
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
            GameObject particle = Instantiate(particles, transform.position, Quaternion.identity); 
            particle.GetComponent<ParticleSystem>().Play();  
            Destroy(gameObject);
        } 
        
    }
    
}
