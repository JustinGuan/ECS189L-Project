using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth;
    public int currHealth;

    private void Start()
    {
        currHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currHealth -= amount;
        if (currHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}

/*using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Check if the player's health has reached zero
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Perform actions when the player dies
        Debug.Log("Player died!");
        // ...
    }
}*/
