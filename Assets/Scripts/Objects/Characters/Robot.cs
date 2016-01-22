using UnityEngine;
using System.Collections;
using Relax.Objects.Pickups;
using Relax.Objects.Interactables;
using Relax.Utility;
using Relax.Interface;

namespace Relax.Objects.Characters {
    public class Robot : Character {
        public MoveIndicator moveIndicator;
        public PickupObject pickup;

        protected new void Start() {
            if (GetComponent<Flammable>()) GetComponent<Flammable>().OnIgnite += OnIgnite;
        }//Start

        protected new void Update() {
            if (Input.GetMouseButtonDown(1)) {
                RaycastHit rayHit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                int layerMask = 1 << LayerMask.NameToLayer("GEOMETRY");
                if (Physics.Raycast(ray, out rayHit, 200f, layerMask)) {
                    navAgent.SetDestination(rayHit.point);
                    UpdateMoveIndicator(rayHit.point);
                    PlaySound(Top.GAME.GetRandomSound("robotMove"));
                    PlaySound(Top.GAME.GetRandomSound("robotMoveLoop"), 1);
                }
            }

            if (moveIndicator != null) moveIndicator.OnTargetReached += OnTargetReached;
            base.UpdateAnimation();
            base.Update();
        }//Update

        private void OnTargetReached() {
            OnStop();
        }//OnTargetReached

        private void UpdateMoveIndicator(Vector3 newPos) {
            if (moveIndicator != null) {
                moveIndicator.gameObject.SetActive(true);
                moveIndicator.transform.position = new Vector3(newPos.x, 1f, newPos.z);
            }
        }//UpdateMoveIndicator

        public new void SetInteractionTarget(InteractableObject obj, InteractableObject.InteractionType type = InteractableObject.InteractionType.Primary, float timeToComplete = 0f) {
            UpdateMoveIndicator(obj.transform.position);
            base.SetInteractionTarget(obj, type, timeToComplete);
            PlaySound(Top.GAME.GetRandomSound("robotMove"));
            PlaySound(Top.GAME.GetRandomSound("robotMoveLoop"), 1);
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
            PlaySound(Top.GAME.GetRandomSound("robotDrop"));
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

        public void OnIgnite() {
            indicator.ShowIcon("sadRobot");
        }//OnIgnite

        protected override void OnStop() {
            PlaySound(Top.GAME.GetRandomSound("robotMoveStop"), 2);
            StopSound(1);
        }//OnStop

        protected override void OnInteract() {
            PlaySound(Top.GAME.GetRandomSound("robotGrab"));
        }//OnInteract

        private void OnDestroy() {
            if (GetComponent<Flammable>()) GetComponent<Flammable>().OnIgnite -= OnIgnite;
        }//OnDestroy
    }//Robot
}//Relax
