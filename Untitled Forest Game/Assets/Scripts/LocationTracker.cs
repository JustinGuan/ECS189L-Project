using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationTracker : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject fireplace;

    public Vector3 GetPlayerPos() {
        return player.transform.position;
    }

    public Vector3 GetFireplacePos() {
        return fireplace.transform.position;
    }

    public void SetPlayer(GameObject go)
    {
        this.player = go;
    }

    public void SetFire(GameObject go)
    {
        this.fireplace = go;
    }
}
