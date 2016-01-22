using UnityEngine;
using System.Collections;

namespace Relax.Game {
    public class ObjectiveStatus : MonoBehaviour {
        public delegate void ObjectiveUpdatedCallback(); 
        public event ObjectiveUpdatedCallback ObjectiveChangeCallback; 
        public delegate float ObjectiveInspectionCallback(); 
        public event ObjectiveInspectionCallback ObjectiveInspected; 
        
        public string description;
        public bool complete;

        public void MarkComplete() {
            complete = true; 
            if (ObjectiveChangeCallback != null) ObjectiveChangeCallback(); 
        }//MarkComplete

        public void MarkIncomplete() {
            complete = false;
            if (ObjectiveChangeCallback != null) ObjectiveChangeCallback(); 
        }//MarkIncomplete

        public void ChangeDescription() {
            complete = true;
            if (ObjectiveChangeCallback != null) ObjectiveChangeCallback(); 
        }//ChangeDescription

        public float InspectObject() {
            if (ObjectiveInspected != null) {
                return ObjectiveInspected();
            } else {
                return 0f; 
            }
        }//InspectObject
    }//ObjectiveStatus
}//ObjectiveStatus
