using UnityEngine;
using System.Collections;
using Relax.Objects.Pickups;
using Relax.Objects.Interactables; 

namespace Relax.Objects.Characters {
    public class Robot : Character {
        public PickupObject pickup; 

        protected void Start() {
            base.Start(); 
        }//Start

        protected void Update() {
            if (Input.GetMouseButtonDown(1)) {
                RaycastHit rayHit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out rayHit)) {
                    navAgent.SetDestination(rayHit.point);
                }
            }

            base.UpdateAnimation(); 
        }//Update
    }//Robot
}
