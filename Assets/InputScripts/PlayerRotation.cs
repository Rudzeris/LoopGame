using UnityEngine;
using UnityEngine.InputSystem;

namespace TopDown.Movement
{
    public class PlayerRotation : Rotator
    {
        private void OnLook(InputValue value)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(value.Get<Vector2>());
            LookAt(mousePosition);
        }
    }
}