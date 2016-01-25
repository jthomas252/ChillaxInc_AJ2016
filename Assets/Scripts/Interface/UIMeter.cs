using UnityEngine;
using System.Collections;

namespace Relax.Interface {
    public class UIMeter : MonoBehaviour {
        public RectTransform handlePivot;

        public float minPos = 100f;
        public float maxPos = -100f;
        public float catchTime = 0.1f;
        private float lastPosition;
        private float nextPosition; 
        private float meterAngle;
        private float currentTime;

        public void SetPosition(float pos = 0f) {
            lastPosition = nextPosition;
            nextPosition = pos; 
            currentTime = 0f; 
        }//SetPosition

        private void SetMeterAngle(float pos = 0f) {
            meterAngle = minPos + (maxPos * pos * 2);
        }//SetMeterAngle

        public void Update() {
            float lerpAmount = (currentTime / catchTime);
            float newPosition = lastPosition + ((nextPosition - lastPosition) * lerpAmount);
            if (currentTime < catchTime) {
                currentTime += Time.deltaTime;
                SetMeterAngle(newPosition);
                if (currentTime > catchTime) currentTime = catchTime; 
            }
            if (handlePivot != null) handlePivot.transform.localRotation = Quaternion.AngleAxis(meterAngle, new Vector3(0f, 0f, 1f));
        }//Update
    }//UIMeter
}//Relax
