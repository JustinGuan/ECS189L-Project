using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodCollision : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Text wood;
    private int branches;

    // Start is called before the first frame update
    void Start()
    {
        // this.wood.text = "Branch";
    }

    // Update is called once per frame
    void Update()
    {
        // this.wood.text = "x" + (this.branches * 1);
    }

    // Detects if it has collided with the box collider of wood and 
    // destroys it if so.
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Wood")
        {
            Debug.Log("Wood Collected.");
            Destroy(collision.gameObject);
            this.branches++;
        }
    }
}
