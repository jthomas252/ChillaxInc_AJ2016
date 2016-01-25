using UnityEngine;
using System.Collections;
using Relax.Utility;
using Relax.Objects.Scenery;
using Relax.Game;

namespace Relax.Objects.Interactables {
    public class RobotSpawner : InteractableObject {
        public int batteriesToLoad = 2;
        private int loadedBatteries = 0; 

        private new void Awake() {
            base.Awake();
            if (GetComponent<ObjectiveStatus>()) {
                GetComponent<ObjectiveStatus>().ObjectiveSetup += OnObjectiveSetup;
            }
        }//Awake

        private void OnObjectiveSetup() {
            status = ObjectStatus.Broken;
            indicator.ShowIcon("noPower");
            loadedBatteries = 0;
        }//OnObjectiveSetup

        protected override void OnFirstButton() {
            Debug.Log("R1");
            Top.GAME.playerCharacter.SetInteractionTarget(this, InteractionType.Primary, 1.5f);
        }//OnFirstButton

        protected override void OnSecondButton() {
            Top.GAME.playerCharacter.SetInteractionTarget(this, InteractionType.Secondary, 1f);
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
                    case Pickups.PickupObject.PickupFunction.BatteryLive:
                        ++loadedBatteries;
                        result = true;
                        if (GetComponent<ObjectiveStatus>()) {
                            if (loadedBatteries >= batteriesToLoad) {
                                indicator.Hide();
                                GetComponent<ObjectiveStatus>().MarkComplete();
                            }

                            GetComponent<ObjectiveStatus>().ChangeDescription("Put a new battery in the Robo-Charger (" + loadedBatteries + "/2)");
                        }
                        break;

                    case Pickups.PickupObject.PickupFunction.BatteryDead:
                        Top.GAME.SetMessageText("The machine didn't seem to do anything.", Color.red);
                        Top.GAME.PlayGlobalSound(Top.GAME.GetRandomSound("robotError"));
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
            } else {
                Top.GAME.PlayGlobalSound(Top.GAME.GetRandomSound("robotHappy"));
            }
            base.Interact();
        }//Interact
    }//RobotSpawner
}//Relax