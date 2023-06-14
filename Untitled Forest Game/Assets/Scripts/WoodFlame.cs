// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using TMPro;
// using UnityEngine.UI;

// public class WoodFlame : MonoBehaviour
// {
//     // Variables for the flame UI.
//     public TextMeshProUGUI flameText;
//     public Image healthBar;
//     public float flameHealth = 100.0f;
//     public float maxHealth = 200.0f;
//     private const float healthRate = 2.0f;
//     private float currentTime;
//     private float time;
//     private float lerpSpeed;
//     private bool withinFlame;
    
//     // Variables for wood UI.
//     public TextMeshProUGUI woodText;
//     private WoodSpawner spawner;
//     private int branches = 0;


//     // Start is called before the first frame update
//     void Start()
//     {
//         spawner = GameObject.Find("World Generator").GetComponent<WoodSpawner>();
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         time = Time.deltaTime;
//         lerpSpeed = 5.0f * time;
//         currentTime += time;
//         if (flameHealth > maxHealth)
//         {
//             flameHealth = maxHealth;
//         }
//         else if (flameHealth >= 0)
//         {
//             if (currentTime >= healthRate)
//             {
//                 flameHealth -= healthRate;
//                 currentTime -= healthRate;

//             }
//             if (withinFlame == true)
//             {
//                 if (Input.GetKeyDown(KeyCode.Z))
//                 {
//                     branches -= 1;
//                     woodText.text = branches.ToString();
//                     Heal(5.0f);
//                     currentTime = 0;
//                     Debug.Log("branches in: " + branches);
//                 }
//             }
//         }
//         else 
//         {
//             currentTime = 0;
//             flameHealth = 0;
//             Debug.Log("Oh no, flame is 0!");
//         }
//         Debug.Log("health: " + flameHealth);
//         flameText.text = (flameHealth / maxHealth) * 100 + "%";
//         HealthBarFiller();
//     }

//     private void OnTriggerEnter(Collider collision)
//     {
//         if (collision.gameObject.tag == "Flame")
//         {
//             Debug.Log("Within fire camp");
//             Debug.Log("Num of branches: " + branches);
//             withinFlame = true;
//         }
//         else
//         {
//             withinFlame = false;
//         }

//         if (collision.gameObject.tag == "Wood")
//         {
//             Debug.Log("Wood Collected.");
//             Destroy(collision.gameObject);
//             branches++;
//             spawner.DestroyWood();
//             woodText.text = branches.ToString();
//             Debug.Log(branches);
//         }
//     }

//     public void HealthBarFiller()
//     {
//         healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, flameHealth / maxHealth, lerpSpeed);
//     }

//     public void Heal(float healAmount)
//     {
//         if (flameHealth < maxHealth)
//         {
//             flameHealth += healAmount;
//         }
//     }

// }
