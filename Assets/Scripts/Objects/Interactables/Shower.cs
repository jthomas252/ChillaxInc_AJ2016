using UnityEngine;
using System.Collections;
using Relax.Utility;

namespace Relax.Objects.Interactables {
    public class Shower : InteractableObject {
        private new void Start() {
            base.Start();
        }//Start

        protected override void OnFirstButton() {
            Top.GAME.playerCharacter.SetInteractionTarget(this, InteractionType.Primary, 2f);
        }//OnFirstButton

        public override void Interact(InteractionType type = InteractionType.Primary) {
            Debug.Log("TV interacted with");
        }//Interact
    }//Shower
}//Relax