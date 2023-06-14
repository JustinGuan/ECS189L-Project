using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FlameHealth : MonoBehaviour
{
    public TextMeshProUGUI flameText;
    public Image healthBar;
    public WoodCollision branch;
    public float health = 100.0f;
    public float maxHealth = 200.0f;
    private const float healthRate = 2.0f;
    private float currentTime;
    private float time;
    private float lerpSpeed;
    private bool withinFlame;

    // Start is called before the first frame update
    void Start()
    {
        health = 100.0f;
    }

    // Update is called once per frame
    void Update()
    {
        time = Time.deltaTime;
        lerpSpeed = 5.0f * time;
        currentTime += time;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        else if (health >= 0)
        {
            if (currentTime >= healthRate)
            {
                health -= healthRate;
                currentTime -= healthRate;
            }
            if (withinFlame == true)
            {
                if (branch.branches != 0 && Input.GetKeyDown(KeyCode.Z))
                {
                    branch.branches -= 1;
                    branch.woodText.text = branch.branches.ToString();
                    Heal(5.0f);
                    currentTime = 0;
                    Debug.Log("branches in: " + branch.branches);
                }
            }
        }
        else 
        {
            currentTime = 0;
            this.health = 0;
        }
        // Debug.Log("health: " + this.health);
        flameText.text = (health / maxHealth) * 100 + "%";
        HealthBarFiller();

    }

    void HealthBarFiller()
    {
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, health / maxHealth, lerpSpeed);
    }


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Flame" && branch.branches != 0)
        {
            Debug.Log("Within fire camp");
            Debug.Log("Num of branches: " + branch.branches);
            withinFlame = true;
        }
        else
        {
            withinFlame = false;
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
