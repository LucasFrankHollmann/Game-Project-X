using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementoTesteController :  ScenarioElement
{
    bool spin = false;

    public override void onInteraction(){
        spin = !spin;
    }

    // Update is called once per frame
    void Update()
    {
        if(spin){
            transform.Rotate(0,0,20);
        }
    }

    
}
