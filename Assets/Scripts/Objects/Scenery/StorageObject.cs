using UnityEngine;
using Relax.Utility;
using Relax.Objects.Pickups;
using System.Collections;
using System.Collections.Generic;
using Relax.Interface;
using Relax.Objects.Interactables;

namespace Relax.Objects.Scenery {
    public class StorageObject : SceneryObject {
        private int maxObjectsStored = 4;
        public List<PickupObject> objectsStored;
        public GameObject attachedObject;
        public Vector3 attachPoint;

        public bool canStoreObjects;
        public bool canPlaceObjects;
        private PickupObject pickedObject;

        public PickupObject[] baseStock; 

        protected void Start() {
            if (attachedObject != null) {
                attachedObject.transform.SetParent(transform);
                attachedObject.transform.localPosition = attachPoint;
            }
            tooltipInfo.StorageButtonCallback += OnStorageButton;
        }//Start

        private void HideStoredObjects() {
            if (objectsStored != null) {
                //This is a bad solution, but because I need the objects active 
                //I'm keeping it like this. 
                for (int i = 0; i < objectsStored.Count; ++i) {
                    objectsStored[i].transform.localScale = new Vector3(0f, 0f, 0f);
                }
            }
        }//HideStoredObjects

        public void RestockObjects() {
            if (objectsStored != null) {
                for (int i = 0; i < objectsStored.Count; ++i) {
                    GameObject.Destroy(objectsStored[i].gameObject);
                }
                objectsStored.Clear(); 
            } 

            for (int i = 0; i < baseStock.Length; ++i) {
                StoreObject(GameObject.Instantiate(baseStock[i]));
            }

            HideStoredObjects();
        }//RestockObjects

        public virtual void StoreObject(PickupObject obj) {
            if (objectsStored == null) {
                objectsStored = new List<PickupObject>();
            }

            if (objectsStored.Count < maxObjectsStored) {
                obj.transform.SetParent(transform);
                objectsStored.Add(obj);
                HideStoredObjects();
            } else {
                Top.GAME.SetMessageText("The box is too full! Take some items out!", Color.red);
                Top.GAME.PlayGlobalSound(Top.GAME.GetRandomSound("robotError"));
            }
        }//StoreObject

        public virtual void TakeObject(PickupObject obj) {
            if (!Top.GAME.playerCharacter.IsHoldingObject()) {
                obj.transform.localScale = new Vector3(1f, 1f, 1f);
                objectsStored.Remove(obj);
                Top.GAME.playerCharacter.SetHeldObject(obj);
            } else {
                Top.GAME.SetMessageText("You can't take an object while you're holding one!", Color.red);
                Top.GAME.PlayGlobalSound(Top.GAME.GetRandomSound("robotError"));
            }
        }//TakeObject

        public virtual void AttachObject(GameObject obj) {
            obj.transform.SetParent(transform);
            obj.transform.localPosition = attachPoint;
        }//AttachObject

        public new void OnDrawGizmos() {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position + attachPoint, 0.5f);
            base.OnDrawGizmos();
        }//OnDrawGizmos

        public override void Interact(InteractionType type = InteractionType.Primary) {
            switch (type) {
                case InteractionType.Primary:
                    PickupObject obj = Top.GAME.playerCharacter.GetHeldObject();
                    if (obj != null) {
                        Top.GAME.playerCharacter.DropHeldObject();
                        StoreObject(obj);
                    } else {
                        Top.GAME.SetMessageText("The robot isn't holding an object.", Color.red, 3.5f);
                    }
                    break;

                case InteractionType.Secondary:
                    if (pickedObject != null) {
                        TakeObject(pickedObject);
                    } else {
                        Top.GAME.SetMessageText("You haven't selected an object!", Color.red, 3.5f);
                    }
                    break;

                default:
                    Debug.Log("Something went wrong!");
                    break;
            }
        }//Interact

        protected override void OnFirstButton() {
            Top.GAME.playerCharacter.SetInteractionTarget(this, InteractionType.Primary, 1f);
        }//OnFirstButton

        protected override void OnSecondButton() {
            Top.GAME.playerCharacter.SetInteractionTarget(this, InteractionType.Secondary, 1f);
        }//OnSecondButton

        protected virtual void OnStorageButton(int i = 0) {
            if (i < objectsStored.Count) {
                pickedObject = objectsStored[i];
            } else {
                pickedObject = null;
            }
        }//OnStorageButton

        private void OnDestroy() {
            tooltipInfo.StorageButtonCallback -= OnStorageButton;
        }//OnDestroy
    }//StorageObject
}//Relax