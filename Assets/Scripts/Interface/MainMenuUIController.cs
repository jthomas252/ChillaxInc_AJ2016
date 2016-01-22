using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Relax.Interface {
    public class MainMenuUIController : MonoBehaviour {
        public RectTransform creditsPanel;
        public AudioSource audioSource;

        public void Start() {
            if (audioSource != null && audioSource.clip != null) audioSource.Play();
        }//Start 

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