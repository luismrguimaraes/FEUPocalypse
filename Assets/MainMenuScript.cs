using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    enum MenuOptionType { Start, Instructions, Quit };
    Transform zombieSelector;
    private MenuOptionType selectedOption = MenuOptionType.Start;
    void Start()
    {
        zombieSelector = transform.Find("ZombieSelector");
    }

    // Update is called once per frame
    void Update()
    {
        if (UnityEngine.Input.GetKeyDown(KeyCode.DownArrow) || UnityEngine.Input.GetKeyDown(KeyCode.S))
        {
            switch (selectedOption)
            {
                case MenuOptionType.Start:
                    zombieSelector.transform.Translate(new Vector3(-2.25f, -1.75f, 0));
                    selectedOption = MenuOptionType.Instructions;
                    break;
                case MenuOptionType.Instructions:
                    zombieSelector.transform.Translate(new Vector3(2.25f, -1.75f, 0));
                    selectedOption = MenuOptionType.Quit;

                    break;
                case MenuOptionType.Quit:
                    break;
                default:
                    break;
            }
        }
        if (UnityEngine.Input.GetKeyDown(KeyCode.UpArrow) || UnityEngine.Input.GetKeyDown(KeyCode.W))
        {

            switch (selectedOption)
            {
                case MenuOptionType.Start:
                    
                    break;
                case MenuOptionType.Instructions:
                    selectedOption = MenuOptionType.Start;
                    zombieSelector.transform.Translate(new Vector3(2.25f, 1.75f, 0));
                    break;
                case MenuOptionType.Quit:
                    selectedOption = MenuOptionType.Instructions;
                    zombieSelector.transform.Translate(new Vector3(-2.25f, 1.75f, 0));
                    break;
                default:
                    break;
            }
        }
        if (UnityEngine.Input.GetKeyDown(KeyCode.Return))
        {

            switch (selectedOption)
            {
                case MenuOptionType.Start:
                    SceneManager.LoadScene("DontDestroyOnLoad");
                    break;
                case MenuOptionType.Instructions:
                    break;
                case MenuOptionType.Quit:

                    Application.Quit(); // Will close a running application.
                                        // However, while this works to end a built application,
                                        // Application Quit is ignored when running the game in Play Mode in the editor.
                    Debug.Log("Quitting Game!");
                    break;
                default:
                    break;
            }
        }
    }
}
