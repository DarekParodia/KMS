using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShardManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Slider shardSlider;
    [SerializeField] private float maxShards = 10;
    [SerializeField] private float currentShards = 0;
    [SerializeField] private Collider2D playerCollider;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        shardSlider.value = currentShards;
        shardSlider.maxValue = maxShards;
        
        // collision
        
    }
    
    public void addShard()
    {
        currentShards++;
        currentShards = Mathf.Clamp(currentShards, 0, maxShards);
    }

    public void damageUpgrade(){}
}
