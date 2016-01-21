using UnityEngine;
using System.Collections;

namespace Relax.Interface {
    public class MainMenuUIController : MonoBehaviour {
        public RectTransform creditsPanel; 

        public void OnNewGameButton(string sceneName) {
            Application.LoadLevel(sceneName);
        }//OnNewGameButton

        public void OnCreditsButton() {
            if (creditsPanel != null) creditsPanel.gameObject.SetActive(true);
        }//OnCreditsButton

        public void OnCreditsBackButton() {
            if (creditsPanel != null) creditsPanel.gameObject.SetActive(false);
        }//OnCreditsBackButton
    }//MainMenuUIController
}//Relax