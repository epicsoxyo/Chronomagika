using System.Collections.Generic;
using System.Collections;
using UnityEngine;



namespace CM_Player
{

    // handles player inputs
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {

        private Animator anim;

        private CharacterController controller;
        [SerializeField] private float movementSpeed = 3.5f;

        private bool isDodgeRolling = false;

        private bool canTimeTravel = true;
        [SerializeField] private float timeTravelCooldownTime;
        private CooldownTimer cooldownTimer;



        // get character controller and animator
        private void Start()
        {

            controller = GetComponent<CharacterController>();

            anim = GetComponentInChildren<Animator>();

            cooldownTimer = FindObjectOfType<CooldownTimer>();

        }



        // each frame: check for actions, check for movement, then move player
        private void Update()
        {

            Vector2 input = GetMovementInput();

            if(EnemyManager.instance != null)
            {
                EnemyManager.instance.IsUndoing = (Input.GetAxis("Undo") >= 0.01f && canTimeTravel);

                if(!EnemyManager.instance.IsUndoing) StartCoroutine("TimeTravelCooldown");
            }

            if(PuzzleManager.instance != null)
            {
                PuzzleManager.instance.IsUndoing = (Input.GetAxis("Undo") >= 0.01f && canTimeTravel);

                if(!PuzzleManager.instance.IsUndoing) StartCoroutine("TimeTravelCooldown");
            }

            if(Input.GetButtonDown("Dodge")) DodgeRoll(input);
            if(!isDodgeRolling) Move(input);

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
        public void Move(Vector2 direction)
        {

            Vector3 velocity = movementSpeed * new Vector3
            (
                direction.x,
                direction.y,
                0f
            );

            controller.Move(velocity * Time.deltaTime);

            anim.SetFloat("HorizontalVelocity", direction.x);
            anim.SetFloat("VerticalVelocity", Mathf.Abs(direction.y));

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

            controller.excludeLayers = LayerMask.GetMask("Enemy"); // make invincible

            const float dodgeTime = 8f/12f; // frames / fps
            float timeElapsed = 0f;

            Vector3 destination = direction * movementSpeed;

            while(timeElapsed < dodgeTime)
            {
                controller.Move(destination * (Time.deltaTime / dodgeTime));

                timeElapsed += Time.deltaTime;

                yield return null;
            }

            controller.excludeLayers = LayerMask.GetMask("Nothing"); // remove invincibility

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



        private IEnumerator TimeTravelCooldown()
        {

            canTimeTravel = false;

            cooldownTimer.ToggleVisibility(true);

            float timeElapsed = 0f;

            while(timeElapsed < timeTravelCooldownTime)
            {

                cooldownTimer.UpdateCooldownTimer(timeTravelCooldownTime, timeElapsed);

                timeElapsed += Time.deltaTime;
                yield return null;

            }

            cooldownTimer.ToggleVisibility(false);

            canTimeTravel = true;

        }


    }

}
