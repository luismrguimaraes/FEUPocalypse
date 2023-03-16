using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    enum MenuOptionType { Start, Instructions, Quit };
    public GameObject mainMenuCanvas;
    public GameObject instructionsCanvas;
    Transform zombieSelector;
    private MenuOptionType selectedOption = MenuOptionType.Start;
    private bool ifInstructionPage = false;
    void Start()
    {
        zombieSelector = transform.Find("MainMenuCanvas").Find("ZombieSelector");
        instructionsCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (ifInstructionPage) // For Instruction Page
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.Escape) || UnityEngine.Input.GetKeyDown(KeyCode.Backspace))
            {
                instructionsCanvas.SetActive(false);
                mainMenuCanvas.SetActive(true);
                ifInstructionPage = false;
            }
            return;
        }
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
                    mainMenuCanvas.SetActive(false);
                    instructionsCanvas.SetActive(true);
                    ifInstructionPage = true;
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
