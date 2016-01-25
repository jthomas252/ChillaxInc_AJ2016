using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Relax.Interface;

namespace Relax.Interface {
    public class HumanUIController : MonoBehaviour {
        public UIMeter angerMeter;
        public UIMeter satisactionMeter; 
        public Text timeText; 

        private int seconds = 0;
        private float lastAnger = 0f;
        private float lastSatisfy = 0f;

        public void UpdateValues(int _seconds, float anger = 0f, float satisfaction = 0f) {
            if (seconds != _seconds) {
                seconds = _seconds;
                FormatTime();
            }

            if (angerMeter && anger != lastAnger) angerMeter.SetPosition(anger);
            if (satisactionMeter && satisfaction != lastSatisfy) satisactionMeter.SetPosition(satisfaction);

            lastAnger = anger;
            lastSatisfy = satisfaction;
        }//UpdateValues

        private void FormatTime() {
            string formattedText = string.Empty;
            int minute = (int)Mathf.Floor(seconds / 60);
            int second = seconds % 60;

            if (minute < 10) {
                formattedText += "0"+minute; 
            } else {
                formattedText += minute.ToString();
            }

            formattedText += ":";

            if (second < 10) {
                formattedText += "0"+second;
            } else {
                formattedText += second; 
            }

            timeText.text = formattedText; 
        }//FormatTime
    }//HumanUIController
}//Relax
