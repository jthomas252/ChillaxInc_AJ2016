using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Relax.Objects.Characters;
using Relax.Interface;
using Relax.Objects.Pickups;
using Relax.Objects.Interactables;
using Relax.Objects.Waypoints;
using Relax.Objects.Scenery;
using Relax.Utility;

namespace Relax.Game {
    public class GameHandler : MonoBehaviour {
        private Robot _playerCharacter;
        public Robot playerCharacter {
            get {
                return _playerCharacter;
            }
        }

        private MainGameUIController mainUI;
        private PickupUIController pickupUI;
        private HumanUIController humanUI;

        [System.Serializable]
        public struct DayInfo {
            public int timeToArrival; 
            public ObjectiveStatus[] objectives;
            public ObjectSpawner[] spawners; 
            public HumanWalkPath path; 
        }
        public DayInfo[] dayObjectives; 
        public static int currentDay = 0; 

        [System.Serializable]
        public struct IndicatorSprite {
            public string type;
            public Sprite sprite;
        }
        public IndicatorSprite[] indicatorSprites;
        private Dictionary<string, Sprite> indicatorDictionary;

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

        public GameObject firePrefab;
        public AudioClip music;

        private AudioSource[] audioSources;
        private float currentTime; 
        private float timeToSpawn;

        private Human _humanCharacter;
        public Human humanCharacter {
            get {
                return _humanCharacter; 
            }
        }

        private void Awake() {
            if (FindObjectOfType<Robot>()) {
                _playerCharacter = FindObjectOfType<Robot>();
            } else {
                throw new MissingComponentException("No player character in scene!");
            }

            if (FindObjectOfType<Human>()) {
                _humanCharacter = FindObjectOfType<Human>();
            } else {
                throw new MissingComponentException("No human character in scene!");
            }
            _humanCharacter.gameObject.SetActive(false);

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

            if (GetComponent<AudioSource>()) {
                audioSources = GetComponents<AudioSource>();
            }

            //Initialize the dictionaries
            soundDictionary = new Dictionary<string, AudioClip[]>();
            for (int i = 0; i < soundLibraries.Length; ++i) {
                soundDictionary.Add(soundLibraries[i].name, soundLibraries[i].sounds);
            }

            indicatorDictionary = new Dictionary<string, Sprite>();
            for (int i = 0; i < indicatorSprites.Length; ++i) {
                indicatorDictionary.Add(indicatorSprites[i].type, indicatorSprites[i].sprite);
            }

            for (int i = 0; i < dayObjectives[currentDay].objectives.Length; ++i) {
                if (dayObjectives[currentDay].objectives[i] != null) {
                    dayObjectives[currentDay].objectives[i].ObjectiveChange += OnObjectiveChanged;
                }
            }

            if (music != null) PlayGlobalSound(music, 1, true);
        }//Start

        private void Start() {
            mainUI.UpdateObjectivesList(dayObjectives[currentDay].objectives);
            OnDayStart();
        }//Start

        private void Update() {
            //Iterate human timer here
            if (currentTime < timeToSpawn) {
                currentTime += Time.deltaTime;
                if (currentTime >= timeToSpawn) {
                    dayObjectives[currentDay].path.gameObject.SetActive(true);
                    _humanCharacter.TeleportIn(dayObjectives[currentDay].path);
                }
            }
            humanUI.UpdateValues((int)(timeToSpawn - currentTime), _humanCharacter.anger, _humanCharacter.satisfaction);

            if (Input.GetKeyDown(KeyCode.F)) {
                if (Time.timeScale != 0) {
                    if (Input.GetKey(KeyCode.LeftShift)) {
                        Time.timeScale = 30;
                    } else {
                        Time.timeScale = 3;
                    }
                }
            } else if (Input.GetKeyUp(KeyCode.F)) {
                if (Time.timeScale != 0) Time.timeScale = 1;
            }
        }//Update

        private void OnDayStart() {
            if (currentDay >= dayObjectives.Length) {
                Ending();
            } else {
                _playerCharacter.Reset();
                mainUI.FadeIn("Day " + (currentDay + 1) + " start!");
                StorageObject[] storageObjects = FindObjectsOfType<StorageObject>();

                for (int i = 0; i < storageObjects.Length; ++i) {
                    storageObjects[i].RestockObjects();
                }

                for (int i = 0; i < dayObjectives[currentDay].spawners.Length; ++i) {
                    dayObjectives[currentDay].spawners[i].Spawn();
                }

                for (int i = 0; i < dayObjectives[currentDay].objectives.Length; ++i) {
                    dayObjectives[currentDay].objectives[i].Setup();
                    dayObjectives[currentDay].objectives[i].MarkIncomplete();
                }

                currentTime = 0f;
                timeToSpawn = dayObjectives[currentDay].timeToArrival;
                humanUI.UpdateValues((int)(timeToSpawn - currentTime));
            }
        }//OnDayStart

        private void OnDayEnd() {
            _humanCharacter.gameObject.SetActive(false);
            mainUI.FadeOut("Day " + (currentDay + 1) + " complete!"); 
        }//OnDayEnd

        public void OnNextDay() {
            ++currentDay;
            OnDayStart(); 
        }//OnNextDay

        public AudioClip GetRandomSound(string key) {
            if (soundDictionary.ContainsKey(key)) {
                int random = Random.Range(0, soundDictionary[key].Length - 1);
                return soundDictionary[key][random];
            } else {
                Debug.LogWarning("No sound under key " + key + " was found and nothing was returned");
                return null;
            }
        }//GetRandomSound

        private void Ending() {
            Application.LoadLevel("EndScene");
        }//Ending

        public void TriggerDayEnd() {
            OnDayEnd(); 
        }//TriggerDayEnd

        public void TriggerGameOver() {
            mainUI.ShowGameOver(); 
            Pause(); 
        }//TriggerGameOver

        public void ScreenShake(int shakes = 30) {
            if (FindObjectOfType<ScreenShake>()) {
                FindObjectOfType<ScreenShake>().Shake(shakes);
            }
        }//ScreenShake

        public AudioClip GetSound(string key, int index = 0) {
            if (soundDictionary != null && soundDictionary.ContainsKey(key) && index < soundDictionary[key].Length) {
                return soundDictionary[key][index];
            } else {
                Debug.LogWarning("No sound under key " + key + ", index " + index + " was found and nothing was returned");
                return null;
            }
        }//GetSound

        public void PlayGlobalSound(AudioClip clip, int source = 0, bool loop = false) {
            if (audioSources != null && source < audioSources.Length) {
                if (loop) {
                    audioSources[source].clip = clip;
                    audioSources[source].Play();
                } else {
                    audioSources[source].PlayOneShot(clip);
                }
            }
        }//PlayGlobalSound

        public void StopGlobalSound(int source = 0) {
            if (audioSources != null && source < audioSources.Length) {
                audioSources[source].Stop();
            }
        }//StopGlobalSound

        public void OnObjectiveChanged() {
            mainUI.UpdateObjectivesList(dayObjectives[currentDay].objectives);
        }//OnObjectiveChanged

        public void Pause() {
            if (Time.timeScale == 0) {
                Time.timeScale = 1;
                ResumeAllAudio();
            } else {
                Time.timeScale = 0;
                PauseAllAudio();
            }
        }//Pause

        private void PauseAllAudio() {
            AudioSource[] sources = FindObjectsOfType<AudioSource>();
            for (int i = 0; i < sources.Length; ++i) {
                sources[i].Pause();
            }
        }//PauseAllAudio

        private void ResumeAllAudio() {
            AudioSource[] sources = FindObjectsOfType<AudioSource>();
            for (int i = 0; i < sources.Length; ++i) {
                sources[i].UnPause();
            }
        }//ResumeAllAudio

        public void SetPickup(PickupObject pickup) {
            pickupUI.UpdateObject(pickup);
        }//SetPickup

        public void HidePickupUI() {
            pickupUI.Hide();
        }//HidePickupUI

        public void SetMessageText(string text, Color color, float duration = 3.5f) {
            mainUI.SetMessageText(text, color, duration);
        }//SetMessageText

        public Sprite GetIndicatorSprite(string key) {
            if (indicatorDictionary.ContainsKey(key)) {
                return indicatorDictionary[key];
            } else {
                Debug.LogWarning("No indicator sprite under key " + key + " was found and nothing was returned");
                return null;
            }
        }//GetIndicatorSprite

        private void OnDestroy() {
            for (int i = 0; i < dayObjectives.Length; ++i) {
                for (int j = 0; j < dayObjectives[i].objectives.Length; ++j) {
                    if (dayObjectives[i].objectives[j] != null) {
                        dayObjectives[i].objectives[j].ObjectiveChange -= OnObjectiveChanged;
                    }
                }
            }
        }//OnDestroy
    }//GameHandler
}//Relax
