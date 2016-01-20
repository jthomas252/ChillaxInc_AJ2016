using UnityEngine;
using System.Collections;
namespace Relax.Interface {
    public class PurchaseUIController : MonoBehaviour {

        public void OnCloseButton() {
            gameObject.SetActive(false); 
        }//OnCloseButton
    }//PurchaseUIController
}//Relax
