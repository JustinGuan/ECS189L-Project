using UnityEngine;
using System.Collections;

namespace Embers
{
	public class PlayerLook : MonoBehaviour
	{

		public Transform playerCamera;
		public Vector2 lookSensitivities;

		private Vector2 XYRotation;

		void Start()
		{
			Cursor.lockState = CursorLockMode.Locked;
		}

		void Update()
		{
			Vector2 MouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

			XYRotation.x -= MouseInput.y * lookSensitivities.y;
			XYRotation.y += MouseInput.x * lookSensitivities.x;

			XYRotation.x = Mathf.Clamp(XYRotation.x, -90, 90);

			transform.eulerAngles = new Vector3(0f, XYRotation.y, 0f);
			playerCamera.localEulerAngles = new Vector3(XYRotation.x, 0f, 0f);
		}
	}
}
