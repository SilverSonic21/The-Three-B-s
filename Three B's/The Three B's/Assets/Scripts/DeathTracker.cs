using UnityEngine;

public class EnemyDeathTracker : MonoBehaviour
{
    public EnemySpawner spawner;

    // Triggered when the enemy is destroyed (like by your bottle)
    void OnDestroy()
    {
        // Check if the application is quitting to avoid errors on stop
        if (spawner != null && !gameObject.scene.isLoaded == false)
        {
            spawner.EnemyDied();
        }
    }
}