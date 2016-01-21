using UnityEngine;
using UnityEngine.UI; 
using System.Collections;
using System.Collections.Generic;
using Relax.Objects.Characters; 
using Relax.Interface; 
using Relax.Objects.Pickups; 

namespace Relax.Game {
    public class GameHandler : MonoBehaviour {
        private Robot _playerCharacter;
        public Robot playerCharacter {
            get {
                return _playerCharacter; 
            }
        }

        [System.Serializable]
        public struct GameObjective {
            public Objective[] objective; 
        }
        public GameObjective[] gameObjectives; 
        private MainGameUIController mainUI;
        private PickupUIController pickupUI; 
        private HumanUIController humanUI; 

        [System.Serializable]
        public struct IndicatorSprite {
            public string type; 
            public Sprite sprite; 
            public IndicatorSprite(string t, Sprite s) {
                type = t; sprite = s; 
            }
        }
        public IndicatorSprite[] indicatorSprites;


        [System.Serializable]
        public struct SoundLibrary {
            public string name; 
            public AudioClip[] sounds; 
        }
        public SoundLibrary[] soundLibraries; 
        private Dictionary<string, AudioClip[]> soundDictionary; 

        public Transform objectGroup;
        public Transform characterGroup;
        public Transform waypointGroup;

        private void Start() {
            if (FindObjectOfType<Robot>()) {
                _playerCharacter = FindObjectOfType<Robot>(); 
            } else {
                throw new MissingComponentException("No player character in scene!");
            }

            if (FindObjectOfType<MainGameUIController>()) {
                mainUI = FindObjectOfType<MainGameUIController>();
            } else {
                throw new MissingComponentException("No Main UI in scene"); 
            }

            if (FindObjectOfType<PickupUIController>()) {
                pickupUI = FindObjectOfType<PickupUIController>();
            } else {
                throw new MissingComponentException("No Pickup UI in scene"); 
            }

            if (FindObjectOfType<HumanUIController>()) {
                humanUI = FindObjectOfType<HumanUIController>();
            } else {
                throw new MissingComponentException("No Human UI in scene");
            }

            //Initialize the dictionary
            soundDictionary = new Dictionary<string,AudioClip[]>();
            for (int i = 0; i < soundLibraries.Length; ++i) {
                soundDictionary.Add(soundLibraries[i].name, soundLibraries[i].sounds); 
            }

            Objective[] test = new Objective[3]{
                new Objective(false, "Test1"),
                new Objective(false, "Test2 with spacing ... ... .. .. . . . . . . . ."),
                new Objective(true, "Test 3 marked as complete\n with new line")
            };
            mainUI.UpdateObjectivesList(test); 
        }//Start

        private void Update() {
            if (Input.GetKeyDown(KeyCode.A)) {
                Objective[] test = new Objective[2]{
                new Objective(false, "I was switched. Testadadadadada1"),
                new Objective(false, "Test2 with spacing ... ... .. .. . . . . . . . .")
            };
                mainUI.UpdateObjectivesList(test); 
            }
        }//Update

        public AudioClip GetRandomSound(string key) {
            if (soundDictionary.ContainsKey(key)) {
                int random = Random.Range(0, soundDictionary[key].Length - 1);
                return soundDictionary[key][random];
            } else {
                Debug.LogWarning("No sound under key " + key + " was found and nothing was returned");
                return null; 
            }
        }//GetRandomSound

        public void Pause() {
            if (Time.timeScale == 0) {
                Time.timeScale = 1;
            } else {
                Time.timeScale = 0; 
            }
        }//Pause

        public void SetPickup(PickupObject pickup) {
            pickupUI.UpdateObject(pickup); 
        }//SetPickup

        public void HidePickupUI() {
            pickupUI.Hide();
        }//HidePickupUI

        public void SetMessageText(string text, Color color, float duration) {
            mainUI.SetMessageText(text, color, duration); 
        }//SetMessageText

        public Sprite GetIndicatorSprite(string type ) {
            Sprite indicator = null; 
            for (int i = 0; i < indicatorSprites.Length; ++i) {
                if (type == indicatorSprites[i].type) {
                    indicator = indicatorSprites[i].sprite; 
                    i = indicatorSprites.Length; 
                }
            }
            if (indicator == null) {
                throw new UnityException("There was no indicator sprite with type " + type + " and null was returned. Please check the project."); 
            }
            return indicator; 
        }//GetIndicatorSprite
    }//GameHandler
}
