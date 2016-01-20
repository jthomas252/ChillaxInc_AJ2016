using UnityEngine;
using System.Collections;

namespace Relax.Game {
    [System.Serializable]
    public class Objective {
        public bool complete;
        public string description;

        public Objective(bool state, string desc) {
            complete = state;
            description = desc;
        }//Objective
    }//Objective
}
