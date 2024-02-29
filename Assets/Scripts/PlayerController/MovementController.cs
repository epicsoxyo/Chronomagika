using System.Collections;
using UnityEngine;



namespace CM_Player
{

    // controls the player movement. called from the player controller.
    public class MovementController : MonoBehaviour
    {

        private Animator anim;

        [SerializeField] private float movementSpeed = 3.5f;
        private float movementAngle = Mathf.Tan(Mathf.Deg2Rad * 30f);
        private float roomSize = 15f;
        private bool isDodgeRolling = false;



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
                destination.y,
                destination.y / Mathf.Tan(Mathf.Deg2Rad * 30f)
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
                    input.y / movementAngle
                );

                transform.position += direction * movementSpeed * Time.deltaTime;
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
                    input.y / Mathf.Tan(Mathf.Deg2Rad * 30f)
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

            // float xMax = roomSize;
            // float yMax = (roomSize * Mathf.Sin(Mathf.Deg2Rad * 30f)) / 2;
            // float zMax = (roomSize * Mathf.Cos(Mathf.Deg2Rad * 30f)) / 2;

            // Debug.Log(destination);

            // destination = new Vector3
            // (
            //     Mathf.Clamp(destination.x, -xMax, xMax),
            //     Mathf.Clamp(destination.y, -yMax, yMax),
            //     Mathf.Clamp(destination.z, -zMax, zMax)
            // );

            // destination = transform.InverseTransformPoint(destination);

            // Debug.Log(destination);

            while(timeElapsed < dodgeTime)
            {
                transform.position += destination * (Time.deltaTime/dodgeTime);

                timeElapsed += Time.deltaTime;

                yield return null;
            }

            isDodgeRolling = false;

        }

    }

}