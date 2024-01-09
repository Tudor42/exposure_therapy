using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Threading;

namespace ExposureThrepay
{
    public class LevelsManager : MonoBehaviour
    {
        [SerializeField]
        public GameObject selectLevelButton;
        [SerializeField]
        public GameObject levelDescriptionLabel;
        [SerializeField]
        public GameObject timerLabel;
        [SerializeField]
        public Button nextLevelButton;

        [SerializeField]
        List<string> levelTexts = new List<string>
            { "Static exposure", "Sound exposure", "Dog behind fence", "Dog walking" };
        List<string> levelDescriptions = new List<string>
            { "Look at a statue for some time.", "Listen to some barking.", "Experiece the feeling of a dog trying to come in your room.", "Experiece the feeling of a dog walking in your room." };
        List<string> sceneNames = new List<string>
            { "StaticExposure", "StaticExposure", "StaticExposure", "StaticExposure" };
        List<int> levelDuration = new List<int>
            { 1 * 3, 1 * 10, 60 * 10, 60 * 10 };

        int current_step = 0;

        private Coroutine countdownCoroutine;

        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            EnableAllLights();

            StartCountdown();
        }

        public void Start()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;

            updateLevelText(current_step);
        }

        void DisableAllLights()
        {
            Light[] lights = FindObjectsOfType<Light>();
            foreach (Light light in lights)
            {
                light.enabled = false;
            }
        }

        void EnableAllLights()
        {
            Light[] lights = FindObjectsOfType<Light>();
            foreach (Light light in lights)
            {
                light.enabled = true;
            }
        }

        public void SelectScene()
        {
           DisableAllLights();

           SceneManager.LoadScene(sceneNames[current_step], LoadSceneMode.Additive);
        }

        public void SetButtonText(string text)
        {
            Text buttonText = selectLevelButton.GetComponentInChildren<Text>();
            if (buttonText != null)
            {
                buttonText.text = text;
            }
            else
            {
                Debug.LogError("Text component not found in the children of the button.");
            }
        }

        public void SetLabelText(string text)
        {
            TextMeshProUGUI labelText = levelDescriptionLabel.GetComponent<TextMeshProUGUI>();
            if (labelText != null)
            {
                labelText.text = text;
            }
            else
            {
                Debug.LogError("TextMeshProUGUI component not found in the label.");
            }
        }

        private string ConvertToString(int time)
        {
            return (time / 60).ToString() + ":" + (time % 60).ToString();
        }

        public void SetTimerText(string time)
        {
            TextMeshProUGUI labelText = timerLabel.GetComponent<TextMeshProUGUI>();
            if (labelText != null)
            {
                labelText.text = time;
            }
            else
            {
                Debug.LogError("TextMeshProUGUI component not found in the label.");
            }
        }

        void StartCountdown()
        {
            StopCountdown();

            countdownCoroutine = StartCoroutine(Countdown(levelDuration[current_step]));
        }

        void StopCountdown()
        {
            if (countdownCoroutine != null)
            {
                StopCoroutine(countdownCoroutine);
                countdownCoroutine = null;
            }
        }

        void UnloadCurrentScene()
        {
            StartCoroutine(UnloadSceneAsync(() =>
            {
                Next();
            }));
        }

        IEnumerator UnloadSceneAsync(System.Action callback)
        {
            yield return new WaitForSeconds(0.1f);

            SceneManager.UnloadSceneAsync(sceneNames[current_step]);

            callback?.Invoke();
        }

        public void NextInLevel()
        {
            UnloadCurrentScene();
        }

        void LevelFinished()
        {
            if (nextLevelButton != null)
            {
                nextLevelButton.interactable = true;
            }
            SetTimerText("Finished!");
        }

        IEnumerator Countdown(int duration)
        {
            while (duration > 0)
            {
                SetTimerText(ConvertToString(duration));
                yield return new WaitForSeconds(1f);
                duration--;
            }
            LevelFinished();
        }

        public void updateLevelText(int current_step)
        {
            SetButtonText(levelTexts[current_step] + "\n" + (current_step + 1).ToString());
            SetLabelText(levelDescriptions[current_step]);
        }

        public void Next()
        {
            current_step = (current_step + 1) % levelTexts.Count;
            updateLevelText(current_step);

        }

        public void Prev()
        {
            current_step = current_step - 1;
            if (current_step < 0)
            {
                current_step = levelTexts.Count - 1;
            }
            updateLevelText(current_step);
        }

        void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
}
