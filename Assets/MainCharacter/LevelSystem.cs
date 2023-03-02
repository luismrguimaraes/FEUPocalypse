using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    public event EventHandler OnExperienceChanged;
    public event EventHandler OnLevelChanged;

    private int currentLevel;
    private int currentExperience;
    private int totalExperience;
    private int nextLevelExperience;


    public LevelSystem()
    {
        currentLevel = 1;
        currentExperience = 0;
        totalExperience = 0;
        nextLevelExperience = 100;
    }

    public void AddExperience(int _xpReceived)
    {
        currentExperience += _xpReceived;
        totalExperience += _xpReceived;
        if (currentExperience >= nextLevelExperience) // Level up
        {
            currentExperience = -nextLevelExperience;
            currentLevel++;
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
