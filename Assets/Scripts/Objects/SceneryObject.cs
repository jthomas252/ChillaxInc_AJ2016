using UnityEngine;
using System.Collections;

public class SceneryObject : MonoBehaviour {
    private void OnDrawGizmos() {
        BoxCollider box = GetComponent<BoxCollider>();
        Gizmos.color = new Color(1f, 0f, 0f, 1f);
        Gizmos.DrawWireCube(box.transform.position, box.size);
    }//OnDrawGizmos
}
