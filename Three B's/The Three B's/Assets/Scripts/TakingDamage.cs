using UnityEngine;
using System.Collections;

public class TakingDamage : MonoBehaviour
{
    public float flashDuration = 0.1f;
    public int flashCount = 3;
    public Color flashColor = Color.red;

    private SpriteRenderer spriteRenderer;
    private Coroutine flashCoroutine;
    private Color originalColor;
    public Endgame endgame;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer == null)
        {
            //Debug.LogError("SpriteRenderer component not found!");
            return;
        }

        originalColor = spriteRenderer.color;
    }

    public void TakeDamage(int hitCount)
    {
        if (spriteRenderer == null) return;

        if (flashCoroutine != null)
        {
            StopCoroutine(flashCoroutine);

        }

        flashCoroutine = StartCoroutine(DamageFlashRoutine());
        Debug.Log("Flash");
    }

    IEnumerator DamageFlashRoutine()
    {
        for (int i = 0; i < flashCount; i++)
        {
            spriteRenderer.color = flashColor;
            yield return new WaitForSeconds(flashDuration / 2f);

            spriteRenderer.color = originalColor;
            yield return new WaitForSeconds(flashDuration / 2f);
        }

        spriteRenderer.color = originalColor;
        flashCoroutine = null;
    }
}
