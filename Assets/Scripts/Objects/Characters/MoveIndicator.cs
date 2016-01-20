using UnityEngine;
using System.Collections;

namespace Relax.Objects.Characters {
    public class MoveIndicator : MonoBehaviour {
        private void OnTriggerEnter() {
            gameObject.SetActive(false);
        }//OnTriggerEnter
    }//MoveIndicator
}//Relax
