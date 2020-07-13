using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioElementTree :  ScenarioElement
{
    bool spin = false;
    [SerializeField] private GameObject spawnsItem; 

    public override void onInteraction(){
        spin = !spin;
        this.takeDamage(50);
    }

    public override void onBreaking(){
        Debug.Log("Breaking "+name);
        GameObject item = Instantiate(spawnsItem, self.transform.position, Quaternion.identity);
        Destroy(item.GetComponent("scriptItemClass")); 
        item.AddComponent<scriptWoodResource>();
        item.GetComponent<scriptWoodResource>().self = item;
        Destroy(self);
    }

    // Update is called once per frame
    void Update()
    {
        if(spin){
            transform.Rotate(0,0,20);
        }
    }

    
}
