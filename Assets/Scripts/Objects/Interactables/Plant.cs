using UnityEngine;
using System.Collections;
using Relax.Utility;
using Relax.Game;

namespace Relax.Objects.Interactables {
    public class Plant : InteractableObject {

        private new void Awake() {
            base.Awake();
            if (GetComponent<ObjectiveStatus>()) {
                GetComponent<ObjectiveStatus>().ObjectiveSetup += OnObjectiveSetup; 
            }
        }//Awake

        private void OnObjectiveSetup() {
            status = ObjectStatus.Broken; 
            indicator.ShowIcon("noWater");
        }//OnObjectiveSetup

        protected override void OnFirstButton() {
            Top.GAME.playerCharacter.SetInteractionTarget(this, InteractionType.Primary, 1f);
        }//OnFirstButton

        public override bool CanInteract(InteractableObject.InteractionType type = InteractionType.Primary) {
            if (type == InteractionType.Primary) {
                if (Top.GAME.playerCharacter.IsHoldingObject()) {
                    return true;
                } else {
                    Top.GAME.SetMessageText("You have nothing to use!", Color.red);
                    Top.GAME.PlayGlobalSound(Top.GAME.GetRandomSound("robotError"));
                    return false;
                }
            }
            return true;
        }//CanInteract

        public override void Interact(InteractionType type = InteractionType.Primary) {
            bool result = false; 
            switch (Top.GAME.playerCharacter.GetHeldObject().function) {
                case Pickups.PickupObject.PickupFunction.FightFire:
                    if (GetComponent<Flammable>()) GetComponent<Flammable>().Ignite(true);
                    result = true; 
                    break;

                case Pickups.PickupObject.PickupFunction.BatteryLive: 
                    result = true;
                    if (status == ObjectStatus.Broken) {
                        status = ObjectStatus.On;
                        indicator.Hide();
                        if (GetComponent<ObjectiveStatus>()) {
                            GetComponent<ObjectiveStatus>().MarkComplete();
                        }
                    }
                    break;

                default:
                    Top.GAME.SetMessageText("That didn't do anything.", Color.red);
                    break;
            }

            Top.GAME.playerCharacter.GetHeldObject().OnUse(result);
            base.Interact();
        }//Interact

        private void OnDestroy() {
            if (GetComponent<ObjectiveStatus>()) {
                GetComponent<ObjectiveStatus>().ObjectiveSetup -= OnObjectiveSetup;
            }
        }//OnDisable
    }//Plant
}//Relax