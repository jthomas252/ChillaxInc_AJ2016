using UnityEngine;
using System.Collections;
using Relax.Utility; 
using Relax.Objects.Interactables; 

namespace Relax.Objects.Pickups {
    public class PickupObject : InteractableObject {
        public void TakeObject(Transform _transform) {
            if (!Top.GAME.playerCharacter.IsHoldingObject()) {
                transform.SetParent(_transform);
                gameObject.SetActive(false);
            } else {
                Top.GAME.SetMessageText("You can't carry multiple objects! Drop your held object.", Color.red, 3.5f); 
            }
        }//PickupObject

        public void DropObject(Vector3 pos) {
            transform.SetParent(Top.GAME.objectGroup); 
            transform.position = pos; 
            gameObject.SetActive(true);
        }//DropObject

        private void OnDrawGizmos() {
            BoxCollider box = GetComponent<BoxCollider>();
            Gizmos.color = new Color(0f, 0f, 1f, 0.5f);
            Gizmos.DrawCube(box.transform.position, box.size);
        }//OnDrawGizmos

        public override void Interact() {
            Top.GAME.playerCharacter.SetHeldObject(this);
        }//Interact

        protected override void OnFirstButton() {
            Top.GAME.playerCharacter.SetInteractionTarget(this);
        }//OnFirstButton
    }//PickupObject
}//Relax