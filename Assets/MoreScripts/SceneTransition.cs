using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public string sceneToLoad;
    public Vector2 playerPosition;
    public VectorValue playerStorage;

    public LogicScript logicScript;

    // Start is called before the first frame update
    void Start()
    {
        logicScript = GameObject.FindGameObjectWithTag("LogicManager").GetComponent<LogicScript>();
        logicScript.SceneTransitionLogicUpdate();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Scene Transition");
        if (collider.CompareTag("Player"))
        {
            playerStorage.initialValue = playerPosition;
            SceneManager.LoadScene(sceneToLoad);
            Input.ResetInputAxes();
        }
    }
}
