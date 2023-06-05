using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody projectileRigidbody;

    private void Awake()
    {
        projectileRigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        float speed = 10f;
        projectileRigidbody.velocity = transform.forward * speed;   
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}