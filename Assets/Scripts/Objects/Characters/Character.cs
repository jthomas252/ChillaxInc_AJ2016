using UnityEngine;
using System.Collections;
using Relax.Utility; 
using Relax.Objects.Interactables;

namespace Relax.Objects.Characters {
    public class Character : MonoBehaviour {
        protected NavMeshAgent navAgent;
        protected Animator animator; 
        protected AudioSource[] audioSources;
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

            if (GetComponent<AudioSource>()) {
                audioSources = GetComponents<AudioSource>();
            } else {
                throw new MissingComponentException("No AudioSource on Character");
            }
        }//Start

        protected void Update() {
            if (interactTarget != null) {
                if (interacting) {
                    if (interactionTime > timeToInteract) {
                        interactTarget.Interact(interactionType);
                        interactTarget = null; 
                        interacting = false;
                        HideIndicator(); 
                        OnInteract();
                    } else {
                        interactionTime += Time.deltaTime; 
                    }
                } else {
                    if (Vector3.Distance(transform.position, interactTarget.transform.position) < interactRange) {
                        interacting = true; 
                        navAgent.path.ClearCorners();
                        OnStop(); 
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

        protected void PlaySound(AudioClip clip, int num = 0) {
            if (num < audioSources.Length) {
                if (audioSources[num].isPlaying) audioSources[num].Stop();
                audioSources[num].clip = clip;
                audioSources[num].Play();
            }
        }//PlaySound

        protected void StopSound(int num = 0) {
            if (num < audioSources.Length) {
                audioSources[num].Stop(); 
            }
        }

        protected virtual void OnInteract() {

        }//OnInteract

        protected virtual void OnStop() {

        }//OnStop
    }//Character
}//Relax