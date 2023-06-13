using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireplaceMechanic : MonoBehaviour
{
    [SerializeField] private float maxCapacity = 200f;
    [SerializeField] private float extinguishRate = 10f;
    [SerializeField] private float currentCapacity = 0.0f;
    [SerializeField] private float maxRadius = 10.0f;
    [SerializeField] private float curRadius = 0.0f;
    private float timeSinceCheck = 0;

    void Start()
    {
        this.currentCapacity = maxCapacity;
        this.curRadius = this.maxRadius;
    }

    // Update is called once per frame
    void Update()
    {
        // loses health every 2.5 seconds.
        if(timeSinceCheck >= 2.5f)
        {
            // Reset the timer.
            timeSinceCheck = 0;
            // Update the size of the flame depending on the ratio between current and max.
            this.currentCapacity -= extinguishRate;
            float capacityRatio = currentCapacity / maxCapacity;
            // scale the size properly. Simply adjusting to capacitiy ratio makes the flame wonky.
            float xScale = capacityRatio * this.transform.localScale.x;
            float yScale = capacityRatio * this.transform.localScale.y * 0.965f;
            float zScale = capacityRatio * this.transform.localScale.z;
            // We update the position so that the flames are not dancing in the air.
            float yPos = this.transform.position.y - 0.025f;
            // We update both position and scale of the fireplace.
            this.transform.localScale = new Vector3(xScale, yScale, zScale);
            this.transform.position = new Vector3(this.transform.position.x, yPos, this.transform.position.z);
        }
        // Simply update our time counter.
        timeSinceCheck += Time.deltaTime;
    }

    // Haven't tested, but should work similar to losing health.
    void RegenHealth(int numWoods)
    {
        // Update the size of the flame depending on the ratio between current and max.
        this.currentCapacity += (extinguishRate * numWoods);
        float capacityRatio = currentCapacity / maxCapacity;
        // scale the size properly. Simply adjusting to capacitiy ratio makes the flame wonky.
        float xScale = capacityRatio * this.transform.localScale.x;
        float yScale = capacityRatio * this.transform.localScale.y * 1.025f;
        float zScale = capacityRatio * this.transform.localScale.z;
        // We update the position so that the flames are not dancing in the air.
        float yPos = this.transform.position.y + 0.025f;
        // We update both position and scale of the fireplace.
        this.transform.localScale = new Vector3(xScale, yScale, zScale);
        this.transform.position = new Vector3(this.transform.position.x, yPos, this.transform.position.z);
    }
    
    public float GetFireRadius()
    {
        return this.curRadius;
    }
}
