using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] private float walkSpeed = 5f;
        [SerializeField] private float runSpeed = 10f;
        [SerializeField] private float jumpForce = 5f;

        [SerializeField] private Rigidbody childRigidbody;
        [SerializeField] private Transform camera;
    
        [SerializeField] private Joystick joystick;
        [SerializeField] private GameObject buyAmmoButton;

    
        private bool isGrounded;
        private bool isRunning;

        private void Update()
        {
            float horizontalInput = joystick.Horizontal;
            float verticalInput = joystick.Vertical;
            Vector3 moveInput = new Vector3(horizontalInput, 0, verticalInput);
            Vector3 move = camera.transform.rotation * moveInput;

            float currentSpeed = isRunning ? runSpeed : walkSpeed;
            Vector3 velocity = move * currentSpeed;
            velocity.y = childRigidbody.velocity.y;

            childRigidbody.velocity = velocity;
        }

        public void HandleJumpButton()
        {
            if (isGrounded)
            {
                childRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
    
        public void SetRunBtnIsClicked()
        {
            isRunning = true;
        }
        public void SetRunBtnIsNotClicked()
        {
            isRunning = false;
        }
    
        private void OnCollisionStay(Collision collision)
        {
            if (collision.collider) 
            {
                isGrounded = true;
            }

            if (collision.collider.CompareTag("BuyZone"))
            {
                buyAmmoButton.SetActive(true);
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            if (collision.collider)  
            {
                isGrounded = false;
            }
            if (collision.collider.CompareTag("BuyZone"))
            {
                buyAmmoButton.SetActive(false);
            }
        }
     
    }
}