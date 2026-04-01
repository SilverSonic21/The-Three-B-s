using UnityEngine;

public class EnemyDeathTracker : MonoBehaviour
{
    public EnemySpawner spawner;

    void OnDestroy()
    {
        if (spawner != null)
        {
            spawner.EnemyDied();
        }
    }
}