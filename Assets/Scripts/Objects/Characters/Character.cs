using UnityEngine;
using System.Collections;
using Relax.Utility; 
using Relax.Objects.Interactables;

namespace Relax.Objects.Characters {
    public class Character : MonoBehaviour {
        protected NavMeshAgent navAgent;
        protected Animator animator; 
        public float interactRange = 2.5f; 
        public InteractableObject interactTarget; 

        //Display indicator -- Probably move to a seperate class? 
        public SpriteRenderer indicator; 
        public SpriteRenderer indicatorIcon; 

        private bool interacting; 
        private float interactionTime;
        private float timeToInteract;
        private InteractableObject.InteractionType interactionType; 
        private string indicatorType;

        protected void Start() {
            if (GetComponent<NavMeshAgent>()) {
                navAgent = GetComponent<NavMeshAgent>();
                navAgent.updateRotation = false; 
            } else {
                throw new MissingComponentException("No NavMesh Component on Character"); 
            }

            if (GetComponentInChildren<Animator>()) {
                animator = GetComponentInChildren<Animator>();
            } else {
                throw new MissingComponentException("No Animator Component in Children of Character");
            }
        }//Start

        protected void Update() {
            if (interactTarget != null) {
                if (interacting) {
                    if (interactionTime > timeToInteract) {
                        interactTarget.Interact();
                        interactTarget = null; 
                        interacting = false;
                        HideIndicator(); 
                    } else {
                        interactionTime += Time.deltaTime; 
                    }
                } else {
                    if (Vector3.Distance(transform.position, interactTarget.transform.position) < interactRange) {
                        interacting = true; 
                        navAgent.path.ClearCorners();
                        ShowIndicator(indicatorType); 
                    }
                }
            }
        }//Update

        protected void UpdateAnimation() {
            //Change this to add a bool of some form to allow it to go back to idle state
            if (Mathf.Abs(navAgent.desiredVelocity.x) > Mathf.Abs(navAgent.desiredVelocity.z)) {
                animator.SetFloat("MoveX", navAgent.desiredVelocity.x);
                animator.SetFloat("MoveZ", 0f);
            } else {
                animator.SetFloat("MoveX", 0f);
                animator.SetFloat("MoveZ", navAgent.desiredVelocity.z);
            }
        }//UpdateAnimation

        public void SetInteractionTarget(InteractableObject obj, InteractableObject.InteractionType type = InteractableObject.InteractionType.Using, float timeToComplete = 0f, string indicator = "default") {
            interactTarget = obj;
            interactionType = type; 
            navAgent.SetDestination(obj.transform.position);
            interacting = false; 
            interactionTime = 0f; 
            indicatorType = indicator;
            timeToInteract = timeToComplete;
        }//SetInteractionTarget

        protected void ShowIndicator(string type = "default") {
            indicator.gameObject.SetActive(true);
            indicatorIcon.sprite = Top.GAME.GetIndicatorSprite(type); 
        }//ShowIndicator

        protected void HideIndicator() {
            indicator.gameObject.SetActive(false);
        }//HideIndicator
    }//Character
}