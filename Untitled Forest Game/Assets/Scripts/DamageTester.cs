using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTester : MonoBehaviour
{
    public AttributesManager playerAtm;
    public AttributesManager enemyAtm;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F11)) 
        {
            playerAtm.DealDamage(enemyAtm.gameObject);
        }
    }
}
