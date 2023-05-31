using UnityEngine;

namespace embers
{
    public class CharacterMovement : MonoBehaviour
    {
        public float speed = 5f;  // Speed of the character movement
        public float jumpForce = 5;
        public bool isOnGround = true;
        private float horizontalInput;
        private float verticalInput;
        private Rigidbody playerRb;

        void Start()
        {
            playerRb = GetComponent<Rigidbody>();
        }

        // FixedUpdate is used for physics calculations
        void FixedUpdate()
        {
            // Get player input
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");

            // Move player forward
            transform.Translate(Vector3.forward * Time.deltaTime * speed * horizontalInput);
            transform.Translate(Vector3.right * Time.deltaTime * speed * verticalInput);

            if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
            {
                playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isOnGround = false;
            }

        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                isOnGround = true;
            }
        }
    }

}