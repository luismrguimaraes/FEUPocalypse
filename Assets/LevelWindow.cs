using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelWindow : MonoBehaviour
{

    private Text levelText;
    private Slider slider;

    void Awake()
    {
        levelText = transform.Find("LevelText").GetComponent<Text>();
        slider = transform.Find("ExperienceBar").GetComponent<Slider>();

        SetExperienceBarSize(0.5f);
        SetLevelNumber(8);
    }

    private void SetLevelNumber(int levelNumber)
    {
        levelText.text = "LEVEL\n" + levelNumber;
    }

    private void SetExperienceBarSize(float experienceNormalized) // float from 0 to 1
    {
        slider.value = experienceNormalized;
        slider.maxValue = 1;
    }

}
