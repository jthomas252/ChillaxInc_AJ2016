using UnityEngine;
using System.Collections;
using Relax.Game;

namespace Relax.Objects.Waypoints {
    public class HumanWalkPath : MonoBehaviour {
        private WalkPathNode[] _nodes;
        private int currentNode; 

        private void Awake() {
            _nodes = GetComponentsInChildren<WalkPathNode>(); 
            currentNode = 0;
        }//Awake

        public Vector3 GetCurrentNodePoint() {
            if (currentNode < _nodes.Length) {
                return _nodes[currentNode].point;
            } else {
                return transform.position;
            }
        }//GetCurrentNodePoint

        private void OnDrawGizmos() {
            WalkPathNode[] pathNodes = GetComponentsInChildren<WalkPathNode>(); 

            Gizmos.color = new Color(0.6f,0.6f,1f,0.8f);
            Vector3 lastLinePoint = transform.position;
            Gizmos.DrawSphere(transform.position, 0.8f);

            if (pathNodes != null) {
                for (int i = 0; i < pathNodes.Length; ++i) {
                    Gizmos.DrawLine(lastLinePoint, pathNodes[i].point);
                    lastLinePoint = pathNodes[i].point;
                }
            }
        }//OnDrawGizmos
    }//HumanWalkPath
}//Relax
