using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TopDown.Movement
{
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerMovement : Mover
    {
        private void OnMove(InputValue value)
        {
            var cameraRotation = new Vector3(Camera.main.transform.rotation.x, Camera.main.transform.rotation.y, Camera.main.transform.rotation.z);
            var IsoMatrix = Matrix4x4.Rotate(Quaternion.Euler(cameraRotation));

            var skewedInput = IsoMatrix.MultiplyPoint3x4(new Vector3(value.Get<Vector2>().x, 0, value.Get<Vector2>().y));

            currentInput = skewedInput;
        }
        private void OnJump()
        {
            jumpButtonPressedTime = Time.time;
        }
    }
}