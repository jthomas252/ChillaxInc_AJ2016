using UnityEngine;
using System.Collections;

namespace Relax.Utility {
    public class Indicator : MonoBehaviour {
        public SpriteRenderer background; 
        public SpriteRenderer icon;

        public void ShowIcon(string type = "default") {
            if (!gameObject.activeSelf) gameObject.SetActive(true);
            Sprite sprite = Top.GAME.GetIndicatorSprite(type);
            if (sprite != null) {
                background.enabled = true;
                icon.enabled = true; 
                icon.sprite = sprite; 
            }
        }//ShowIcon

        public void Hide() {
            background.enabled = false; 
            icon.enabled = false; 
        }//Hide
    }//Indicator
}//Relax
