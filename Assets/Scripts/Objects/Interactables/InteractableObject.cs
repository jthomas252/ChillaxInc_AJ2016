using UnityEngine;
using System.Collections;
using Relax.Game;
using Relax.Utility;

namespace Relax.Objects.Interactables {
    public class InteractableObject : MonoBehaviour {
        public enum ObjectStatus {
            Broken,
            Off,
            On
        }

        public enum InteractionType {
            Primary,
            Secondary
        }

        protected ObjectTooltipInfo tooltipInfo;
        protected ObjectiveStatus objectiveStatus;
        public Indicator indicator;

        protected void Start() {
            if (GetComponent<ObjectTooltipInfo>()) {
                tooltipInfo = GetComponent<ObjectTooltipInfo>();
            } else {
                throw new MissingComponentException("No tooltip info on object");
            }

            if (GetComponent<Indicator>()) {
                indicator = GetComponent<Indicator>();
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

        public virtual void Interact(InteractionType type = InteractionType.Primary) {
            Debug.Log("BaseInteract");
        }//Interact

        public virtual bool CanInteract(InteractionType type = InteractionType.Primary) {
            return true;
        }//CanInteract

        public virtual void OnIgnite() {

        }//OnIgnite

        private void OnDrawGizmos() {
            BoxCollider box = GetComponent<BoxCollider>();
            Gizmos.color = new Color(0f, 1f, 0f, 0.5f);
            Gizmos.DrawCube(box.transform.position, box.size);
        }//OnDrawGizmos
    }//InteractableObject
}//Relax