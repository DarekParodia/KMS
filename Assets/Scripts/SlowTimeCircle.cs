using System.Collections;
using UnityEngine;

public class SlowTimeCircle : MonoBehaviour
{   
    private SpriteRenderer spriteRenderer;
    
    [SerializeField] private Sprite slowTimeCircleSprite; // The sprite for the slow time circle
    [SerializeField] private Sprite slowTimeSandSprite; // The sprite for the slow time sand
    
    private Collider2D slowTimeCircleCollider; // The collider for the slow time circle
    
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
        
        // Scale the circle 10 times its original size
        transform.localScale *= 10;
        
        // Wait for the durme effect
        yield return new WaitForSeconds(slowTimeEffectDuration);

        // Remove the slow time circle
        Destroy(gameObject);
    }
}