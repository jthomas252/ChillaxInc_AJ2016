using UnityEngine;
using System.Collections;

namespace Relax.Utility {
    public class ScreenShake : MonoBehaviour {
        public float timeBetweenShakes = 0.1f; 
        public float shakeRange = 1.5f; 

        private Vector3 startingPoint;
        private int maxShakes; 

        public void Shake(int shakes = 30) {
            startingPoint = transform.position; 
            maxShakes = shakes; 

            StartCoroutine(IterateShake());
        }//Shake

        private IEnumerator IterateShake() {
            for (int i = 0; i < maxShakes; ++i) {
                ShakePosition();
                yield return new WaitForSeconds(timeBetweenShakes);
            }
            RevertPosition();
            StopCoroutine(IterateShake());
        }//IterateShake

        private void ShakePosition() {
            float xDiff = Random.Range(-shakeRange, shakeRange);
            float yDiff = Random.Range(-shakeRange, shakeRange);
            float zDiff = Random.Range(-shakeRange, shakeRange);

            transform.position = new Vector3(
                startingPoint.x + xDiff,
                startingPoint.y + yDiff,
                startingPoint.z + zDiff
            );
        }//ShakePosition

        private void RevertPosition() {
            transform.position = startingPoint; 
        }//RevertPosition
    }//ScreenShake
}//Relax