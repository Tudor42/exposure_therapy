using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace ExposureThrepay
{
    public class LevelsStepManager : MonoBehaviour
    {
        [SerializeField]
        public GameObject selectLevelButton;

        [SerializeField]
        public GameObject levelDescriptionLabel;

        [SerializeField]
        List<string> levelTexts = new List<string>
            { "Photo exposure", "Sound exposure", "Dog behind fence", "Dog walking" };
        List<string> levelDescriptions = new List<string>
            { "Look at a photo for some time.", "Listen to some barking.", "Experiece the feeling of a dog trying to come in your room.", "Experiece the feeling of a dog walking in your room." };

        int current_step = 0;

        public void Start()
        {
            updateLevelText(current_step);
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
    }
}
