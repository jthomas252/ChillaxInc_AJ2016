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
        public RectTransform purchasePanel;
        public RectTransform restartConfirmPanel;
        public RectTransform gameOverPanel;
        public CanvasGroup fadePanel;
        public Text newDayText;
        public Text messageText;

        private struct ObjectiveDisplay {
            public Image image;
            public Text text;

            public ObjectiveDisplay(Image i, Text t) {
                image = i; text = t;
            }
        }
        private List<ObjectiveDisplay> objectiveDisplayList;
        private float currentTime;
        private float messageDuration;

        public void Start() {
            if (objectivePanel == null) {
                throw new MissingComponentException("The objective panel is not set up properly");
            }
            currentTime = 0f; messageDuration = 0f;
        }//Start

        public void Update() {
            if (currentTime < messageDuration) {
                currentTime += Time.deltaTime;
                messageText.color = new Color(
                    messageText.color.r,
                    messageText.color.g,
                    messageText.color.b,
                    1f - (currentTime / messageDuration));
            }
        }//Update

        //UI Buttons
        public void OnPauseButton() {
            Top.GAME.Pause();

            if (pauseGroup != null) {
                if (Time.timeScale == 1) {
                    pauseGroup.gameObject.SetActive(false);
                } else {
                    pauseGroup.gameObject.SetActive(true);
                }
            }

            Top.GAME.PlayGlobalSound(Top.GAME.GetRandomSound("ui_click"));
        }//OnPauseButton

        public void ShowGameOver() {
            if (gameOverPanel != null) gameOverPanel.gameObject.SetActive(true);
        }//ShowGameOver

        public void SetMessageText(string text, Color color, float duration) {
            if (messageText != null) {
                messageText.text = text;
                messageText.color = color;
                messageDuration = duration;
                currentTime = 0f;
            }
        }//SetMessageText

        public void UpdateObjectivesList(ObjectiveStatus[] objectives) {
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

        public void OnRestartButton() {
            if (restartConfirmPanel != null) restartConfirmPanel.gameObject.SetActive(true);
            Top.GAME.PlayGlobalSound(Top.GAME.GetRandomSound("ui_click"));
        }//OnRestartButton

        public void OnRestartCancel() {
            if (restartConfirmPanel != null) restartConfirmPanel.gameObject.SetActive(false);
            Top.GAME.PlayGlobalSound(Top.GAME.GetRandomSound("ui_click"));
        }//OnRestartCancel

        public void OnRestartConfirm() {
            OnPauseButton();
            Application.LoadLevel(Application.loadedLevel);
        }//OnRestartConfirm

        public void FadeIn(string text) {
            if (newDayText != null) newDayText.text = text;
            StartCoroutine(Fade(true));
        }//FadeIn

        public void FadeOut(string text) {
            if (newDayText != null) newDayText.text = text;
            Top.GenericEvent callback = new Top.GenericEvent(Top.GAME.OnNextDay);
            StartCoroutine(Fade(false, callback));
        }//FadeOut

        private IEnumerator Fade(bool dir = false, Top.GenericEvent callback = null) {
            if (fadePanel != null) {
                for (float i = 0; i < 100; ++i) {
                    yield return new WaitForSeconds(0.01f);
                    if (dir) {
                        fadePanel.alpha = Mathf.Lerp(1f, 0f, (i / 100));
                    } else {
                        fadePanel.alpha = Mathf.Lerp(0f, 1f, (i / 100));
                    }
                }
            }
            if (callback != null) {
                callback();
                callback = null;
            }
            StopCoroutine(Fade());
        }//Fade
    }//MainGameUIController
}//Relax