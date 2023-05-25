using UnityEngine;
using System.Collections;

namespace Embers
{
	public class PlayerMovement : MonoBehaviour
	{

		[SerializeField] private float moveSmoothTime;
		[SerializeField] private float gravityStrength;
		[SerializeField] private float jumpStrength;
		[SerializeField] private float walkSpeed;
		[SerializeField] private float runSpeed;

		private CharacterController controller;
		private Vector3 currentMoveVelocity;
		private Vector3 moveDampVelocity;
		private Vector3 currentForceVelocity;

		void Start()
		{
			controller = GetComponent<CharacterController>();
		}

		void Update()
		{
			Vector3 playerInput = new Vector3(
				Input.GetAxisRaw("Horizontal"),
				0,
				Input.GetAxisRaw("Vertical")
			);

			if (playerInput.magnitude > 1f)
			{
				playerInput.Normalize();
			}

			Vector3 MoveVector = transform.TransformDirection(playerInput);

			float CurrentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

			currentMoveVelocity = Vector3.SmoothDamp(currentMoveVelocity,
				MoveVector * CurrentSpeed,
				ref moveDampVelocity,
				moveSmoothTime);

			controller.Move(currentMoveVelocity * Time.deltaTime);

			Ray groundCheckRay = new Ray(transform.position, Vector3.down);

			// Check if the player is on the ground
			if (Physics.Raycast(groundCheckRay, 1.1f))
			{
				// Keep player on floor on slopes
				currentForceVelocity.y = -2f;
				if (Input.GetKeyDown(KeyCode.Space))
				{
					currentForceVelocity.y = jumpStrength;
				}
			}
			else
			{
				currentForceVelocity.y -= gravityStrength * Time.deltaTime;
			}

			controller.Move(currentForceVelocity * Time.deltaTime);
		}
	}
}