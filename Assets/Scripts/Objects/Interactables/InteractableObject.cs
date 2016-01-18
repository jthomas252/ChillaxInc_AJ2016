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

        [System.Serializable]
        public struct ObjectStateInfo {
            public ObjectStatus status;
            public string descriptionText;
        }

        public ObjectStatus startingState = ObjectStatus.Off; 
        public ObjectStateInfo[] stateInfo; 
        public bool flammable = true; 

        private ObjectTooltipInfo tooltipInfo; 
        private ObjectiveStatus objectiveStatus; 

        private void Start() {

        }//Start

        private void Update() {

        }//Update

        private void OnDrawGizmos() {
            BoxCollider box = GetComponent<BoxCollider>(); 
            Gizmos.color = new Color(0f,0.5f,0f,0.5f); 
            Gizmos.DrawCube(box.transform.position, box.size);
        }//OnDrawGizmos
    }//InteractableObject
}