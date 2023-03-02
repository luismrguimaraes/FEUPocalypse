using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    private int currentLevel;
    private int currentExperience;
    private int totalExperience;
    private int nextLevelExperience;

    // Start is called before the first frame update

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
        if (currentExperience >= nextLevelExperience) // Level up
        {
            currentExperience = -nextLevelExperience;
            currentLevel++;
        }
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
