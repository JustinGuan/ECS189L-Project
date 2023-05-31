using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Embers
{
    public class Projectile : MonoBehaviour
    {
        public Transform projectileSpawnPoint;
        public GameObject projectilePrefab;
        public float projectileSpeed = 10;

        private void Update()
        {
            if (Input.GetMouseButtonDown(1)) 
            {
                var projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
                projectile.GetComponent<Rigidbody>().velocity = projectileSpawnPoint.forward * projectileSpeed;
            }
        }
    }
}