using System.Collections;
using UnityEngine;

public class SlowTimeCircle : MonoBehaviour
{   
    private SpriteRenderer spriteRenderer;
    
    [SerializeField] private Sprite slowTimeCircleSprite; // The sprite for the slow time circle
    [SerializeField] private Sprite slowTimeSandSprite; // The sprite for the slow time sand
    
    public float slowTimeEffectDuration = 15f; // Duration of the slow time effect in seconds
    public float activationDelay = 3f; // Delay before the slow time effect is applied

    private void Start()
    {   
        // Get the SpriteRenderer component
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = slowTimeSandSprite;
        StartCoroutine(ActivateAfterDelay());
    }

    private IEnumerator ActivateAfterDelay()
    {
        // Wait for the activation delay
        yield return new WaitForSeconds(activationDelay);
        
        // Change the sprite to the slow time circle
        spriteRenderer.sprite = slowTimeCircleSprite;
        
        // Apply the slow time effect to enemies within the circle's radius
        Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(transform.position, transform.localScale.x / 2);
        foreach (Collider2D enemy in enemiesInRange)
        {
            // Assuming enemies have a script that can be slowed down
            Enemy enemyScript = enemy.GetComponent<Enemy>();
            if (enemyScript != null)
            {
                enemyScript.isSlowedDown = true;
            }
        }

        // Wait for the duration of the slow time effect
        yield return new WaitForSeconds(slowTimeEffectDuration);

        // Reset the isSlowedDown variable for enemies within the circle's radius
        enemiesInRange = Physics2D.OverlapCircleAll(transform.position, transform.localScale.x / 2);
        foreach (Collider2D enemy in enemiesInRange)
        {
            Enemy enemyScript = enemy.GetComponent<Enemy>();
            if (enemyScript != null)
            {
                enemyScript.isSlowedDown = false;
            }
        }

        // Remove the slow time circle
        Destroy(gameObject);
    }
}