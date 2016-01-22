using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Relax.Utility; 
using Relax.Objects.Pickups;
using Relax.Objects.Interactables;

namespace Relax.Interface {
    public class PickupUIController : MonoBehaviour {
        public Text text; 
        public Image image;

        private void Start() {
            Hide(); 
        }//Start

        public void UpdateObject(PickupObject pickup) {
            transform.localScale = new Vector3(1f,1f);
            if (pickup.GetComponentInChildren<SpriteRenderer>()) {
                text.text = pickup.GetComponent<ObjectTooltipInfo>().objectName;
                if (pickup.holdingSprite != null) {
                    image.sprite = pickup.holdingSprite;
                } else {
                    image.sprite = pickup.GetComponentInChildren<SpriteRenderer>().sprite;
                }
            }
        }//UpdateObject

        public void Hide() {
            transform.localScale = new Vector3(0f,0f);
        }//Hide

        public void OnDropButton() {
            Top.GAME.playerCharacter.DropHeldObject(); 
            Hide();
        }//Drop
    }//PickupUIController
}
