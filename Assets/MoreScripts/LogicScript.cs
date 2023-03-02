using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicScript : MonoBehaviour
{
    public float experience = 0f;
    private LevelSystem levelSystem;
    [SerializeField] private LevelWindow levelWindow;
    //private int level = 0;
    // Start is called before the first frame update
    //void Start()
    //{
    //}

    //// Update is called once per frame
    //void Update()
    //{
        
    //}

    void Awake()
    {
        levelSystem = new LevelSystem();
        levelWindow.SetLevelSystem(levelSystem);

        //levelWindow

    }

    public void GainXP(int xp)
    {
        Debug.Log("gaining exp");
        levelSystem.AddExperience(xp);
    }
}
