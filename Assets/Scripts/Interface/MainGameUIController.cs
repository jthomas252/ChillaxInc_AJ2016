using UnityEngine;
using System.Collections;
using UnityEngine.UI; 
using Relax.Utility; 

namespace Relax.Interface {
    public class MainGameUIController : MonoBehaviour {
        public CanvasGroup pauseGroup; 

        public void OnPauseButton() {
            Top.GAME.Pause(); 
            
            if (pauseGroup != null) {
                if (Time.timeScale == 1) {
                    pauseGroup.gameObject.SetActive(false);
                } else {
                    pauseGroup.gameObject.SetActive(true);
                }
            }
        }//OnPauseButton
    }//MainGameUIController
}