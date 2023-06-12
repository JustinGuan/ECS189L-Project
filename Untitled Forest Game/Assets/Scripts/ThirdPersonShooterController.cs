using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class ThirdPersonShooterController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera aimVirtualCamera;
    [SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();
    [SerializeField] private Transform debugTransform;
    [SerializeField] private Transform pfFireProjectile;
    [SerializeField] private Transform spawnFireProjectile;
    [SerializeField] private KeyCode fireKeyboard = KeyCode.Mouse1;



    // Start is called before the first frame update
    private void Awake()
    {
  
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 mouseWorldPosition = Vector3.zero;

        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask))
        {
            debugTransform.position = raycastHit.point;
            mouseWorldPosition = raycastHit.point;
        }

        if (Input.GetKeyDown(fireKeyboard))
        {
            // Instantiate the projectile prefab at the desired position and rotation
            Vector3 aimDirection = (mouseWorldPosition - spawnFireProjectile.position).normalized;
            Instantiate(pfFireProjectile, spawnFireProjectile.position, Quaternion.LookRotation(aimDirection, Vector3.up));
        }
    }
}
