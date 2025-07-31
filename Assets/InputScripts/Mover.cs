using UnityEngine;

namespace TopDown.Movement
{

    [RequireComponent(typeof(CharacterController))]
    public class Mover : MonoBehaviour
    {
        [SerializeField] private float movementSpeed;
        protected CharacterController characterController;
        protected Vector3 currentInput;

        [Header("Õàðêè ïðûæêà")]
        [SerializeField] private float maxJumpTime;
        [SerializeField] private float maxJumpHeight;
        private float gravityForce = 9.8f;
        protected float jumpVelocity;
        protected Vector3 velocityDirection;

        private void Start()
        {
            characterController = GetComponent<CharacterController>();

            float maxHeightTime = maxJumpTime / 2;
            gravityForce = ((2 * maxJumpHeight) / Mathf.Pow(maxHeightTime, 2));
            jumpVelocity = (2 * maxJumpHeight) / maxHeightTime;
        }

        private void Update()
        {
            velocityDirection.x = currentInput.x * movementSpeed;
            velocityDirection.z = currentInput.z * movementSpeed;
            characterController.Move(velocityDirection * Time.deltaTime);
            GravityHandling();
        }

        private void CheckPlayerHeight()
        {
            if (transform.position.y <= -5)
            {
                Debug.Log("ÓÌÝÐ");
            }
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
