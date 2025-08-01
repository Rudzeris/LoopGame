using System;
using UnityEngine;

namespace TopDown.Movement
{

    [RequireComponent(typeof(CharacterController))]
    public class Mover : MonoBehaviour
    {
        [SerializeField] private float movementSpeed;
        protected CharacterController characterController;
        protected Vector3 currentInput;
        private Vector3 spawnPosition;

        [Header("Харки прыжка")]
        [SerializeField] private float maxJumpTime;
        [SerializeField] private float maxJumpHeight;
        [SerializeField] protected float JumpButtonGracePeriod;
        private float gravityForce = 9.8f;
        private float tempGravityForce;
        protected float jumpVelocity;
        public Vector3 velocityDirection;
        protected float? lastGroundedTime;
        protected float? jumpButtonPressedTime;

        private void OnEnable()
        {
            Messenger.AddListener(GameEvent.PLAYER_STAY_CRITICAL_ZONE, OnPlayerStayCriticalZone);
        }

        private void OnPlayerStayCriticalZone()
        {
            characterController.enabled = false;
        }

        private void Start()
        {
            characterController = GetComponent<CharacterController>();
            spawnPosition = transform.position;

            float maxHeightTime = maxJumpTime / 2;
            gravityForce = ((2 * maxJumpHeight) / Mathf.Pow(maxHeightTime, 2));
            jumpVelocity = (2 * maxJumpHeight) / maxHeightTime;
        }

        private void Update()
        {
            UpdateLastGroundedTime();
            BufferingJump();
            HorizontalMovement();
            GravityHandling();
        }

        private void UpdateLastGroundedTime()
        {
            if (characterController.isGrounded)
            {
                lastGroundedTime = Time.time;
            }
        }

        private void BufferingJump()
        {
            if (Time.time - lastGroundedTime <= JumpButtonGracePeriod)
            {
                if (Time.time - jumpButtonPressedTime <= JumpButtonGracePeriod)
                {
                    velocityDirection.y = jumpVelocity;
                    jumpButtonPressedTime = null;
                    lastGroundedTime = null;
                }
            }
        }

        private void HorizontalMovement()
        {
            velocityDirection.x = currentInput.x * movementSpeed;
            velocityDirection.z = currentInput.z * movementSpeed * 1.5f;
            if(characterController.enabled)
            characterController.Move(velocityDirection * Time.deltaTime);
        }

        private void GravityHandling()
        {
            if (!characterController.isGrounded)
            {
                velocityDirection.y -= gravityForce * Time.deltaTime;
            }
            else
            {
                velocityDirection.y = -0.5f;
            }
        }
    }

}
