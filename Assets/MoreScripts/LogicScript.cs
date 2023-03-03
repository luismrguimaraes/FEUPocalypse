using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicScript : MonoBehaviour
{
    public float experience = 0f;
    private LevelSystem levelSystem;
    [SerializeField] private GameObject canvas;

    void Awake()
    {
        levelSystem = gameObject.AddComponent<LevelSystem>();
        levelSystem.Init();

        LevelWindow levelWindow = Instantiate(canvas, transform.position, transform.rotation).GetComponentInChildren<LevelWindow>();

        levelWindow.Init();
        levelWindow.SetLevelSystem(levelSystem);
    }
    


    public void GainXP(int xp)
    {
        levelSystem.AddExperience(xp);
    }
}
