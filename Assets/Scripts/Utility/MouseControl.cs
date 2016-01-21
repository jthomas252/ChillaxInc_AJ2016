using UnityEngine;
using UnityEngine.EventSystems; 
using System.Collections;
using Relax.Interface; 
using Relax.Objects.Interactables;

namespace Relax.Utility {
    public class MouseControl : MonoBehaviour {
        public Rect cameraBounds = new Rect(-40f,-40f,0f,0f);
        public Vector2 sizeLimit = new Vector2(1f,30f);
        public float scrollSpeed = 0.25f; 

        private Camera camera; 
        private Vector2 mouseLast;
        private bool mouseDown; 
        private float staticHeight; 
        private TooltipUIController tooltipUI; 
        private ObjectUIController objectUI; 

        void Start() {
            if (GetComponent<Camera>()) {
                camera = GetComponent<Camera>();
            } else {
                throw new MissingComponentException("Camera movement script is not attached to a object with Camera");
            }

            if (FindObjectOfType<TooltipUIController>()) {
                tooltipUI = FindObjectOfType<TooltipUIController>(); 
            } else {
                throw new MissingComponentException("No Tooltip UI in Scene");
            }

            if (FindObjectOfType<ObjectUIController>()) {
                objectUI = FindObjectOfType<ObjectUIController>();
            } else {
                throw new MissingComponentException("No Object UI in Scene");
            }

            mouseDown = false; 
            staticHeight = transform.position.y; 
        }//Start

        void Update() {
            bool hitObject = false;
            if (!EventSystem.current.IsPointerOverGameObject()) {
                RaycastHit rayHit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out rayHit) && rayHit.transform.gameObject.GetComponent<ObjectTooltipInfo>()) {
                    hitObject = true;
                    ObjectTooltipInfo info = rayHit.transform.gameObject.GetComponent<ObjectTooltipInfo>();
                    if (Input.GetMouseButtonDown(0) && info.canInteract) {
                        objectUI.SetObject(info);
                    } else if (objectUI.targetObject != rayHit.transform.gameObject) {
                        tooltipUI.SetObject(info);
                    }
                }

                if (Input.GetMouseButton(0) && !hitObject) {
                    objectUI.UnsetObject();
                }
            }

            if (Input.GetMouseButtonDown(0)) {
                mouseLast = Input.mousePosition;
                mouseDown = true;
            }  

            if (Input.GetMouseButtonUp(0)) {
                mouseDown = false;
            }

            //Mouse scroll to zoom. 
            float scroll = Input.GetAxis("Mouse ScrollWheel"); 
            if (scroll > 0f && camera.orthographicSize > sizeLimit.x) {
                camera.orthographicSize -= 1f; 
            } else if (scroll < 0f && camera.orthographicSize < sizeLimit.y) {
                camera.orthographicSize += 1f;
            }
            float zoomRatio = (camera.orthographicSize + sizeLimit.x) / sizeLimit.y; 

            //Camera Movement
            if (mouseDown) {
                transform.Translate(new Vector3(
                    (scrollSpeed * zoomRatio) * (mouseLast.x - Input.mousePosition.x),
                    0f,
                    (scrollSpeed * zoomRatio) * (mouseLast.y - Input.mousePosition.y)));
                transform.position = new Vector3(transform.position.x,staticHeight,transform.position.z);
                mouseLast = Input.mousePosition;

                if (transform.position.x < cameraBounds.x) {
                    transform.position = new Vector3(cameraBounds.x, transform.position.y, transform.position.z);
                } else if (transform.position.x > cameraBounds.width) {
                    transform.position = new Vector3(cameraBounds.width, transform.position.y, transform.position.z);
                }

                if (transform.position.z < cameraBounds.y) {
                    transform.position = new Vector3(transform.position.x, transform.position.y, cameraBounds.y);
                } else if (transform.position.z > cameraBounds.height) {
                    transform.position = new Vector3(transform.position.x, transform.position.y, cameraBounds.height);
                }
            }
        }//Update
    }//CameraMovement
}