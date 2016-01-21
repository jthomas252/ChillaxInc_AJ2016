using UnityEngine;
using System.Collections;
using Relax.Utility;

namespace Relax.Objects.Interactables {
    public class TV : InteractableObject {
        public Sprite onSprite;
        public Sprite offSprite; 

        private void Start() {
            base.Start(); 
        }

        protected override void OnFirstButton() {
            Top.GAME.playerCharacter.SetInteractionTarget(this, InteractionType.Using, 2f, "TV");
        }//OnFirstButton

        public override void Interact(InteractionType type = InteractionType.Primary) {
            Debug.Log("TV interacted with");
        }//Interact
    }//TV
}