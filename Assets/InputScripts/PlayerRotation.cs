using UnityEngine;
using UnityEngine.InputSystem;

namespace TopDown.Movement
{
    public class PlayerRotation : Rotator
    {
        public GameObject plane;
        public LayerMask layerMask;
        private void OnLook(InputValue value)
        {
            Ray ray = Camera.main.ScreenPointToRay(value.Get<Vector2>());
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                if (hit.collider.gameObject == plane)
                {
                    Vector3 hitPoint = hit.point;
                    LookAt(hitPoint);
                }
            }
        }
    }
}