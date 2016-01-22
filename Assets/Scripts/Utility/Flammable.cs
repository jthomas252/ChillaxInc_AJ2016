using UnityEngine;
using System.Collections;
using Relax.Objects.Interactables;

namespace Relax.Utility {
    public class Flammable : MonoBehaviour {
        public delegate void FireEvent(); 
        public event Top.GenericEvent OnIgnite;
        public event Top.GenericEvent OnContain; 

        public float spreadRange = 3.5f;
        public float timeTillSpread = 6f; 
        public bool startOnFire = false; 

        private bool _isOnFire = false;
        public bool isOnFire {
            get {
                return _isOnFire;
            }
        }
        private float currentTime = 0f; 

        private void Start() { 
            if (startOnFire) Ignite(); 
        }//Start

        public void Ignite(bool playSound = true) {
            if (Top.GAME.firePrefab != null && !isOnFire) {
                GameObject fire = GameObject.Instantiate(
                    Top.GAME.firePrefab, 
                    transform.position + new Vector3(0f,1f), 
                    Quaternion.identity) as GameObject;
                fire.transform.SetParent(transform);
                if (fire.GetComponent<Fire>()) fire.GetComponent<Fire>().OnContained += Contain;
                if (OnIgnite != null) OnIgnite(); 
                currentTime = 0f; 
                _isOnFire = true; 
                if (playSound) Top.GAME.PlayGlobalSound(Top.GAME.GetSound("fireExplode")); 
                Top.GAME.PlayGlobalSound(Top.GAME.GetSound("fireIdle"), 2, true);
            }
        }//Ignite

        private void Update() {
            if (isOnFire && currentTime < timeTillSpread) {
                currentTime += Time.deltaTime; 
                if (currentTime > timeTillSpread) Spread(); 
            }
        }//Update

        private void Spread() {
            currentTime = 0f; 
            Collider[] objects = Physics.OverlapSphere(transform.position, spreadRange);
            for (int i = 0; i < objects.Length; ++i) {
                if (objects[i].GetComponent<Flammable>()) objects[i].GetComponent<Flammable>().Ignite(false); 
            }
        }//Spread

        private void Contain() {
            if (OnContain != null) OnContain(); 
            _isOnFire = false;
            if (FindObjectsOfType<Fire>().Length == 1) {
                Top.GAME.StopGlobalSound(2);
            } 
        }//Contain
    }//Flammable
}//Relax