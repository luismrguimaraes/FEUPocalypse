using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    public event EventHandler OnExperienceChanged;
    public event EventHandler OnLevelChanged;
    public int fullHpRecoverLevelInterval = 5;

    private int currentLevel;
    private int currentExperience;
    private int totalExperience;
    private int nextLevelExperience;
    private LogicScript logicScript;


    public void Init()
    {
        currentLevel = 1;
        currentExperience = 0;
        totalExperience = 0;
        nextLevelExperience = 100;

        logicScript = GameObject.FindGameObjectWithTag("LogicManager").GetComponent<LogicScript>();
    }

    public void AddExperience(int _xpReceived)
    {
        currentExperience += _xpReceived;
        totalExperience += _xpReceived;
        if (currentExperience >= nextLevelExperience) // Level up
        {
            currentExperience -= nextLevelExperience;
            currentLevel++;
            nextLevelExperience += 50;
            if (currentLevel % fullHpRecoverLevelInterval == 0)
            {
                logicScript.RestoreToMaxHealth();
            }
            if (OnLevelChanged != null) OnLevelChanged(this, EventArgs.Empty);

        }
        if (OnExperienceChanged != null) OnExperienceChanged(this, EventArgs.Empty);
    }

    public int GetTotalExperience()
    {
        return totalExperience;
    }

    public int GetLevelNumber()
    {
        return currentLevel;
    }

    public float GetExperienceNormalized()
    {
        return (float)currentExperience / nextLevelExperience;
    }
}
