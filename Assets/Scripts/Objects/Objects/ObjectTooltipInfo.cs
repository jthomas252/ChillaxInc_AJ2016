using UnityEngine;
using System.Collections;

namespace Relax.Interface {
    public class ObjectTooltipInfo : MonoBehaviour {
        //Tooltip information
        public string name = "N/A";
        public string description = "N/A";
        public string interaction = "Left click to Interact.";
        public string status = "N/A"; 

        //Object interaction callbacks
        public bool canInteract = true; 
        public delegate void ObjectUICallback(); 
        public event ObjectUICallback FirstButtonCallback;
        public event ObjectUICallback SecondButtonCallback;
        public event ObjectUICallback ThirdButtonCallback; 
        public event ObjectUICallback FourthButtonCallback; 

        //Button names
        [System.Serializable]
        public struct ButtonName {
            public string name;
            public bool enabled; 

            public ButtonName(string n, bool e) {
                name = n; enabled = e; 
            }
        }

        public ButtonName[] buttonNames = new ButtonName[4]{
            new ButtonName("Button1", true),
            new ButtonName("Button2", true),
            new ButtonName("Button3", true),
            new ButtonName("Button4", true)
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
                    if (ThirdButtonCallback != null) 
                        ThirdButtonCallback();
                    break;

                case 3:
                    if (FourthButtonCallback != null) 
                        FourthButtonCallback();
                    break;
            }
        }//CallButton
    }//ObjectTooltipInfo
}