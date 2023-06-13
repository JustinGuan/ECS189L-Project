using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationTracker : MonoBehaviour
{
    [SerializeField] public GameObject player;
    [SerializeField] public GameObject fireplace;

    public Vector3 GetPlayerPos() {
        return player.transform.position;
    }

    public Vector3 GetFireplacePos() {
        return fireplace.transform.position;
    }
}
