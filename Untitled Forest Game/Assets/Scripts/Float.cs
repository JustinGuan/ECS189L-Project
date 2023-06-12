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
    public Transform pivot2;
    public Vector3 targetAngle = new Vector3(0f, 0f, 5.121f);
    private Vector3 currentAngle;
    private Vector3 startpos;

    // Start is called before the first frame update
    void Start()
    {
        shooting = false;
        currentAngle = transform.eulerAngles;
        Vector3 startpos = pivot2.transform.position;
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
            transform.position = pivot.transform.position + new Vector3(0.0f, Mathf.Sin(Time.time), 0.0f);
        }

        if (shooting == true)
        {
            transform.RotateAround(pivot2.transform.position, Vector3.up, 700 * Time.deltaTime);
            transform.Rotate(0f, -1f, 1f, Space.Self);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopShoot();
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
