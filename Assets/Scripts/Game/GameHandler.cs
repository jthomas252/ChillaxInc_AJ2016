using UnityEngine;
using System.Collections;
using Relax.Objects.Characters; 

namespace Relax.Game {
    public class GameHandler : MonoBehaviour {
        private Robot _playerCharacter;
        public Robot playerCharacter {
            get {
                return _playerCharacter; 
            }
        }

        private void Start() {
            if (FindObjectOfType<Robot>()) {
                _playerCharacter = FindObjectOfType<Robot>(); 
            } else {
                throw new MissingComponentException("No player character in scene!");
            }


        }//Start

        private void Update() {

        }//Update

        public void Pause() {
            if (Time.timeScale == 0) {
                Time.timeScale = 1;
            } else {
                Time.timeScale = 0; 
            }
        }//Pause
    }//GameHandler
}
