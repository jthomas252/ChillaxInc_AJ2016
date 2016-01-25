using UnityEngine;
using System.Collections;
using Relax.Utility;

namespace Relax.Objects.Interactables {
    public class Shower : InteractableObject {
        private new void Awake() {
            base.Awake();
        }//Awake

        protected override void OnFirstButton() {
            Top.GAME.playerCharacter.SetInteractionTarget(this, InteractionType.Primary, 2f);
        }//OnFirstButton

        public override void Interact(InteractionType type = InteractionType.Primary) {
            Fire[] fires = Top.GAME.playerCharacter.GetComponentsInChildren<Fire>();
            for (int i = 0; i < fires.Length; ++i) {
                fires[i].Contain();
            }
            Top.GAME.PlayGlobalSound(Top.GAME.GetRandomSound("robotHappy")); 
            base.Interact();
        }//Interact
    }//Shower
}//Relax