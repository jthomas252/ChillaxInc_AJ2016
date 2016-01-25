using UnityEngine;
using System.Collections;
using Relax.Utility;
using Relax.Objects.Interactables;

namespace Relax.Objects.Pickups {
    public class PickupObject : InteractableObject {
        public enum PickupFunction {
            Repair,
            LightBulb,
            FoodCart,
            FightFire,
            BatteryLive,
            BatteryDead,
            Trash
        }
        public PickupFunction function; 

        public bool consumeOnUse = true;
        public Sprite holdingSprite;

        public void TakeObject(Transform _transform) {
            if (Top.GAME.playerCharacter.IsHoldingObject()) {
                Top.GAME.playerCharacter.DropHeldObject();
            }
            transform.SetParent(_transform);
            transform.localScale = new Vector3(0f, 0f, 0f);
            Top.GAME.SetPickup(this);
        }//PickupObject

        public void DropObject(Vector3 pos, bool dispose = false) {
            transform.localScale = new Vector3(1f, 1f, 1f);
            transform.SetParent(Top.GAME.objectGroup);
            transform.position = pos;
            Top.GAME.HidePickupUI();
            if (dispose) Destroy(this.gameObject);
        }//DropObject

        public void OnUse(bool result) {
            if (consumeOnUse && result) Top.GAME.playerCharacter.DropHeldObject(true);
        }//OnUse

        private void OnDrawGizmos() {
            BoxCollider box = GetComponent<BoxCollider>();
            Gizmos.color = new Color(0f, 0f, 1f, 0.5f);
            Gizmos.DrawCube(box.transform.position, transform.localScale);
        }//OnDrawGizmos

        public override void Interact(InteractionType type = InteractionType.Primary) {
            Top.GAME.playerCharacter.SetHeldObject(this);
        }//Interact

        protected override void OnFirstButton() {
            Top.GAME.playerCharacter.SetInteractionTarget(this);
        }//OnFirstButton
    }//PickupObject
}//Relax