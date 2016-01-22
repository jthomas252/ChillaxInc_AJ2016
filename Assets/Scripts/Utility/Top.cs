using UnityEngine;
using System.Collections;
using Relax.Game; 

namespace Relax.Utility {
    public class Top {
        public delegate void GenericEvent(); 

        public static float ANGER_COST_INCORRECT = 0.2f; 
        public static float ANGER_COST_FIRE      = 0.8f; 
        public static float ANGER_COST_TRASH     = 0.05f; 

        private static GameHandler _GAME; 
        public static GameHandler GAME {
            get {
                if (_GAME == null) {
                    if (GameObject.FindObjectOfType<GameHandler>()) {
                        _GAME = GameObject.FindObjectOfType<GameHandler>(); 
                    } else {
                        throw new MissingComponentException("No GameHandler in Scene");
                    }
                }
                return _GAME; 
            }
        }
    }//Top
}//Relax
