using System.Collections;
using UnityEngine;



namespace CM_Player
{

    // handles player inputs
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour
    {

        private Animator anim;

        private CharacterController characterController;
        [SerializeField] private float movementSpeed = 3.5f;

        private bool canMove = true;
        private bool isDodgeRolling = false;



        // get character controller and animator
        private void Start()
        {

            characterController = GetComponent<CharacterController>();
            anim = GetComponentInChildren<Animator>();

        }



        // each frame: check for actions, check for movement, then move player
        private void Update()
        {

            if(canMove)
            {
                Vector2 inputDirection = GetMovementInput();

                if(Input.GetAxis("Dodge") >= 0.01f) DodgeRoll(inputDirection);

                Move(inputDirection);
            }

        }



        // gets movement input using currently active input method
        private Vector2 GetMovementInput()
        {

            Vector2 input = new Vector2
            (
                Input.GetAxisRaw("Horizontal"),
                Input.GetAxisRaw("Vertical")
            );

            input.Normalize();

            if(input.magnitude >= 0.01f) return input;

            return Vector2.zero;

        }



        // moves the player in the specified direction if they are not currently dodging
        public void Move(Vector2 input)
        {

            if(!isDodgeRolling && input != Vector2.zero)
            {
                Vector3 direction = new Vector3
                (
                    input.x,
                    input.y,
                    0f
                );

                characterController.Move(direction * movementSpeed * Time.deltaTime);
            }

            anim.SetFloat("HorizontalVelocity", input.x);
            anim.SetFloat("VerticalVelocity", Mathf.Abs(input.y));

        }



        // dodge rolls in the specified direction
        public void DodgeRoll(Vector2 input)
        {

            if(!isDodgeRolling && input != Vector2.zero)
            {
                Vector3 direction = new Vector3
                (
                    input.x,
                    input.y,
                    0f
                );

                StartCoroutine(DodgeRollRoutine(direction));
            }

        }



        // dodgeroll routine
        private IEnumerator DodgeRollRoutine(Vector3 direction)
        {

            isDodgeRolling = true;

            anim.SetTrigger("Dodge");

            const float dodgeTime = 8f/12f; // frames / fps
            float timeElapsed = 0f;

            Vector3 destination = direction * movementSpeed;

            while(timeElapsed < dodgeTime)
            {
                characterController.Move(destination * (Time.deltaTime / dodgeTime));

                timeElapsed += Time.deltaTime;

                yield return null;
            }

            isDodgeRolling = false;

        }



        // teleports player to destination. does a small particle burst if doParticleEffect is true
        public void TeleportTo(Vector2 destination, bool doParticleEffect)
        {

            Vector3 destination3D = new Vector3
            (
                destination.x,
                0f,
                destination.y
            );

            transform.position = destination3D;

            // if(doParticleEffect) TriggerParticleEffect()

        }


    }

}
