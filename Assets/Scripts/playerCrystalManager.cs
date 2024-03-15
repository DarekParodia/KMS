using System.Collections;
using System.Collections.Generic;
using UnityEditor.TextCore.Text;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField]private GameObject pointer;
    [SerializeField] private float minPointerDistance = 2.0f;
    [SerializeField] private float rotationSmoothness = 2.0f;
    [SerializeField]private bool isMining = false;
    
    private MultiInputSystem multiInputSystem;
    
    private GameObject closestCrystal;
    void Start()
    {
        this.multiInputSystem = GetComponent<MultiInputSystem>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.closestCrystal = getClosestCrystal();
        if (this.closestCrystal != null)
        {
            // if crystal is close enough, hide pointer
            if (getClosestCrystalDistance() < minPointerDistance)
            {
                pointer.SetActive(false);
            }
            else
            {
                pointer.transform.rotation = Quaternion.Slerp(pointer.transform.rotation, Quaternion.Euler(0, 0, getClosestCrystalAngle()), rotationSmoothness * Time.deltaTime);
                pointer.SetActive(true);
            }
        }
        else
        {
            pointer.SetActive(false);
        }
    }

    private GameObject getClosestCrystal()
    {
        GameObject[] crystals = GameObject.FindGameObjectsWithTag("Crystal");

        GameObject closestCrystal = null;
        float minDistance = Mathf.Infinity;

        Vector3 playerPosition = transform.position;

        foreach (GameObject crystal in crystals)
        {
            if(crystal.GetComponent<Crystal>().isMined()) continue;
            float distance = Vector3.Distance(playerPosition, crystal.transform.position);
            if (distance < minDistance)
            {
                closestCrystal = crystal;
                minDistance = distance;
            }
        }
        return closestCrystal;
    }
    

    private float getClosestCrystalDistance()
    {
        if (this.closestCrystal == null) return 0;
        return Vector3.Distance(transform.position, this.closestCrystal.transform.position);
    }
    
    private float getClosestCrystalAngle()
    {
        if (this.closestCrystal == null) return 0;
        Vector3 direction = this.closestCrystal.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        return angle;
    }
    
    public void startMining()
    {
        this.isMining = true;
    }
    public void stopMining()
    {
        this.isMining = false;
    }
}
