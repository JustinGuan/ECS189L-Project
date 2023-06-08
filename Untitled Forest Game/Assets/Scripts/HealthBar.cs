using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public Image healthBar;
    float health = 100.0f;
    float maxHealth = 100.0f;
    float lerpSpeed;

    // Start is called before the first frame update
    private void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    private void Update()
    {
        healthText.text = "Health: " + health + "%";
        HealthBarFiller();
        lerpSpeed = 5.0f * Time.deltaTime;

        // Testing.
        if(Input.GetButtonDown("Jump"))
        {
            Damage(20.0f);
        }
        if (Input.GetButtonDown("Fire1"))
        {
            Heal(10.0f);
        }

        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    void HealthBarFiller()
    {
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, health / maxHealth, lerpSpeed);
    }

    public void Damage(float damageAmount)
    {
        if (health > 0)
        {
            health -= damageAmount;
        }
    }

    public void Heal(float healAmount)
    {
        if (health < maxHealth)
        {
            health += healAmount;
        }
    }
}
