using UnityEngine;
using Relax.Utility;
using Relax.Objects.Pickups;
using System.Collections;
using System.Collections.Generic;

namespace Relax.Objects.Scenery {
    [ExecuteInEditMode]
    public class StorageObject : SceneryObject {
        public int maxObjectsStored = 4; 
        public List<PickupObject> objectsStored; 
        public GameObject attachedObject; 
        public Vector3 attachPoint; 

        public bool canStoreObjects; 
        public bool canPlaceObjects;

        protected void Start() {
            if (attachedObject != null) {
                attachedObject.transform.SetParent(transform);
                attachedObject.transform.localPosition = attachPoint; 
            }
            objectsStored = new List<PickupObject>(); 
        }//Start

        public virtual void StoreObject(PickupObject obj) {
            if (objectsStored.Count < maxObjectsStored) {
                obj.gameObject.SetActive(false);
                obj.transform.SetParent(transform);
                objectsStored.Add(obj); 
            }
        }//StoreObject

        public virtual void TakeObject(PickupObject obj) {
            if (!Top.GAME.playerCharacter.IsHoldingObject()) {
                objectsStored.Remove(obj);
                Top.GAME.playerCharacter.SetHeldObject(obj);
            }
        }//TakeObject

        public virtual void AttachObject(GameObject obj) {
            obj.transform.SetParent(transform); 
            obj.transform.localPosition = attachPoint; 
        }//AttachObject

        public void OnDrawGizmos() {
            Gizmos.color = Color.red; 
            Gizmos.DrawSphere(transform.position + attachPoint, 0.5f); 
            base.OnDrawGizmos(); 
        }//OnDrawGizmos

        public override void Interact() {
            base.Interact();
        }//Interact

        protected override void OnFirstButton() {
            Top.GAME.playerCharacter.SetInteractionTarget(this, InteractionType.Primary, 1f);
        }//OnFirstButton

        protected override void OnSecondButton() {
            Top.GAME.playerCharacter.SetInteractionTarget(this, InteractionType.Secondary, 1f);
        }//OnSecondButton
    }//StorageObject
}//Relax