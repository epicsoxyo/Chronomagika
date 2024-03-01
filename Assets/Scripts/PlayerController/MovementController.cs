using System.Collections;
using UnityEngine;



namespace CM_Player
{

    // controls the player movement. called from the player controller.
    [RequireComponent(typeof(CharacterController))]
    public class MovementController : MonoBehaviour
    {

        private Animator anim;
        private CharacterController characterController;

        [SerializeField] private float movementSpeed = 3.5f;
        // private float movementAngle = Mathf.Tan(Mathf.Deg2Rad * 30f);
        private bool isDodgeRolling = false;



        private void Awake()
        {
            
            characterController = GetComponent<CharacterController>();

        }



        private void Start()
        {

            anim = GetComponentInChildren<Animator>();

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

                StartCoroutine(DoDodgeRoll(direction));
            }

        }



        private IEnumerator DoDodgeRoll(Vector3 direction)
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



    }

}