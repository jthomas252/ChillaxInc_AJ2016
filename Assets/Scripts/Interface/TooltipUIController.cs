using UnityEngine;
using System.Collections;
using UnityEngine.UI; 
using Relax.Objects.Interactables;
using Relax.Utility;

namespace Relax.Interface {
    public class TooltipUIController : MonoBehaviour {
        public float lingerTime = 1f; 
        private GameObject _targetObject;
        public GameObject targetObject {
            get {
                return _targetObject; 
            }
        }

        //UI Elements
        public Text headerText; 

        private float activeTime = 0f; 
        private CanvasGroup canvas; 

        private void Start() {
            Hide();
            if (GetComponent<CanvasGroup>()) {
                canvas = GetComponent<CanvasGroup>(); 
            } else {
                throw new MissingComponentException("No CanvasGroup on Tooltip. Will break fading"); 
            }
        }//Start

        public void SetObject(ObjectTooltipInfo info) {
            if (transform.localScale.x == 0f) {
                Show();
            }

            if (info.gameObject != _targetObject) Top.GAME.PlayGlobalSound(Top.GAME.GetRandomSound("ui_hover"));

            activeTime = 0f;
            _targetObject = info.transform.gameObject; 

            headerText.text = info.objectName;
        }//Show

        private void Show() {
            transform.localScale = new Vector3(1f, 1f);
        }//Show

        private void Hide() {
            transform.localScale = new Vector3(0f,0f); 
        }//Hide

        private void Update() {
            if (targetObject != null) {
                transform.position = Camera.main.WorldToScreenPoint(targetObject.transform.position);
            }

            if (activeTime < lingerTime) {
                activeTime += Time.deltaTime;
                canvas.alpha = 1f - (activeTime/lingerTime); 
            } else {
                Hide(); 
            }
        }//Update

        public void OnClicked() {
            if (_targetObject != null && 
                _targetObject.GetComponent<ObjectTooltipInfo>() && 
                _targetObject.GetComponent<ObjectTooltipInfo>().canInteract) {
                if (FindObjectOfType<ObjectUIController>()) 
                    FindObjectOfType<ObjectUIController>().SetObject(_targetObject.GetComponent<ObjectTooltipInfo>());
                    Top.GAME.PlayGlobalSound(Top.GAME.GetRandomSound("ui_click"));
            }
        }//OnClick
    }//TooltipUIController
}
