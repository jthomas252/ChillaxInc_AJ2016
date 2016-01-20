using UnityEngine;
using System.Collections;
using System.Collections.Generic; 
using UnityEngine.UI; 
using Relax.Utility; 
using Relax.Game; 

namespace Relax.Interface {
    public class MainGameUIController : MonoBehaviour {
        public CanvasGroup pauseGroup; 
        public RectTransform objectivePanel; 
        public Image objectiveIncompleteImage; 
        public Image objectiveCompleteImage; 
        public Text objectiveTextBase; 
        public Color objectiveIncompleteColor; 
        public Color objectiveCompleteColor; 
        public float objectiveImageOffset = 32f;
        public float objectiveSpacing = 48f;

        private struct ObjectiveDisplay {
            public Image image; 
            public Text text; 

            public ObjectiveDisplay (Image i, Text t) {
                image = i; text = t; 
            }
        }
        private List<ObjectiveDisplay> objectiveDisplayList; 

        public void Start() {
            if (objectivePanel == null) {
                throw new MissingComponentException("The objective panel is not set up properly"); 
            }
        }//Start

        public void OnPauseButton() {
            Top.GAME.Pause(); 
            
            if (pauseGroup != null) {
                if (Time.timeScale == 1) {
                    pauseGroup.gameObject.SetActive(false);
                } else {
                    pauseGroup.gameObject.SetActive(true);
                }
            }
        }//OnPauseButton

        public void UpdateObjectivesList(Objective[] objectives) {
            if (objectiveDisplayList == null) {
                objectiveDisplayList = new List<ObjectiveDisplay>();
            } 

            //Do some error checking here to make sure that these elements aren't null.
            for (int i = 0; i < objectives.Length; ++i) {
                if (i >= objectiveDisplayList.Count) {
                    Text newText = GameObject.Instantiate<Text>(objectiveTextBase); 
                    newText.text = objectives[i].description;

                    Image newImage;
                    if (objectives[i].complete) {
                        newImage = GameObject.Instantiate<Image>(objectiveCompleteImage);
                        newText.color = objectiveCompleteColor;
                    } else {
                        newImage = GameObject.Instantiate<Image>(objectiveIncompleteImage);
                        newText.color = objectiveIncompleteColor; 
                    }

                    newImage.rectTransform.SetParent(objectivePanel, false); 
                    newText.rectTransform.SetParent(objectivePanel, false);

                    newImage.rectTransform.position = new Vector3(
                        newImage.rectTransform.position.x, 
                        newImage.rectTransform.position.y - (i * objectiveSpacing));

                    newText.rectTransform.position = new Vector3(
                        objectiveImageOffset + newText.rectTransform.position.x,
                        newText.rectTransform.position.y - (i * objectiveSpacing));

                    ObjectiveDisplay newObjective = new ObjectiveDisplay(newImage, newText);
                    objectiveDisplayList.Add(newObjective); 
                } else {
                    objectiveDisplayList[i].image.gameObject.SetActive(true);
                    objectiveDisplayList[i].text.gameObject.SetActive(true);

                    if (objectives[i].complete) {
                        objectiveDisplayList[i].image.sprite = objectiveCompleteImage.sprite; 
                        objectiveDisplayList[i].text.color = objectiveCompleteColor;
                    } else {
                        objectiveDisplayList[i].image.sprite = objectiveIncompleteImage.sprite;
                        objectiveDisplayList[i].text.color = objectiveIncompleteColor;
                    }

                    objectiveDisplayList[i].text.text = objectives[i].description; 
                }
            }

            //Turn off any display objectives that aren't needed.
            for (int i = objectives.Length; i < objectiveDisplayList.Count; ++i) {
                objectiveDisplayList[i].image.gameObject.SetActive(false);
                objectiveDisplayList[i].text.gameObject.SetActive(false);
            }
        }//UpdateObjectivesList
    }//MainGameUIController
}