using UnityEngine;
using System.Collections;
using Relax.Game; 

namespace Relax.Utility {
    public class Top {
        private GameHandler _GAME; 
        public GameHandler GAME {
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
}
