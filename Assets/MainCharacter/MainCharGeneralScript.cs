using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharGeneralScript : MonoBehaviour
{
    private MainCharMovementScript mainCharMovementScript;
    private MainCharCollectiblesScript mainCharCollectiblesScript;

    // Start is called before the first frame update
    void Start()
    {
        mainCharMovementScript = GetComponent<MainCharMovementScript>();
        mainCharCollectiblesScript = GetComponent<MainCharCollectiblesScript>();

        DontDestroyOnLoad(gameObject);
    }

    public void OnSceneTransitionStart()
    {
        mainCharMovementScript.OnSceneTransitionStart();
        mainCharCollectiblesScript.OnSceneTransitionStart();
    }
    
}
