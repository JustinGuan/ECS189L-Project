using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Float : MonoBehaviour
{
    /* create a rigidbody for the scepter and assign it to rb
     * create duplicate of scepter in same position, but without this script and assign it to original_rot
     * make sure that the scepter for original_rot is not visible to the player
     * assign the visible scepter to new_rot
     * for the pivot, create a cube and place it where the pivot point should be
     * assign the cube to pivot, turn off the visibility for the cube
     * the target angle can be changed as needed
     */
    float helper;
    public bool shooting;
    public Rigidbody rb;
    public float speed;
    public Transform original_rot;
    public Transform new_rot;
    public GameObject pivot;
    public Vector3 targetAngle = new Vector3(0f, 0f, 5.121f);
    private Vector3 currentAngle;

    // Start is called before the first frame update
    void Start()
    {
        shooting = false;
        currentAngle = transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        var step = speed * Time.deltaTime;

        if (shooting == false)
        {
            helper += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, pivot.transform.position, 0.1f);
            transform.rotation = Quaternion.Slerp(original_rot.rotation, new_rot.rotation, 0.1f);
        }

        if (shooting == true)
        {
            //insert any action that is performed while shooting
        }
    }

    void Shoot()
    {
        shooting = true;
    }

    void StopShoot()
    {
        shooting = false;
    }
}
