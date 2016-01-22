using UnityEngine;
using System.Collections;
using Relax.Utility;
using Relax.Objects.Scenery;

namespace Relax.Objects.Interactables {
    public class RobotSpawner : InteractableObject {

        private new void Start() {
            base.Start();
        }//Start

        protected override void OnFirstButton() {
            Top.GAME.playerCharacter.SetInteractionTarget(this, InteractionType.Primary, 2f);
        }//OnFirstButton

        public override void Interact(InteractionType type = InteractionType.Primary) {
            Debug.Log("TV interacted with");
        }//Interact
    }//TV
}