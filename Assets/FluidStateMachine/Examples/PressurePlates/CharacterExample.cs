using UnityEngine;

namespace CleverCrow.FluidStateMachine.Examples {
    [RequireComponent(typeof(CharacterController))]
    public class CharacterExample : MonoBehaviour {
        CharacterController characterController;

        public float speed = 6.0f;
        public float gravity = 20.0f;

        private Vector3 moveDirection = Vector3.zero;

        void Start () {
            characterController = GetComponent<CharacterController>();
        }

        void Update () {
            if (characterController.isGrounded) {
                moveDirection = new Vector3(Input.GetAxis("Horizontal") * -1, 0.0f, Input.GetAxis("Vertical") * -1);
                moveDirection *= speed;
            }

            moveDirection.y -= gravity * Time.deltaTime;

            characterController.Move(moveDirection * Time.deltaTime);
        }
    }
}