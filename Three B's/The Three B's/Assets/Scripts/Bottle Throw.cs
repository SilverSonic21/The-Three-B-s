using UnityEngine;

public class BottleThrow : MonoBehaviour
{
public float speed = 5f;
    public float life = 1f;
    public GameObject particles;
    public GameObject particles2;
    private float direction = 1f;
    private bool hasHit = false;
    private Rigidbody2D rb;
    public float rotationSpeed = 500f;
    public AudioSource audioSource;
    public AudioClip breakSound;

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
        if (hasHit) return;
        if (collision.gameObject.CompareTag("Enemy"))
        {
            hasHit = true;

           AudioSource.PlayClipAtPoint(breakSound, transform.position);

        
            Destroy(collision.gameObject);

            
            GameObject p1 = Instantiate(particles, transform.position, Quaternion.identity);
            GameObject p2 = Instantiate(particles2, transform.position, Quaternion.identity);

            p1.GetComponent<ParticleSystem>().Play();
            p2.GetComponent<ParticleSystem>().Play();

        
        Destroy(gameObject);
        } 
        Debug.Log("Hit");
        
    }
    
}
