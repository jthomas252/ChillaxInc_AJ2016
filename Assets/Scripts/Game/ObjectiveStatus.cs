using UnityEngine;
using System.Collections;
using Relax.Utility;

namespace Relax.Game {
    public class ObjectiveStatus : MonoBehaviour {
        public delegate void ObjectiveUpdatedCallback();
        public event ObjectiveUpdatedCallback ObjectiveChange;
        public event ObjectiveUpdatedCallback ObjectiveSetup;

        public float ObjectiveCompleteAmount = 0.1f;
        public float ObjectiveIncompleteAmount = 0.35f;

        private string originalDescription; 
        public string description;
        public bool complete;

        private void Awake() {
            originalDescription = description; 
        }//Awake

        public void Setup() {
            if (ObjectiveSetup != null) ObjectiveSetup();
        }//Setup

        public void MarkComplete() {
            complete = true;
            if (ObjectiveChange != null) ObjectiveChange();
        }//MarkComplete

        public void MarkIncomplete() {
            complete = false;
            description = originalDescription; 
            if (ObjectiveChange != null) ObjectiveChange();
        }//MarkIncomplete

        public void ChangeDescription(string newText) {
            description = newText;
            if (ObjectiveChange != null) ObjectiveChange();
        }//ChangeDescription

        public void InspectObject() {
            if (complete) {
                Top.GAME.humanCharacter.IncreaseSatisfaction(ObjectiveCompleteAmount, true);
            } else {
                Top.GAME.humanCharacter.IncreaseAnger(ObjectiveIncompleteAmount, true);
            }
        }//InspectObject
    }//ObjectiveStatus
}//ObjectiveStatus
