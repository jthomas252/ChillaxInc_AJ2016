using UnityEngine;
using System.Collections;

namespace Relax.Objects.Characters {
    public class MoveIndicator : MonoBehaviour {
        public delegate void MovementEvent(); 
        public event MovementEvent OnTargetReached; 
        
        private void OnTriggerEnter() {
            if (OnTargetReached != null) OnTargetReached(); 
            gameObject.SetActive(false);
        }//OnTriggerEnter
    }//MoveIndicator
}//Relax
