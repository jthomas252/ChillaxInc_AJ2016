using UnityEngine;
using System.Collections;
using Relax.Objects.Waypoints;
using Relax.Utility;
using Relax.Game; 

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

        public float parseTime = 2f;
        public float lookInterval = 1f;
        public float lookRange = 10f;
        private bool examining = false; 
        private HumanWalkPath path;
        private float currentTime;
        private int currentObjective;
        private ObjectiveStatus[] objectives; 

        private void BeginPathing() {
            if (navAgent != null && path != null) {
                navAgent.SetDestination(path.GetCurrentNodePoint());
            }
        }//BeginPathing

        protected new void Update() {
            if (!examining && navAgent.remainingDistance != Mathf.Infinity && navAgent.remainingDistance == 0f) {
                if (path.HasNextNode()) {
                    examining = true; 
                    objectives = path.GetObjectives(); 
                    currentObjective = 0; 
                    currentTime = 0f;
                } else {
                    Top.GAME.TriggerDayEnd(); 
                }
            } else if (examining && objectives != null) {
                if (currentObjective >= objectives.Length) {
                    examining = false;
                    OnFinishEvaluation(); 
                } else {
                    if (currentTime <= 0f) {
                        objectives[currentObjective].InspectObject();
                        currentObjective++; 
                        currentTime = parseTime; 
                    } else {
                        currentTime -= Time.deltaTime; 
                    }
                }
            } else if (currentTime <= 0f) {
                int layerMask = 1 << LayerMask.NameToLayer("OBJECT");
                Collider[] objects = Physics.OverlapSphere(transform.position, lookRange, layerMask);
                for (int i = 0; i < objects.Length; ++i) {
                    Reaction reaction = objects[i].GetComponent<Reaction>();
                    if (reaction != null && !reaction.reactedTo) {
                        int rayMask = 1 << LayerMask.NameToLayer("WALL");
                        RaycastHit rayHit;
                        if (!Physics.Linecast(transform.position, reaction.transform.position, out rayHit, rayMask)) {
                            if (reaction.isNegative) {
                                IncreaseAnger(reaction.React(), true);
                                Debug.DrawLine(transform.position, reaction.transform.position, Color.red, 4f);
                            } else {
                                IncreaseSatisfaction(reaction.React(), true);
                                Debug.DrawLine(transform.position, reaction.transform.position, Color.green, 4f);
                            }
                        }
                    }
                }
                currentTime = lookInterval; 
            } else {
                currentTime -= Time.deltaTime;
            }

            base.UpdateAnimation();
            base.Update();
        }//Update

        private void OnFinishEvaluation() {
            navAgent.SetDestination(path.GetNextNode());
        }//OnFinishEvaluation

        public void TeleportIn(HumanWalkPath _path) {
            transform.position = _path.transform.position;
            gameObject.SetActive(true);
            Top.GAME.ScreenShake(10);
            PlaySound(Top.GAME.GetRandomSound("teleport")); 
            path = _path; 

            BeginPathing();
        }//TeleportIn

        public void IncreaseSatisfaction(float amnt, bool playSound = false) {
            _satisfaction += amnt; 
            if (playSound) PlaySound(Top.GAME.GetRandomSound("human_pos"));
            if (satisfaction < 0f) _satisfaction = 0f;
            if (satisfaction > 1f) _satisfaction = 1f; 
        }//IncreaseSatisfaction

        public void IncreaseAnger(float amnt, bool playSound = false) {
            Top.GAME.ScreenShake(10);
            _anger += amnt;
            if (playSound) PlaySound(Top.GAME.GetRandomSound("human_neg"));
            if (anger < 0f) _anger = 0f; 
            if (anger >= 1f) Top.GAME.TriggerGameOver();
        }//IncreaseAnger
    }//Human
}//Relax