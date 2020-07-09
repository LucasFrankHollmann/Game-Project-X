using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scriptMovePoint : ScenarioElement
{

    bool spin = false;
    
    public override void onInteraction(){
        Debug.Log("Moving");
        spin = !spin;
        SceneManager.LoadScene("NewArea");
        Debug.Log("Moved to new area!");
    }

    void FixedUpdate(){
        if(spin){
            transform.Rotate(0,0,20);
        }
    }
}
