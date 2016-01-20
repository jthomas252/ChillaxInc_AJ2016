using UnityEngine;
using System.Collections;
using Relax.Utility;

namespace Relax.Objects.Interactables {
    public class TV : InteractableObject {
        private void Start() {
            base.Start(); 
        }

        protected override void OnFirstButton() {
            Top.GAME.playerCharacter.SetInteractionTarget(this, InteractionType.Using, 2f, "TV");
        }//OnFirstButton

        public override void Interact() {
            Debug.Log("TV interacted with");
        }//Interact
    }//TV
}