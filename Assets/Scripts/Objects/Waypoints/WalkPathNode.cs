using UnityEngine;
using System.Collections;
using Relax.Game;

namespace Relax.Objects.Waypoints {
    [System.Serializable]
    public class WalkPathNode : MonoBehaviour {
        public Vector3 point {
            get {
                return transform.position; 
            }
        }

        public ObjectiveStatus[] objects; 
        public float checkRadius = 3f; 

        private void OnDrawGizmos() {
            Gizmos.color = new Color(0.6f,0.6f,1f,0.33f);
            Gizmos.DrawSphere(point, 0.5f);

            Gizmos.color = new Color(1f,1f,1f,1f);
            if (objects != null) {
                for (int i = 0; i < objects.Length; ++i) {
                    if (objects[i] != null) Gizmos.DrawLine(point, objects[i].transform.position); 
                }
            }
        }//OnDrawGizmos
    }//WalkPathNode
}//Relax