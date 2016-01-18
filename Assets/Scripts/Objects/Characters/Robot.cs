using UnityEngine;
using System.Collections;

namespace Relax.Objects.Characters {
    public class Robot : Character {
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
