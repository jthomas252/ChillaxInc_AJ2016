using UnityEngine;
using System.Collections;
using Relax.Utility; 

namespace Relax.Objects.Characters {
    public class Character : MonoBehaviour {
        protected NavMeshAgent navAgent;
        protected Animator animator; 

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
    }//Character
}