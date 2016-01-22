using UnityEngine;
using System.Collections;
using Relax.Utility;

namespace Relax.Objects.Scenery {
    public class Door : MonoBehaviour {
        public float openTime = 0.5f;
        public GameObject door;
        public float minSize = 0.1f;
        private float currentTime = 0f;
        private bool isOpening = false;

        private void OnTriggerEnter() {
            isOpening = true;
            if (currentTime < 0.1f) Top.GAME.PlayGlobalSound(Top.GAME.GetSound("door"));
        }//OnTriggerEnter

        private void OnTriggerExit() {
            isOpening = false;
        }//OnTriggerExit

        private void Update() {
            if (isOpening && currentTime < openTime) {
                currentTime += Time.deltaTime;
                if (currentTime > openTime) currentTime = openTime;
            } else if (isOpening == false && currentTime > 0f) {
                currentTime -= Time.deltaTime;
                if (currentTime < 0f) currentTime = 0f;
            }

            float newSize = 1f - (currentTime / openTime);
            if (newSize < minSize) newSize = minSize;

            door.transform.localScale = new Vector3(
                transform.localScale.x,
                newSize,
                transform.localScale.z);
        }//Update
    }//Door
}//Relax
