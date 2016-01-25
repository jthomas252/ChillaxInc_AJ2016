using UnityEngine;
using System.Collections;
using Relax.Utility;
using Relax.Game;

namespace Relax.Objects.Interactables {
    public class AlarmClock : InteractableObject {
        public float smashChance = 0.5f;
        public float igniteChance = 0.25f;
        private AudioSource audioSource;

        private new void Awake() {
            base.Awake(); 
            if (GetComponent<AudioSource>()) {
                audioSource = GetComponent<AudioSource>();
                audioSource.clip = Top.GAME.GetSound("alarm");
            }

            if (GetComponent<ObjectiveStatus>()) {
                GetComponent<ObjectiveStatus>().ObjectiveSetup += OnObjectiveSetup;
            }
        }//Awake

        private void OnObjectiveSetup() {
            status = ObjectStatus.Broken;
            indicator.ShowIcon("broken");
            if (audioSource != null) audioSource.Play();
        }//OnObjectiveSetup

        //Turn on / off
        protected override void OnFirstButton() {
            Top.GAME.playerCharacter.SetInteractionTarget(this, InteractionType.Primary, 0.5f);
        }//OnFirstButton

        //Use object / repair
        protected override void OnSecondButton() {
            Top.GAME.playerCharacter.SetInteractionTarget(this, InteractionType.Secondary, 0.2f);
        }//OnSecondButton

        public override bool CanInteract(InteractableObject.InteractionType type = InteractionType.Primary) {
            return true;
        }//CanInteract

        public override void Interact(InteractionType type = InteractionType.Primary) {
            if (type == InteractionType.Primary) {
                switch (status) {
                    case ObjectStatus.Off:
                        status = ObjectStatus.On;
                        if (audioSource != null) audioSource.Play();
                        break;

                    case ObjectStatus.On:
                        status = ObjectStatus.Off;
                        if (audioSource != null) audioSource.Stop();
                        break;

                    case ObjectStatus.Broken:
                        Top.GAME.SetMessageText("It won't stop beeping! Oh no!", Color.red);
                        Top.GAME.PlayGlobalSound(Top.GAME.GetRandomSound("robotError"));
                        break;
                }
            } else {
                Top.GAME.PlayGlobalSound(Top.GAME.GetSound("smash"));
                float rand = Random.Range(0f,1f);
                if (rand < smashChance) {
                    Top.GAME.PlayGlobalSound(Top.GAME.GetRandomSound("robotError"));
                    if (rand < igniteChance) {
                        if (GetComponent<Flammable>()) GetComponent<Flammable>().Ignite();
                    }
                } else {
                    status = ObjectStatus.Off;
                    if (audioSource != null) audioSource.Stop();
                    indicator.Hide();
                    if (GetComponent<ObjectiveStatus>()) {
                        GetComponent<ObjectiveStatus>().MarkComplete();
                    }
                }
            }
            base.Interact();
        }//Interact

        public override void OnIgnite() {
            status = ObjectStatus.Broken;
            indicator.ShowIcon("broken");
        }//OnIgnite
    }//TV
}//Relax