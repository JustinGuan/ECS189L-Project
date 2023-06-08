using UnityEditor.PackageManager.UI;
using UnityEngine;

public class SceneTestMovement : MonoBehaviour
{
//script only for testing visuals
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            this.transform.Translate(new Vector3(-10f, 0f, 0f) * Time.deltaTime, Space.World);


        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {


            this.transform.Translate(new Vector3(10f, 0f, 0f) * Time.deltaTime, Space.World);
        }
        else if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            this.transform.Translate(new Vector3(0f, 0f, 10f) * Time.deltaTime, Space.World);
        }
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            this.transform.Translate(new Vector3(0f, 0f, -10f) * Time.deltaTime, Space.World);
        }
    }
}

