using UnityEngine;

namespace FPSShooterRuntime
{
    public class FPSController : MonoBehaviour
    {
        [Header("Camera Settings")]
        [SerializeField]
        private CameraControl cameraControl;
        
        [Header("Gravity Settings")]
        [SerializeField] 
        private float gravity=25.0f;
        
        // Controller speed properties.
        [Header("Speed Settings")]
        [SerializeField]
        private float walkSpeed;

        [SerializeField]
        private float runSpeed;

        [SerializeField]
        private float sprintSpeed;

        [SerializeField] 
        private float jumpForce;

        // Controller key properties.
        [Header("Key Settings")]
        [SerializeField]
        private KeyCode walkKey;

        [SerializeField]
        private KeyCode sprintKey;
        
        [SerializeField]
        private KeyCode jumpKey;

        // Stored required properties.
        private float speed;
        private Vector2 input;
        private Vector3 moveDirection;
        private float heightChange;
        private bool isJumped;

        // Stored required components.
        private CharacterController characterController;

        
        protected virtual void Awake()
        {
            characterController = GetComponent<CharacterController>();
            cameraControl.Initialization(transform);

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        
        protected virtual void Update()
        {
            cameraControl.Update();

            ReadInput();
            CalculateSpeed();
            CalculateHeight();
            CalculateDirection();
            ApplyMovement();
        }

        protected virtual void ReadInput()
        {
            input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            isJumped = Input.GetKey(jumpKey);
        }

        protected virtual void CalculateSpeed()
        {
            if (Input.GetKey(walkKey))
                speed = walkSpeed;
            else if (Input.GetKey(sprintKey))
                speed = sprintSpeed;
            else
                speed = runSpeed;
        }

        protected virtual void CalculateHeight()
        {
            if (characterController.isGrounded)
            {
                heightChange = -1;
                if (isJumped)
                {
                    heightChange = jumpForce;
                }
            }
        }
        

        protected virtual void CalculateDirection()
        {
            Vector3 vertical = transform.forward * input.y * speed;
            Vector3 horizontal = transform.right * input.x * speed;
            moveDirection = vertical + horizontal;
            heightChange -= gravity * Time.deltaTime;
            moveDirection.y = heightChange;
        }

        protected virtual void ApplyMovement()
        {
            characterController.Move(moveDirection * Time.deltaTime);
        }

        #region [Getter / Setter]
        public float GetWalkSpeed()
        {
            return walkSpeed;
        }

        public void SetWalkSpeed(float value)
        {
            walkSpeed = value;
        }

        public float GetRunSpeed()
        {
            return runSpeed;
        }

        public void SetRunSpeed(float value)
        {
            runSpeed = value;
        }

        public float GetSprintSpeed()
        {
            return sprintSpeed;
        }

        public void SetSprintSpeed(float value)
        {
            sprintSpeed = value;
        }

        public KeyCode GetWalkKey()
        {
            return walkKey;
        }

        public void SetWalkKey(KeyCode value)
        {
            walkKey = value;
        }

        public KeyCode GetSprintKey()
        {
            return sprintKey;
        }

        public void SetSprintKey(KeyCode value)
        {
            sprintKey = value;
        }
        #endregion
    }
}