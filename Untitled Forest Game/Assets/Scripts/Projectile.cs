using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifetime = 3f; // The duration in seconds before the projectile is destroyed

    private Rigidbody projectileRigidbody;

    private void Awake()
    {
        projectileRigidbody = GetComponent<Rigidbody>();
        FindObjectOfType<AudioManager>().PlaySoundEffect("Magic");
    }

    private void Start()
    {
        float speed = 10f;
        projectileRigidbody.velocity = transform.forward * speed;

        StartCoroutine(DestroyAfterLifetime());
    }

    private IEnumerator DestroyAfterLifetime()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }

}
