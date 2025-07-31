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
            Vector3 playerInput = new Vector3(value.Get<Vector2>().x, 0, value.Get<Vector2>().y);
            currentInput = playerInput;
        }
        private void OnJump()
        {
            Debug.Log("JUMP " + jumpVelocity);
            velocityDirection.y = jumpVelocity;
        }
    }
}