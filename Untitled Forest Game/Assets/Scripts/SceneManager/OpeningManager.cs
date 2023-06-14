using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningManager : MonoBehaviour
{
    private ScenesManager sm;
    private float openingTime = 80.0f;
    private float timeSinceCheck = 0.0f;

    // Start is called before the first frame update
    void Awake()
    {
        sm = GetComponent<ScenesManager>();
        timeSinceCheck = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        // Update the time
        timeSinceCheck += Time.deltaTime;
        // Try to get the scenesmanager script again.
        if(sm == null) 
        {
            sm = GetComponent<ScenesManager>();
            return;
        }
        // Once 80 seconds has passed.
        if(timeSinceCheck >= openingTime)
        {
            sm.LoadGameLevel();
        }
    }
}
