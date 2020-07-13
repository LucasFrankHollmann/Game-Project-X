using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptInventoryWindow : MonoBehaviour
{
    public GameObject inventoryWindow; 
    
    GameObject gameController;
    public bool inventoryIsActive;
    public int currentInventorySlots = 2;
    private int maxInventorySlots = 10;

    void Start(){
        gameController = GameObject.FindWithTag("GameController");
        inventoryIsActive=false;
        updateInventoryCapacity();
    }

    void updateInventoryCapacity(){
        Transform grid = inventoryWindow.transform.Find("InventoryGrid");
        Transform[] slots = new Transform[10];
        for(int i = 0; i<10 ; i++){
            slots[i] = grid.GetChild(i);
            if(i>=currentInventorySlots){
                slots[i].gameObject.SetActive(false);
            }
        }
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
