using UnityEngine;
using System.Collections;
namespace Relax.Objects.Pickups {
    public class PickupObject : MonoBehaviour {

        private void Start() {

        }//Start

        private void Update() {

        }//Update

        private void OnDrawGizmos() {
            BoxCollider box = GetComponent<BoxCollider>();
            Gizmos.color = new Color(0f, 0f, 1f, 1f);
            Gizmos.DrawWireCube(box.transform.position, box.size);
        }//OnDrawGizmos
    }//PickupObject
}