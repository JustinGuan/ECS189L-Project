using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationTracker : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject fireplace;

    Vector3 GetPlayerPos() {
        return player.transform.position;
    }

    Vector3 GetFireplacePos() {
        return fireplace.transform.position;
    }
}
