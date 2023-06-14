using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WoodCollision : MonoBehaviour
{
    public int branches = 0;
    private WoodSpawner spawner;
    public FlameHealth flameHealth;
    public TextMeshProUGUI woodText;

    // Start is called before the first frame update
    void Start()
    {
        spawner = GameObject.Find("World Generator").GetComponent<WoodSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(flameHealth.health);
        flameHealth.flameText.text = (flameHealth.health / flameHealth.maxHealth) * 100 + "%";
        // flameHealth.HealthBarFiller();
    }

    // Detects if it has collided with the box collider of wood and 
    // destroys it if so.
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Wood")
        {
            Debug.Log("Wood Collected.");
            Destroy(collision.gameObject);
            this.branches++;
            spawner.DestroyWood();
            woodText.text = branches.ToString();
            Debug.Log(branches);
        }
    }
}
