using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicScript : MonoBehaviour
{
    public float experience = 0f;
    private LevelSystem levelSystem;
    [SerializeField] private LevelWindow levelWindow;

    void Awake()
    {
        levelSystem = new LevelSystem();
        levelWindow.SetLevelSystem(levelSystem);

    }

    public void GainXP(int xp)
    {
        levelSystem.AddExperience(xp);
    }
}
