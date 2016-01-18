using UnityEngine;
using System.Collections;
using UnityEngine.UI; 
using Relax.Objects.Interactables;

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
            Show(); 
            activeTime = 0f;
            _targetObject = info.transform.gameObject; 

            headerText.text = info.name; 
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
    }//TooltipUIController
}
