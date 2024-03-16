using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShardManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Slider shardSlider;
    [SerializeField] private float maxShards = 10;
    [SerializeField] private float currentShards = 0;

    [SerializeField]
    private TextMeshProUGUI shardText;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        shardSlider.value = currentShards;
        shardSlider.maxValue = maxShards;
        shardText.text = currentShards + " / " + maxShards;
        // collision
        
    }
    
    public void addShard()
    {
        currentShards++;
        currentShards = Mathf.Clamp(currentShards, 0, maxShards);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Shard"))
        {
            Destroy(other.gameObject);
            addShard();
        }
    }
}
