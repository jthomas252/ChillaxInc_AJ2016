using UnityEngine;
using System.Collections;
using Relax.Game; 

namespace Relax.Objects.Interactables {
    public class InteractableObject : MonoBehaviour {
        public enum ObjectStatus {
            OnFire,
            Broken,
            Off,
            On
        }

        public enum InteractionType {
            Primary,
            Secondary,
            Using,
            Fixing,
            FightingFire
        }

        protected ObjectTooltipInfo tooltipInfo; 
        protected ObjectiveStatus objectiveStatus; 

        protected void Start() {
            if (GetComponent<ObjectTooltipInfo>()) {
                tooltipInfo = GetComponent<ObjectTooltipInfo>(); 
            } else {
                throw new MissingComponentException("No tooltip info on object");
            }

            tooltipInfo.FirstButtonCallback += OnFirstButton; 
            tooltipInfo.SecondButtonCallback += OnSecondButton; 
        }//Start

        protected virtual void OnFirstButton() {
            Debug.Log("Base1stButton");
        }//OnFirstButton

        protected virtual void OnSecondButton() {
            Debug.Log("Base2ndButton");
        }//OnSecondButton

        public virtual void Interact() {
            Debug.Log("BaseInteract"); 
        }//Interact

        private void OnDrawGizmos() {
            BoxCollider box = GetComponent<BoxCollider>(); 
            Gizmos.color = new Color(0f,1f,0f,0.5f); 
            Gizmos.DrawCube(box.transform.position, box.size);
        }//OnDrawGizmos
    }//InteractableObject
}