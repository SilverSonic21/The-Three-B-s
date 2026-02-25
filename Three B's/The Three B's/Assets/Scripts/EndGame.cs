using UnityEngine;
using UnityEngine.SceneManagement;

public class Endgame : MonoBehaviour
{
    public TakingDamage takingdamage;
    public Health health;
    //public WinScreen winScreen;

    [Header("Settings")]
    public int maxHits = 3;

    private int hitCount = 0;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        health = player.GetComponent<Health>();

        if (player != null)
        {
            takingdamage = player.GetComponent<TakingDamage>();
        }

        takingdamage.endgame = this;
        health.endgame = this;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            hitCount++;

            if (takingdamage != null)
                takingdamage.TakeDamage(hitCount);

            if (health != null)
                health.HealthDamage(hitCount);
                Debug.Log(health);

            //Debug.Log("Hit #" + hitCount);

            if (hitCount >= maxHits)
            {
                Lose();
            }
        }
    }

    void Lose()
    {
        //Debug.Log("Game Over!");

      SceneManager.LoadScene("Lose");
    }

    public void Win()
    {
       // if (winScreen != null)
         //  winScreen.DisplayWinResults();
    }
}
