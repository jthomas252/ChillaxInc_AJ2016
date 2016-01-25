using UnityEngine;
using System.Collections;
using Relax.Utility;
using Relax.Game;

namespace Relax.Objects.Interactables {
    public class Lamp : InteractableObject {

        private new void Awake() {
            base.Awake();
        }//Awake

        private void Start() {
            if (status == ObjectStatus.Broken) {
                indicator.ShowIcon("broken");
            }

            if (GetComponent<ObjectiveStatus>()) {
                GetComponent<ObjectiveStatus>().ObjectiveSetup += OnObjectiveSetup;
            }
        }//Start

        private void OnObjectiveSetup() {
            status = ObjectStatus.Broken;
            indicator.ShowIcon("broken");
        }//OnObjectiveSetup

        //Use object
        protected override void OnFirstButton() {
            Top.GAME.playerCharacter.SetInteractionTarget(this, InteractionType.Primary, 0.5f);
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
            return false;
        }//CanInteract

        public override void Interact(InteractionType type = InteractionType.Primary) {
            if (type == InteractionType.Primary) {
                bool result = false;
                switch (Top.GAME.playerCharacter.GetHeldObject().function) {
                    //Light bulb
                    case Pickups.PickupObject.PickupFunction.LightBulb:
                        result = true;
                        status = ObjectStatus.Off;
                        indicator.Hide();
                        if (GetComponent<ObjectiveStatus>()) {
                            GetComponent<ObjectiveStatus>().MarkComplete();
                        }
                        break;

                    //Water / Extenguisher
                    case Pickups.PickupObject.PickupFunction.FightFire:
                        result = true;
                        if (GetComponent<Flammable>()) GetComponent<Flammable>().Ignite();
                        break;

                    default:
                        Top.GAME.SetMessageText("That didn't do any good", Color.red);
                        Top.GAME.PlayGlobalSound(Top.GAME.GetRandomSound("robotError"));
                        break;
                }
                Top.GAME.playerCharacter.GetHeldObject().OnUse(result);
            }
            base.Interact();
        }//Interact

        public override void OnIgnite() {
            status = ObjectStatus.Broken;
            indicator.ShowIcon("broken");
        }//OnIgnite
    }//TV
}//Relax