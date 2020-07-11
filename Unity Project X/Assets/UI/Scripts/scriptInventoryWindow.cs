using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptInventoryWindow : MonoBehaviour
{
    public GameObject inventoryWindow; 
    GameObject gameController;
    public bool inventoryIsActive;

    void Start(){
        gameController = GameObject.FindWithTag("GameController");
        inventoryIsActive=false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I)){
            if(gameController.GetComponent<ScriptGameController>().getGameState() != 0){
                if(inventoryWindow.activeSelf){
                    closeInventory();
                }
                else{
                    openInventory();
                }
            }
        }
    }

    public void closeInventory(){
        inventoryIsActive=false;
        inventoryWindow.SetActive(false);
    }

    public void openInventory(){
        inventoryIsActive=true;
        inventoryWindow.SetActive(true);
    }
}
