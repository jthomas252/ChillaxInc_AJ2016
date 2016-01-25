using UnityEngine;
using System.Collections;
using Relax.Utility;
using Relax.Game;

namespace Relax.Objects.Interactables {
    public class FoodDispenser : InteractableObject {
        public int foodToLoad = 3;
        private int loadedFood = 0;

        private new void Awake() {
            base.Awake();
            if (GetComponent<ObjectiveStatus>()) {
                GetComponent<ObjectiveStatus>().ObjectiveSetup += OnObjectiveSetup;
            }
        }//Awake

        private void OnObjectiveSetup() {
            status = ObjectStatus.Broken;
            indicator.ShowIcon("noFood");
            loadedFood = 0;
        }//OnObjectiveSetup

        //Load
        protected override void OnFirstButton() {
            Top.GAME.playerCharacter.SetInteractionTarget(this, InteractionType.Primary, 1.5f);
        }//OnFirstButton

        protected override void OnSecondButton() {
            Top.GAME.playerCharacter.SetInteractionTarget(this, InteractionType.Secondary, 1.5f);
        }//OnSecondButton

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
            if (type == InteractionType.Primary) {
                bool result = false;
                switch (Top.GAME.playerCharacter.GetHeldObject().function) {
                    //Battery 
                    case Pickups.PickupObject.PickupFunction.FoodCart:
                        ++loadedFood;
                        result = true;
                        if (GetComponent<ObjectiveStatus>()) {
                            if (loadedFood >= foodToLoad) {
                                indicator.Hide();
                                GetComponent<ObjectiveStatus>().MarkComplete();
                            }

                            GetComponent<ObjectiveStatus>().ChangeDescription("Refill the food dispenser (" + loadedFood + "/" + foodToLoad + ")");
                        }
                        break;

                    case Pickups.PickupObject.PickupFunction.Trash:
                    case Pickups.PickupObject.PickupFunction.BatteryLive:
                    case Pickups.PickupObject.PickupFunction.LightBulb:
                    case Pickups.PickupObject.PickupFunction.BatteryDead:
                    case Pickups.PickupObject.PickupFunction.FightFire:
                        result = true;
                        Top.GAME.SetMessageText("It didn't like that.", Color.red);
                        Top.GAME.PlayGlobalSound(Top.GAME.GetRandomSound("robotError"));
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
    }//Shower
}//Relax