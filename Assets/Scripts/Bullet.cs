using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
     public float bulletTTL = 2.0f;
     public float bulletVelo = 10.0f;
     public float bulletKnockbackFactor = 1.0f;
    void enemyHit(GameObject hitObject)
    {
        // Get the enemy script
        Enemy enemy = hitObject.GetComponent<Enemy>();
        // Call the enemyHit function
        enemy.enemyHit(this.gameObject);
        // Destroy the bullet
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the bullet has hit an enemy
        if (other.gameObject.CompareTag("Enemy"))
        {
            // Call the enemyHit function
            enemyHit(other.gameObject);

            // Get the Rigidbody2D component of the enemy
            Rigidbody2D enemyRigidbody = other.gameObject.GetComponent<Rigidbody2D>();

            // Calculate the force to be applied
            Vector2 force = transform.right * bulletVelo * bulletKnockbackFactor;

            // Apply the force to the enemy
            enemyRigidbody.AddForce(force);
            
            // destroy itself
            Destroy(gameObject);
        }
    }
}
