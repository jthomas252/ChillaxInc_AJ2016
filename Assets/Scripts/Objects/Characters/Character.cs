using UnityEngine;
using System.Collections;
using Relax.Utility; 

namespace Relax.Objects.Characters {
    public class Character : MonoBehaviour {
        private NavMeshAgent navAgent;
        private Animator animator; 

        void Start() {
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

        void Update() {
            if (Input.GetMouseButtonDown(0)) {
                Ray screenRay = Camera.main.ScreenPointToRay(Input.mousePosition);

                RaycastHit hit;
                if (Physics.Raycast(screenRay, out hit)) {
                    navAgent.SetDestination(hit.point);
                    Debug.DrawRay(hit.point, new Vector3(0f,1f), Color.red, 5f);
                }
            }

            Debug.Log(navAgent.desiredVelocity);
            if (Mathf.Abs(navAgent.desiredVelocity.x) > Mathf.Abs(navAgent.desiredVelocity.z)) {
                animator.SetFloat("MoveX", navAgent.desiredVelocity.x);
                animator.SetFloat("MoveZ", 0f);
            } else {
                animator.SetFloat("MoveX", 0f);
                animator.SetFloat("MoveZ", navAgent.desiredVelocity.z);
            }
        }//Update
    }//Character
}