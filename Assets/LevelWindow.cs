using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelWindow : MonoBehaviour
{

    private Text levelText;
    private Slider slider;
    private LevelSystem levelSystem;

    void Awake()
    {
        levelText = transform.Find("LevelText").GetComponent<Text>();
        slider = transform.Find("ExperienceBar").GetComponent<Slider>();

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

    public void SetLevelSystem(LevelSystem _levelSystem)
    {
        levelSystem = _levelSystem;

        // Update the initial values
        SetLevelNumber(levelSystem.GetLevelNumber());
        SetExperienceBarSize(levelSystem.GetExperienceNormalized());

        // Subscribe to changed events
        levelSystem.OnExperienceChanged += LevelSystem_OnExperienceChanged;
        levelSystem.OnLevelChanged += LevelSystem_OnLevelChanged;
    }

    private void LevelSystem_OnExperienceChanged(object sender, System.EventArgs e)
    {
        // Experience changed, update bar size

        SetExperienceBarSize(levelSystem.GetExperienceNormalized());
    }

    private void LevelSystem_OnLevelChanged(object sender, System.EventArgs e)
    {
        // Level changed, update text

        SetLevelNumber(levelSystem.GetLevelNumber());
    }
}
