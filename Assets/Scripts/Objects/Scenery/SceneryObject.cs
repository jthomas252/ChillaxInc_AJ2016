using UnityEngine;
using System.Collections;
using Relax.Objects.Interactables;

namespace Relax.Objects.Scenery {
    public class SceneryObject : InteractableObject {
        protected void OnDrawGizmos() {
            BoxCollider box = GetComponent<BoxCollider>();
            Gizmos.color = new Color(1f, 0f, 0f, 0.5f);
            Gizmos.DrawCube(box.transform.position, box.size);
        }//OnDrawGizmos
    }//SceneryObject
}//Relax