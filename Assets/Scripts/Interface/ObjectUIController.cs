using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI; 
using Relax.Objects.Interactables;
using Relax.Objects.Scenery;
using Relax.Objects.Pickups;

namespace Relax.Interface {
    public class ObjectUIController : MonoBehaviour {
        private GameObject _targetObject;
        public GameObject targetObject {
            get {
                return _targetObject;
            }
        }
        private ObjectTooltipInfo activeInfo; 

        //UI Elements
        public Text headerText;
        public Text descriptionText;
        public Text statusText; 
        public Button[] buttons; 
        public RectTransform storagePanel; 

	    private void Start () {
	        Hide(); 
	    }//Start
	
	    private void Update () {
            if (targetObject != null) {
	            transform.position = Camera.main.WorldToScreenPoint(targetObject.transform.position);  
            }
	    }//Update

        private void Show() {
            transform.localScale = new Vector3(1f, 1f);
        }//Show

        private void Hide() {
            transform.localScale = new Vector3(0f, 0f);
        }//Hide

        public void SetObject(ObjectTooltipInfo info) {
            Show(); 
            activeInfo = info; 
            _targetObject = info.transform.gameObject; 

            headerText.text = info.name; 
            descriptionText.text = info.description; 
            if (info.showStatus) statusText.text = info.status.ToString();
            for (int i = 0; i < buttons.Length; ++i) {
                if (buttons[i].GetComponentInChildren<Text>() && i < info.buttonNames.Length) {
                    buttons[i].GetComponentInChildren<Text>().text = info.buttonNames[i].name; 
                    buttons[i].gameObject.SetActive(info.buttonNames[i].enabled); 
                }
            }
        }//SetObject

        public void UnsetObject() {
            Hide(); 
            _targetObject = null; 
            activeInfo = null; 
        }//UnsetObject

        public void OnButton(int i) {
            if (activeInfo != null) activeInfo.CallButton(i); 
        }//OnButton
    }//ObjectUIController
}
