using UnityEngine;
using System.Collections;
using Relax.Utility;

namespace Relax.Objects.Interactables {
    public class Trashcan : InteractableObject {
        public Animator anim; 
        public AudioSource audioSource; 

        private new void Awake() {
            base.Awake();

            if (GetComponent<Animator>()) {
                anim = GetComponent<Animator>();
            }

            if (GetComponent<AudioSource>()) {
                audioSource = GetComponent<AudioSource>();
            }
        }//Start

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
                case Pickups.PickupObject.PickupFunction.BatteryDead:
                    if (GetComponent<Flammable>()) GetComponent<Flammable>().Ignite(true);
                    break;

                case Pickups.PickupObject.PickupFunction.BatteryLive:
                    SSJ();
                    break;

                default:
                    break;
            }
            Top.GAME.playerCharacter.DropHeldObject(true);
            base.Interact(); 
        }//Interact

        private void SSJ() {
            Top.GAME.ScreenShake(240);
            if (anim != null) {
                anim.SetBool("SSJ", true);
            } 
            if (audioSource != null) {
                audioSource.clip = Top.GAME.GetRandomSound("ssj_loop");
                audioSource.loop = true; 
                audioSource.Play(); 
            }
            Top.GAME.playerCharacter.SuperMode(); 
        }//SSJ
    }//Trashcan
}//Relax