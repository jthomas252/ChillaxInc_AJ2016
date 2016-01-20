using UnityEngine;
using System.Collections;

namespace Relax.Objects.Scenery {
    [ExecuteInEditMode]
    public class StorageObject : SceneryObject {
        public GameObject[] objectsStored; 
        public GameObject attachedObject; 
        public Vector3 attachPoint; 

        public bool canStoreObjects; 
        public bool canPlaceObjects;

        protected void Start() {
            if (attachedObject != null) {
                attachedObject.transform.SetParent(transform);
                attachedObject.transform.localPosition = attachPoint; 
            }
        }//Start

        public virtual void StoreObject(GameObject obj) {
            Debug.Log("Override me");
        }//StoreObject

        public virtual void AttachObject(GameObject obj) {
            obj.transform.SetParent(transform); 
            obj.transform.localPosition = attachPoint; 
        }//AttachObject

        public void OnDrawGizmos() {
            Gizmos.color = Color.red; 
            Gizmos.DrawSphere(transform.position + attachPoint, 0.5f); 
            base.OnDrawGizmos(); 
        }//OnDrawGizmos
    }//StorageObject
}//Relax