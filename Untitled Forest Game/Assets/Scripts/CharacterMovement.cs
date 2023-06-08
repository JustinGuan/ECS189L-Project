using UnityEngine;


namespace Embers
{
	public class CharacterMovement : MonoBehaviour
	{
		[SerializeField] private float gravityStrength;
		[SerializeField] private float jumpStrength;
		[SerializeField] private float moveSpeed = 2f;
		public bool useCharacterForward = false;
		public bool lockToCameraForward = false;
		public float turnSpeed = 10f;
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
			if (useCharacterForward)
				speed = Mathf.Abs(input.x) + input.y;
			else
				speed = Mathf.Abs(input.x) + Mathf.Abs(input.y);

			speed = Mathf.Clamp(speed, 0f, 1f);
			speed = Mathf.SmoothDamp(anim.GetFloat("Speed"), speed, ref velocity, 0.1f);
			anim.SetFloat("Speed", speed);


			transform.Translate(targetDirection * speed * moveSpeed * Time.deltaTime, Space.World);
			// Vertical movement
			Ray groundCheckRay = new Ray(transform.position, Vector3.down);

			// Check if the player is on the ground
			if (Physics.Raycast(groundCheckRay, 1.0f))
			{
				if (Input.GetKeyDown(jumpKeyboard))
				{
					rb.AddForce(Vector3.up * jumpStrength, ForceMode.Impulse);
				}
			}
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

		// Update is called once per frame
		void FixedUpdate()
		{
#if ENABLE_LEGACY_INPUT_MANAGER


			// Apply gravity to player
			rb.AddForce(Physics.gravity * (gravityStrength - 1) * rb.mass);



			if (input.y < 0f && useCharacterForward)
				direction = input.y;
			else
				direction = 0f;

			anim.SetFloat("Direction", direction);

			// set sprinting
			isSprinting = ((Input.GetKey(sprintJoystick) || Input.GetKey(sprintKeyboard)) && input != Vector2.zero && direction >= 0f);
			anim.SetBool("isSprinting", isSprinting);


#else
        InputSystemHelper.EnableBackendsWarningMessage();
#endif
		}

		public virtual void UpdateTargetDirection()
		{
			if (!useCharacterForward)
			{
				turnSpeedMultiplier = 1f;
				var forward = mainCamera.transform.TransformDirection(Vector3.forward);
				forward.y = 0;

				//get the right-facing direction of the referenceTransform
				var right = mainCamera.transform.TransformDirection(Vector3.right);

				// determine the direction the player will face based on input and the referenceTransform's right and forward directions
				targetDirection = input.x * right + input.y * forward;
			}
			else
			{
				turnSpeedMultiplier = 0.2f;
				var forward = transform.TransformDirection(Vector3.forward);
				forward.y = 0;

				//get the right-facing direction of the referenceTransform
				var right = transform.TransformDirection(Vector3.right);
				targetDirection = input.x * right + Mathf.Abs(input.y) * forward;
			}
		}
	}
}