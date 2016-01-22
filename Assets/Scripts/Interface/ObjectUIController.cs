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
        public Sprite storageDefaultSprite;

        private void Start() {
            Hide();
        }//Start

        private void Update() {
            if (targetObject != null) {
                transform.position = Camera.main.WorldToScreenPoint(targetObject.transform.position);
            }
        }//Update

        private void Show() {
            transform.localScale = new Vector3(1f, 1f);
        }//Show

        private void Hide() {
            transform.localScale = new Vector3(0f, 0f);
            if (storagePanel != null) {
                storagePanel.localScale = new Vector3(0f, 0f);
                RevertStorageIcons();
            }
        }//Hide

        public void SetObject(ObjectTooltipInfo info) {
            Show();
            activeInfo = info;
            _targetObject = info.transform.gameObject;

            headerText.text = info.objectName;
            descriptionText.text = info.description;
            if (info.showStatus) statusText.text = info.status.ToString();
            for (int i = 0; i < buttons.Length; ++i) {
                if (i < info.buttonNames.Length) {
                    buttons[i].gameObject.SetActive(info.buttonNames[i].enabled);
                    if (buttons[i].gameObject.activeSelf) buttons[i].GetComponentInChildren<Text>().text = info.buttonNames[i].name;
                }
            }

            if (info.showStorage && info.gameObject.GetComponent<StorageObject>()) {
                ShowStoragePanel(info.gameObject.GetComponent<StorageObject>().objectsStored);
            }
        }//SetObject

        private void ShowStoragePanel(List<PickupObject> pickups) {
            if (storagePanel != null && pickups != null) {
                storagePanel.localScale = new Vector3(1f, 1f);
                Button[] buttons = storagePanel.GetComponentsInChildren<Button>();

                for (int i = 0; i < buttons.Length; ++i) {
                    if (i < pickups.Count) {
                        buttons[i].GetComponentInChildren<Text>().text = pickups[i].GetComponent<ObjectTooltipInfo>().objectName;
                        buttons[i].GetComponentInChildren<Image>().sprite = pickups[i].GetComponentInChildren<SpriteRenderer>().sprite;
                    }
                }
            }
        }//ShowStoragePanel

        public void UnsetObject() {
            Hide();
            _targetObject = null;
            activeInfo = null;
        }//UnsetObject

        public void OnButton(int i) {
            if (activeInfo != null) activeInfo.CallButton(i);
        }//OnButton

        public void MarkStorageButtonActive(Image image) {
            Button[] buttons = storagePanel.GetComponentsInChildren<Button>();
            for (int i = 0; i < buttons.Length; ++i) {
                buttons[i].GetComponentInChildren<Image>().color = new Color(1f, 1f, 1f);
            }
            if (image != null) image.color = new Color(0f, 1f, 0f);
        }//MarkStorageButtonActive

        private void RevertStorageIcons() {
            Button[] buttons = storagePanel.GetComponentsInChildren<Button>();
            for (int i = 0; i < buttons.Length; ++i) {
                buttons[i].GetComponentInChildren<Image>().color = new Color(1f, 1f, 1f);
                buttons[i].GetComponentInChildren<Image>().sprite = storageDefaultSprite;
                buttons[i].GetComponentInChildren<Text>().text = "Empty";
            }
        }//RevertStorageIcons
    }//ObjectUIController
}
