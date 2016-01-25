using UnityEngine;
using System.Collections;
using Relax.Utility;

namespace Relax.Objects.Interactables {
    public class Recycle : InteractableObject {

        private new void Awake() {
            base.Awake();
        }//Awake

        protected override void OnFirstButton() {
            Top.GAME.playerCharacter.SetInteractionTarget(this, InteractionType.Primary, 1f);
        }//OnFirstButton

        public override bool CanInteract(InteractableObject.InteractionType type = InteractionType.Primary) {
            if (type == InteractionType.Primary) {
                if (Top.GAME.playerCharacter.IsHoldingObject()) {
                    return true;
                } else {
                    Top.GAME.SetMessageText("You have nothing to throw away!", Color.red);
                    Top.GAME.PlayGlobalSound(Top.GAME.GetRandomSound("robotError"));
                    return false;
                }
            }
            return true;
        }//CanInteract

        public override void Interact(InteractionType type = InteractionType.Primary) {
            switch (Top.GAME.playerCharacter.GetHeldObject().function) {
                case Pickups.PickupObject.PickupFunction.Trash:
                    if (GetComponent<Flammable>()) GetComponent<Flammable>().Ignite(true);
                    break;

                default:
                    break;
            }
            Top.GAME.playerCharacter.DropHeldObject(true);
            base.Interact();
        }//Interact
    }//Recycle
}//Relax