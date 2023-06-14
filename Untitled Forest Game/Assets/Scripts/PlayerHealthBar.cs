using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealthBar : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public Image healthBar;
    [SerializeField] private float health = 100.0f;
    [SerializeField] private float maxHealth = 100.0f;
    float lerpSpeed;

    private void Awake()
    {   
        this.health = maxHealth;
    }

    // Update is called once per frame
    private void Update()
    {
        if (this.health <= 0)
        {
            this.health = 0;
        }

        if (this.health > maxHealth)
        {
            this.health = maxHealth;
        }

        healthText.text = health + "/100";
        HealthBarFiller();
        lerpSpeed = 5.0f * Time.deltaTime;

        //Testing.
        /*
        if(Input.GetButtonDown("Jump"))
        {
             Damage(20.0f);
        }
        if (Input.GetButtonDown("Fire1"))
        {
             Heal(10.0f);
        }
        */
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

    public float GetPlayerHealth()
    {
        return this.health;
    }
}
