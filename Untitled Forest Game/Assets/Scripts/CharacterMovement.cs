using UnityEngine;


namespace Embers
{
	public class CharacterMovement : MonoBehaviour
	{
		[SerializeField] private float gravityStrength;
		[SerializeField] private float jumpStrength;
		[SerializeField] private float moveSpeed = 2f;
		[SerializeField] private float turnSpeed = 10f;
		[SerializeField] private KeyCode sprintJoystick = KeyCode.JoystickButton2;
		[SerializeField] private KeyCode sprintKeyboard = KeyCode.LeftShift;
		[SerializeField] private KeyCode jumpKeyboard = KeyCode.Space;


		private float turnSpeedMultiplier;
		private float speed = 0f;
		private float direction = 0f;
		private bool isSprinting = false;
		private Animator anim;
		private Vector3 targetDirection;
		private Vector2 input;
		private Quaternion freeRotation;
		private Camera mainCamera;
		private float velocity;
		private Rigidbody rb;


		// Use this for initialization
		void Start()
		{
			anim = GetComponent<Animator>();
			mainCamera = Camera.main;
			rb = GetComponent<Rigidbody>();
		}

		void Update()
		{
			input.x = Input.GetAxis("Horizontal");
			input.y = Input.GetAxis("Vertical");

			// set speed to both vertical and horizontal inputs
			speed = Mathf.Abs(input.x) + Mathf.Abs(input.y);

			speed = Mathf.Clamp(speed, 0f, 1f);
			speed = Mathf.SmoothDamp(anim.GetFloat("Speed"), speed, ref velocity, 0.1f);
			anim.SetFloat("Speed", speed);

			isSprinting = Input.GetKey(sprintKeyboard) || Input.GetKey(sprintJoystick) && speed > 0f;
			anim.SetBool("isSprinting", isSprinting);

			speed = isSprinting ? 2 * speed : speed;


			transform.Translate(targetDirection * speed * moveSpeed * Time.deltaTime, Space.World);
			// Vertical movement
			Ray groundCheckRay = new Ray(transform.position, Vector3.down);

			// Check if the player is on the ground
			if (Physics.Raycast(groundCheckRay, 0.5f))
			{
				if (Input.GetKeyDown(jumpKeyboard))
				{
					rb.AddForce(Vector3.up * jumpStrength, ForceMode.Impulse);
				}
			}

			// Apply gravity to player
			rb.AddForce(Physics.gravity * (gravityStrength - 1) * rb.mass * Time.deltaTime);

			// Update target direction relative to the camera view (or not if the Keep Direction option is checked)
			UpdateTargetDirection();
			if (input != Vector2.zero && targetDirection.magnitude > 0.1f)
			{
				Vector3 lookDirection = targetDirection.normalized;
				freeRotation = Quaternion.LookRotation(lookDirection, transform.up);
				var diferenceRotation = freeRotation.eulerAngles.y - transform.eulerAngles.y;
				var eulerY = transform.eulerAngles.y;

				if (diferenceRotation < 0 || diferenceRotation > 0) eulerY = freeRotation.eulerAngles.y;
				var euler = new Vector3(0, eulerY, 0);

				transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(euler), turnSpeed * turnSpeedMultiplier * Time.deltaTime);
			}
		}

		public virtual void UpdateTargetDirection()
		{
			turnSpeedMultiplier = 1f;
			var forward = mainCamera.transform.TransformDirection(Vector3.forward);
			forward.y = 0;

			//get the right-facing direction of the referenceTransform
			var right = mainCamera.transform.TransformDirection(Vector3.right);

			// determine the direction the player will face based on input and the referenceTransform's right and forward directions
			targetDirection = input.x * right + input.y * forward;
		}
	}
}