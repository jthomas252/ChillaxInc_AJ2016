using UnityEngine;
using System.Collections;
using Relax.Objects.Pickups;
using Relax.Objects.Interactables; 
using Relax.Utility; 
using Relax.Interface; 

namespace Relax.Objects.Characters {
    public class Robot : Character {
        public GameObject moveIndicator; 
        public PickupObject pickup; 

        protected void Start() {
            base.Start(); 
        }//Start

        protected void Update() {
            if (Input.GetMouseButtonDown(1)) {
                RaycastHit rayHit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out rayHit)) {
                    navAgent.SetDestination(rayHit.point);
                    UpdateMoveIndicator(rayHit.point);
                }
            }

            base.UpdateAnimation();
            base.Update(); 
        }//Update

        private void UpdateMoveIndicator(Vector3 newPos) {
            if (moveIndicator != null) {
                moveIndicator.gameObject.SetActive(true);
                moveIndicator.transform.position = new Vector3(newPos.x, 1f, newPos.z); 
            }
        }//UpdateMoveIndicator

        public void SetInteractionTarget(InteractableObject obj, InteractableObject.InteractionType type = InteractableObject.InteractionType.Using, float timeToComplete = 0f, string indicator = "default") {
            UpdateMoveIndicator(obj.transform.position); 
            base.SetInteractionTarget(obj, type, timeToComplete, indicator);
        }//SetInteractionTarget

        public void SetHeldObject(PickupObject _pickup) {
            if (pickup == null) {
                Top.GAME.SetPickup(_pickup);
                _pickup.TakeObject(transform);
                pickup = _pickup;
                FindObjectOfType<ObjectUIController>().UnsetObject();
            } else {
                Top.GAME.SetMessageText("You can't carry multiple objects! Drop your held object.", Color.red, 3.5f);
            }
        }//SetHeldObject

        public void DropHeldObject() {
            pickup.DropObject(transform.position);
            pickup = null;
        }//DropHeldObject

        public PickupObject GetHeldObject() {
            return pickup; 
        }//GetHeldObject

        public bool IsHoldingObject() {
            if (pickup == null) {
                return false;
            } else {
                return true; 
            }
        }//IsHoldingObject
    }//Robot
}
