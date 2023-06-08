using UnityEngine;
using System.Collections;

namespace Embers
{
	public class CameraSwitch : MonoBehaviour
	{

		[SerializeField] private GameObject firstPersonCamera;
		[SerializeField] private GameObject thirdPersonCamera;
		private bool firstPerson = false;

		// LateUpdate is called after Update each frame
		void LateUpdate()
		{
			if (Input.GetKeyDown(KeyCode.C))
			{
				firstPerson = !firstPerson;
				firstPersonCamera.SetActive(firstPerson);
				thirdPersonCamera.SetActive(!firstPerson);
			}
		}
	}
}