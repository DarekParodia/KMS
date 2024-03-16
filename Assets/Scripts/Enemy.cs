using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private float minDistance = 1.0f;
    [SerializeField] private float maxDistance = 40.0f;
    [SerializeField] private float maxHP = 100.0f;
    [SerializeField] private float currentHP = 100.0f;
    [SerializeField] private GameObject shardPrefab;
    [SerializeField] private AnimationClip[] movementAnimations;
    
    private Animator Animator;
    
    private float timeFactor = 1.0f;
    
    public bool isSlowedDown = false;
    private float slowDownFactor = 0.5f;
    public bool isFrozen = false;
    
    public GameObject slowTimeCircle;
    private SpriteRenderer spriteRenderer;
    
    private float lastAttack = 0.0f;
    
    private GameObject player;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        Animator = GetComponent<Animator>();
    }

    void Update()
    {
        // gradually change color to red
        spriteRenderer.color = Color.Lerp(Color.white, Color.red, 1 - (currentHP / maxHP));
        if(this.currentHP <= 0)
        {   
            if ( UnityEngine.Random.Range(0f, 1f) <= 0.1f)
            {
                SpawnShard();
            }
            Destroy(gameObject);
        }
        if (isFrozen) return;
        // player detection
        if (getDistanceToPlayer() > maxDistance || player == null)
        {
            player = getClosestPlayer();
        }
        
        // movement 2d top down
        if (player != null)
        {
            Vector3 direction = (player.transform.position - transform.position).normalized;
            this.transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime * timeFactor);
            PlayMoveAnimation(direction); // Play animation based on direction
        }
    }
    
    private void SpawnShard()
    {
        Instantiate(shardPrefab, transform.position, Quaternion.identity);
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
        this.currentHP -= bullet.GetComponent<Bullet>().getDamage();
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
        }
        else
        {
            this.timeFactor = 1.0f;
        }
        if (other.gameObject.CompareTag("Player") && Time.time - lastAttack > 1.0f)
        {
            Debug.Log("PLAYER");
            Player player = other.gameObject.GetComponent<Player>();
            player.playerHit(10.0f);
            lastAttack = Time.time;
        }
    }

    public void restore()
    {
        this.timeFactor = 1.0f;
    }
    
    private void PlayMoveAnimation(Vector3 direction)
    {
        if (direction.y > 0) // Moving up
        {
            Animator.Play(movementAnimations[0].name);
        }
        else if (direction.y < 0) // Moving down
        {
            Animator.Play(movementAnimations[1].name);
        }
    }
}
