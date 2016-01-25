using UnityEngine;
using System.Collections;
using Relax.Utility;
using Relax.Objects.Pickups;
using Relax.Interface;

namespace Relax.Objects.Interactables {
    public class Fire : InteractableObject {
        public event Top.GenericEvent OnContained;
        public PickupObject interactObject;

        private new void Awake() {
            base.Awake();
        }//Awake

        protected override void OnFirstButton() {
            Top.GAME.playerCharacter.SetInteractionTarget(this, InteractionType.Primary, 1f);
        }//OnFirstButton

        public override bool CanInteract(InteractionType type = InteractionType.Primary) {
            if (Top.GAME.playerCharacter.GetHeldObject() && Top.GAME.playerCharacter.GetHeldObject().function == PickupObject.PickupFunction.FightFire) {
                Top.GAME.PlayGlobalSound(Top.GAME.GetSound("fireExtenguish"));
                return true;
            } else {
                Top.GAME.SetMessageText("You don't have a fire extenguisher!", Color.red);
                Top.GAME.PlayGlobalSound(Top.GAME.GetRandomSound("robotError"));
                return false;
            }
        }//Interact

        public override void Interact(InteractableObject.InteractionType type = InteractionType.Primary) {
            FindObjectOfType<ObjectUIController>().UnsetObject();
            Contain();
        }//Interact

        public void Contain() {
            if (OnContained != null) OnContained();
            Destroy(this.gameObject);
        }//Contain
    }//Fire
}//Relax