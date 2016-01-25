using UnityEngine;
using System.Collections;

namespace Relax.Utility {
    public class Reaction : MonoBehaviour {
        private bool _reactedTo;
        public bool reactedTo {
            get {
                return _reactedTo;
            }
        }
        public float reactionAmount = 0.025f; 
        public bool isNegative = true; 

        public float React() {
            _reactedTo = true; 
            return reactionAmount; 
        }//React
    }//Reaction
}//Relax