using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptPauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI; 
    GameObject gameController;

    void Start(){
        gameController = GameObject.FindWithTag("GameController");
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(gameController.GetComponent<ScriptGameController>().getGameState() == 0){
                ResumeGame();
            }
            else{
                PauseGame();
            }
        }
    }

    public void ResumeGame(){
        gameController.GetComponent<ScriptGameController>().pauseGameStateTrigger();
        pauseMenuUI.SetActive(false);
    }

    public void PauseGame(){
        gameController.GetComponent<ScriptGameController>().pauseGameStateTrigger();
        pauseMenuUI.SetActive(true);
    }

    public void OptionsButton(){
        Debug.Log("Opening Options");
    }

    public void MenuButton(){
        //gameController.GetComponent<ScriptGameController>().pauseGameStateTrigger();
        Debug.Log("Going to Main Menu");
    }

    public void QuitButton(){
        Debug.Log("Quitting Game");
    }
}
