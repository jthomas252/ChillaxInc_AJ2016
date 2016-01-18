using UnityEngine;
using System.Collections;
using Relax.Utility;

namespace Relax.Objects.Interactables {
    public class TV : InteractableObject {
        private void Start() {
            base.Start(); 
        }

        protected override void OnFirstButton() {
            Debug.Log("My button was pressed");
            Top.GAME.playerCharacter.SetInteractionTarget(this);
        }//OnFirstButton

        public override void Interact() {
            Debug.Log("I was interacted with!");
        }//Interact
    }//TV
}