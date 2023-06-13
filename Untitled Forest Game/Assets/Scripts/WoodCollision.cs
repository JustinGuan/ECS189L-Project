using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WoodCollision : MonoBehaviour
{
    private int branches = 0;
    private WoodSpawner spawner;
    public TextMeshProUGUI woodText;

    // Start is called before the first frame update
    void Start()
    {
        spawner = GameObject.Find("Floor").GetComponent<WoodSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        // Testing
        if (Input.GetButtonDown("Fire2"))
        {
            this.branches++;
            woodText.text = branches.ToString();
        }
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
