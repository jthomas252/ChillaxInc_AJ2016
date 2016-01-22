using UnityEngine;
using System.Collections;
using Relax.Objects.Interactables;

namespace Relax.Objects.Waypoints {
    public class ObjectSpawner : MonoBehaviour {
        public InteractableObject objectToSpawn;
        private InteractableObject spawnInstance;
        public Transform spawnParent;

        public void Spawn() {
            spawnInstance = GameObject.Instantiate<InteractableObject>(objectToSpawn); 
            spawnInstance.transform.position = transform.position; 
            spawnInstance.transform.SetParent(spawnParent);
        }//Spawn

        private void OnDrawGizmos() {
            Gizmos.color = new Color(0.5f,0.5f,0.5f,0.5f);
            Gizmos.DrawSphere(transform.position, 0.5f);
        }//OnDrawGizmos
    }//ObjectSpawner
}//Relax