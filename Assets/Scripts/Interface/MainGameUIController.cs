using UnityEngine;
using System.Collections;

namespace Relax.Interface {
    public class MainGameUIController : MonoBehaviour {
        public void OnWallHide() {
            GameObject[] objects = GameObject.FindGameObjectsWithTag("WallHide");
            for (int i = 0; i < objects.Length; ++i) {
                objects[i].transform.localScale = new Vector3(1f,0.1f); 
            }
        }//OnWallHide
    }//MainGameUIController
}