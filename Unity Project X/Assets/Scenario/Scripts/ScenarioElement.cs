using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScenarioElement : MonoBehaviour
{
    [SerializeField] private bool breakable = false;
    [SerializeField] private int hp = 100;

    public GameObject self;

    public void setInitValues(int hp, bool breakable){
        this.breakable = breakable;
        this.hp = hp;
    }
    
    public virtual void onInteraction(){
        Debug.Log("Trying to interact with "+this.name+", it has no OnInteraction override programmed!");
    }

    public virtual void onBreaking(){
        Destroy(self);
    }

    public virtual void takeDamage(int dmg){
        this.hp -= dmg;
        if(hp<=0){
            onBreaking();
        }
    }

    public int getHp(){
        return hp;
    }
}
