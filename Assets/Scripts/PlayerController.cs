using UnityEngine;
using UnityEngine.InputSystem;

namespace DapperDino.InputSystemTutorials
{
    public class PlayerController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Animator animator = null;
        [SerializeField] private PlayerInput playerInput = null;
        [SerializeField] private CharacterController controller = null;

        [Header("Settings")]
        [SerializeField] private float movementSpeed = 5f;

        private Vector3 inputMovement;

        public PlayerInput PlayerInput => playerInput;

        private void Update()
        {
            var finalMovement = inputMovement;

            finalMovement *= movementSpeed * Time.deltaTime;

            controller.Move(finalMovement);

            Vector3 velocity = controller.velocity;
            velocity.y = 0f;

            if (velocity.magnitude > 0.2f)
            {
                transform.rotation = Quaternion.LookRotation(finalMovement);
            }

            animator.SetBool("IsWalking", velocity.magnitude > 0.2f);
        }

        public void Move(InputAction.CallbackContext ctx)
        {
            var inputValue = ctx.ReadValue<Vector2>();
            inputMovement = new Vector3(inputValue.x, 0f, inputValue.y);
        }

        public void Jump(InputAction.CallbackContext ctx)
        {
            if (!ctx.performed) { return; }

            animator.SetTrigger("Jump");
        }
    }
}
