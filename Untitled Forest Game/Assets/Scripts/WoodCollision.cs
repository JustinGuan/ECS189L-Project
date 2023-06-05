using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WoodCollision : MonoBehaviour
{
    // [SerializeField] private UnityEngine.UI.Text wood;
    private int branches = 0;
    private WoodSpawner spawner;
    public TextMeshProUGUI woodText;

    // Start is called before the first frame update
    void Start()
    {
        // this.wood.text = "Branch";
        spawner = GameObject.Find("Floor").GetComponent<WoodSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        // this.wood.text = "x" + (this.branches * 1);
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
            woodText.text = "Wood: " + branches.ToString();
            Debug.Log(branches);
        }
    }
}
