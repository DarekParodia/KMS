using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private float minDistance = 1.0f;
    [SerializeField] private float maxDistance = 40.0f;
    
    private float timeFactor = 1.0f;
    
    public bool isSlowedDown = false;
    private float slowDownFactor = 0.5f;
    public bool isFrozen = false;
    
    public GameObject slowTimeCircle;
    
    private GameObject player;

    void Update()
    {
        if (isFrozen) return;
        // player detection
        if (getDistanceToPlayer() > maxDistance || player == null)
        {
            player = getClosestPlayer();
        }
        
        // movement 2d top down
        if (player != null)
        {
            this.transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed *Time.deltaTime * timeFactor);
        }
    }

    private GameObject getClosestPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in players)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }
    
    private float getDistanceToPlayer()
    {
        if (this.player == null) return 0;
        return Vector3.Distance(transform.position, this.player.transform.position);
    }

    public void enemyHit(GameObject bullet)
    {
        
    }

    // This method is called when another object enters a trigger collider attached to this object
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("TRIGGER");
        if (other.gameObject.CompareTag("Skill"))
        {
            Debug.Log("szkyl");
            SlowTimeCircle slowTimeCircle = other.gameObject.GetComponent<SlowTimeCircle>();
            this.timeFactor = slowTimeCircle.getTimeFactor();
            slowTimeCircle.addEnemy(this);
        }
    }

    public void restore()
    {
        this.timeFactor = 1.0f;
    }
}
