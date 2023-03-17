using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameOverScript : MonoBehaviour
{
    enum OptionType {RETRY, MAINMENU}
    private OptionType selected = OptionType.RETRY;
    private Transform pointer;
    private bool active = false;
    private void Start()
    {
        pointer = transform.Find("Pointer");
        pointer.gameObject.SetActive(false);
        DontDestroyOnLoad(gameObject);
    }

    public void MoveDown()
    {
        pointer.transform.Translate(new Vector3(200.0f, -100.0f, 0));
    }

    public void MoveUp()
    {
        pointer.transform.Translate(new Vector3(-200.0f, 100.0f, 0));

    }
    public void SetActive(bool _active)
    {
        active = _active;
        pointer.gameObject.SetActive(_active);

    }

    void Update()
    {
        if (!active)
        {
            return;
        }
        if ((UnityEngine.Input.GetKeyDown(KeyCode.DownArrow) || UnityEngine.Input.GetKeyDown(KeyCode.S)) && selected == OptionType.RETRY)
        {
            MoveDown();
            selected = OptionType.MAINMENU;
        }
        else if ((UnityEngine.Input.GetKeyDown(KeyCode.UpArrow) || UnityEngine.Input.GetKeyDown(KeyCode.W)) && selected == OptionType.MAINMENU)
        {
            MoveUp();
            selected = OptionType.RETRY;
        }
        else if (UnityEngine.Input.GetKeyDown(KeyCode.Return))
        {
            switch (selected)
            {
                case OptionType.RETRY:
                    GameObject.FindGameObjectWithTag("LogicManager").GetComponent<LogicScript>().RestartGame();
                    break;

                case OptionType.MAINMENU:
                    GameObject.FindGameObjectWithTag("LogicManager").GetComponent<LogicScript>().ReturnToMenu();
                    break;

                default:
                    break;
            }
        }
    }
}
