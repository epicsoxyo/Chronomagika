using System.Collections;
using UnityEngine;



namespace CM_Player
{

    // handles player inputs
    public class PlayerController : MonoBehaviour
    {

        [Header ("Movement")]
        private MovementController movementController;
        private bool canMove = true;
        public bool CanMove // called by actions that prevent movement
        {
            get{return canMove;}
            set{canMove = value;}
        }
        private bool isDodgeRolling = false;



        // get components for mouse input, movement controller, and action controller
        private void Start()
        {

            movementController = GetComponentInChildren<MovementController>();

        }



        // each frame: check for actions, check for movement, then move player
        private void Update()
        {

            HandleActions();

            if(canMove)
            {
                Vector2 inputDirection = GetMovementInput();

                if(Input.GetAxis("Dodge") >= 0.01f) movementController.DodgeRoll(inputDirection);

                movementController.Move(inputDirection);
            }


        }



        // detect actions
        private void HandleActions()
        {



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

    }

}
