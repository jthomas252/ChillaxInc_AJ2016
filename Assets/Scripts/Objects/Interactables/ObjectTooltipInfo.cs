using UnityEngine;
using System.Collections;
using Relax.Objects.Interactables;

namespace Relax.Objects.Interactables {
    public class ObjectTooltipInfo : MonoBehaviour {
        //Tooltip information
        public string objectName = "";
        public string description = "";
        public InteractableObject.ObjectStatus status {
            get {
                if (GetComponent<InteractableObject>()) {
                    return GetComponent<InteractableObject>().status;
                } else {
                    return InteractableObject.ObjectStatus.Off; 
                }
            }
        }

        //Object interaction callbacks
        public bool canInteract = true;
        public bool showStatus = false;
        public bool showStorage = false;
        public bool showTooltip = true; 
        public delegate void ObjectUICallback();
        public delegate void ObjectUISelectionCallback(int i = 0);
        public event ObjectUICallback FirstButtonCallback;
        public event ObjectUICallback SecondButtonCallback;
        public event ObjectUISelectionCallback StorageButtonCallback;

        //Button names
        [System.Serializable]
        public struct ButtonName {
            public string name;
            public bool enabled;

            public ButtonName(string n, bool e) {
                name = n; enabled = e;
            }
        }

        public ButtonName[] buttonNames = new ButtonName[2]{
            new ButtonName("Button1", true),
            new ButtonName("Button2", true)
        };

        public void CallButton(int i) {
            switch (i) {
                case 0:
                    if (FirstButtonCallback != null)
                        FirstButtonCallback();
                    break;

                case 1:
                    if (SecondButtonCallback != null)
                        SecondButtonCallback();
                    break;

                case 2:
                case 3:
                case 4:
                case 5:
                    if (StorageButtonCallback != null) {
                        StorageButtonCallback(i - 2);
                    }
                    break;
            }
        }//CallButton
    }//ObjectTooltipInfo
}