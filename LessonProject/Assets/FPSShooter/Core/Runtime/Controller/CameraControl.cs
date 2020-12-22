using UnityEngine;

namespace FPSShooterRuntime
{
    [System.Serializable]
    public sealed class CameraControl
    {
        // Base camera control properties.
        [SerializeField]
        private Transform cameraTransform;

        [SerializeField]
        private Vector2 sensitivity;

        [SerializeField]
        private Vector2 verticalLimits;

        // Stored required components.
        private Transform controller;

        // Stored required properties.
        private Vector2 input;
        private Vector2 desiredRotation;
        private Quaternion xQuatRotation;
        private Quaternion yQuatRotation;

        public void Initialization(Transform controller)
        {
            this.controller = controller;
        }

        public void Update()
        {
            ReadInput();
            CalculateRotation();
            ClampVerticalRotation();
            CalculateQuaternion();
            ApplyRotation();
        }

        private void ReadInput()
        {
            input.x = Input.GetAxis("Mouse X");
            input.y = Input.GetAxis("Mouse Y");
        }

        private void CalculateRotation()
        {
            desiredRotation.x += input.x * sensitivity.x * Time.deltaTime;
            desiredRotation.y += input.y * sensitivity.y * Time.deltaTime;
        }

        private void ClampVerticalRotation()
        {
            desiredRotation.y = Mathf.Clamp(desiredRotation.y, verticalLimits.x, verticalLimits.y);
        }

        private void CalculateQuaternion()
        {
            xQuatRotation = Quaternion.AngleAxis(desiredRotation.x, Vector3.up);
            yQuatRotation = Quaternion.AngleAxis(desiredRotation.y, -Vector3.right);
        }

        private void ApplyRotation()
        {
            cameraTransform.localRotation = yQuatRotation;
            controller.rotation = xQuatRotation;
        }
    }
}