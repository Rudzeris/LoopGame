using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.GameObjects.Attacks
{
    [RequireComponent(typeof(PlayerInput))]

    public class PlayerShooter : Shooter
    {
        private InputAction _fireAction;

        private void Awake()
        {
            var playerInput = GetComponent<PlayerInput>();
            _fireAction = playerInput.actions["Fire"];
        }

        private void OnEnable()
        {
            _fireAction.performed += OnFirePerformed;
        }

        private void OnDisable()
        {
            _fireAction.performed -= OnFirePerformed;
        }

        private void OnFirePerformed(InputAction.CallbackContext ctx)
        {
            Shoot();
        }

    }
}
