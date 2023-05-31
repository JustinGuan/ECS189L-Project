using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Embers
{
    public class Enemy : MonoBehaviour
    {
        public int maxHealth = 100;
        int currentHealth;

        void Start () 
        {
            currentHealth = maxHealth;
        }

        public void TakeDamage(int damage)
        {
            //Take damage animation
            currentHealth -= damage;

            if(currentHealth < 0) 
            {
                Die();
            }
        }

        void Die() 
        {
            Debug.Log("Enemy died!");
            //Die animation
            //Remove enemy
        }
    }
}