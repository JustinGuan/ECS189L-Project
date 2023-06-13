using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FlameHealth : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public Image healthBar;
    public float health;
    public float maxHealth = 200.0f;
    private const float healthRate = 2.0f;
    private float currentTime;
    private float time;
    private float lerpSpeed;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        time = Time.deltaTime;
        lerpSpeed = 5.0f * time;
        currentTime += time;
        if (health >= 0)
        {
            if (currentTime >= healthRate)
            {
                health -= healthRate;
                currentTime -= healthRate;
            }
        }
        else 
        {
            currentTime = 0;
            health = 0;
        }
        Debug.Log(currentTime);
        healthText.text = (health / maxHealth) * 100 + "%";
        HealthBarFiller();
    }

    void HealthBarFiller()
    {
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, health / maxHealth, lerpSpeed);
    }

}
