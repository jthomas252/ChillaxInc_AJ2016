using UnityEngine;
using System.Collections;

namespace Relax.Objects.Interactables {
    public class ObjectTooltipInfo : MonoBehaviour {
        //Tooltip information
        public string name = "";
        public string description = "";
        public string status = ""; 

        //Object interaction callbacks
        public bool canInteract = true; 
        public delegate void ObjectUICallback(); 
        public event ObjectUICallback FirstButtonCallback;
        public event ObjectUICallback SecondButtonCallback;

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
            Debug.Log(i);
            switch (i) {
                case 0:
                    if (FirstButtonCallback != null) 
                        FirstButtonCallback();
                    break;

                case 1:
                    if (SecondButtonCallback != null) 
                        SecondButtonCallback();
                    break;
            }
        }//CallButton
    }//ObjectTooltipInfo
}