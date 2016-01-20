using UnityEngine;
using System.Collections;

namespace Relax.Interface {
    public class UIMeter : MonoBehaviour {
        public RectTransform handlePivot;
        
        public float minPos = 100f; 
        public float maxPos = -100f; 
        public float updateTime = 0.2f; 

        private float currentTime = 0f; 
        private float lastPosition;
        private float nextPosition;

        public void Start() {
            SetPosition(0.33f);
        }

        public void SetPosition(float pos = 0f) {
            nextPosition = minPos + (maxPos * pos * 2);
        }//SetPosition

        public void Update() {
            if (handlePivot != null) handlePivot.transform.localRotation = Quaternion.AngleAxis(nextPosition, new Vector3(0f,0f,1f));
        }//Update
    }//UIMeter
}//Relax
