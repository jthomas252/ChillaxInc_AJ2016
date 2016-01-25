using UnityEngine;
using System.Collections;
using Relax.Utility;
using Relax.Game;

namespace Relax.Objects.Interactables {
    public class TV : InteractableObject {
        public Sprite onSprite;
        public Sprite offSprite;
        private AudioSource audioSource;

        private new void Awake() {
            base.Awake();

            if (GetComponent<AudioSource>()) {
                audioSource = GetComponent<AudioSource>();
            }

            if (GetComponent<ObjectiveStatus>()) {
                GetComponent<ObjectiveStatus>().ObjectiveSetup += OnObjectiveSetup;
            }
        }//Awake

        private void OnObjectiveSetup() {
            status = ObjectStatus.Broken;
            indicator.ShowIcon("broken");
        }//OnObjectiveSetup

        //Turn on / off
        protected override void OnFirstButton() {
            Top.GAME.playerCharacter.SetInteractionTarget(this, InteractionType.Primary, 0.5f);
        }//OnFirstButton

        //Use object / repair
        protected override void OnSecondButton() {
            Top.GAME.playerCharacter.SetInteractionTarget(this, InteractionType.Secondary, 3f);
        }//OnSecondButton

        public override bool CanInteract(InteractableObject.InteractionType type = InteractionType.Primary) {
            if (type == InteractionType.Secondary) {
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
                switch (status) {
                    case ObjectStatus.Off:
                        status = ObjectStatus.On;
                        if (audioSource != null) {
                            audioSource.clip = Top.GAME.GetSound("ssj_loop");
                            audioSource.Play();
                        }
                        if (GetComponentInChildren<SpriteRenderer>()) GetComponentInChildren<SpriteRenderer>().sprite = onSprite;
                        break;

                    case ObjectStatus.On:
                        status = ObjectStatus.Off;
                        if (audioSource != null) audioSource.Stop();
                        if (GetComponentInChildren<SpriteRenderer>()) GetComponentInChildren<SpriteRenderer>().sprite = offSprite;
                        break;

                    case ObjectStatus.Broken:
                        Top.GAME.SetMessageText("The TV is broken!", Color.red);
                        Top.GAME.PlayGlobalSound(Top.GAME.GetRandomSound("robotError"));
                        break;
                }
            } else {
                bool result = false;
                switch (Top.GAME.playerCharacter.GetHeldObject().function) {
                    //Duct tape
                    case Pickups.PickupObject.PickupFunction.Repair:
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