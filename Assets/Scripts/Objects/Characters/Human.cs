using UnityEngine;
using System.Collections;
using Relax.Objects.Waypoints;
using Relax.Utility;

namespace Relax.Objects.Characters {
    public class Human : Character {
        private float _anger = 0f;
        public float anger {
            get {
                return _anger; 
            }
        }

        private float _satisfaction = 0f;
        public float satisfaction {
            get {
                return _satisfaction;
            }
        }

        private HumanWalkPath path; 

        protected new void Update() {
            base.UpdateAnimation();
            base.Update();
        }//Update

        public void TeleportIn(HumanWalkPath _path) {
            gameObject.SetActive(true);
            PlaySound(Top.GAME.GetRandomSound("teleport")); 
            transform.position = _path.transform.position;
            path = _path; 
            navAgent.SetDestination(_path.GetCurrentNodePoint());
        }//TeleportIn
    }//Human
}//Relax