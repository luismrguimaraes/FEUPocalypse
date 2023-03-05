using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public string sceneToLoad;
    public Vector2 playerPosition;
    public VectorValue playerStorage;
    public bool isEntrance;

    private LogicScript logicScript;
    private SceneManagerScript sceneManagerScript;


    // Start is called before the first frame update
    void Start()
    {
        logicScript = GameObject.FindGameObjectWithTag("LogicManager").GetComponent<LogicScript>();
        logicScript.SceneTransitionOnStartUpdate();

        sceneManagerScript = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneManagerScript>();
        sceneManagerScript.SceneTransitionOnStartUpdate();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            if (isEntrance)
            {
                // Store position that player will be after exiting
                sceneManagerScript.SetPlayerPositionAfterExiting(transform.position + new Vector3(0, -2, 0));
                playerStorage.initialValue = playerPosition;

                // Store current Wave
                sceneManagerScript.StoreCurrentWave();
            }
            else
            {
                // Get the stored position
                playerStorage.initialValue = sceneManagerScript.GetPlayerPositionAfterExiting();

            }
            SceneManager.LoadScene(sceneToLoad);
            Input.ResetInputAxes();
        }
    }
}
