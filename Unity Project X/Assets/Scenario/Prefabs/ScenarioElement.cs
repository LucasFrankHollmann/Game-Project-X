using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScenarioElement : MonoBehaviour
{
    [SerializeField] private bool breakable = false;
    [SerializeField] private int hp = 100;

    public void setInitValues(int hp, bool breakable){
        this.breakable = breakable;
        this.hp = hp;
    }
    
    public virtual void onInteraction(){}

    public virtual void onBreaking(){}

    public virtual void takeDamage(int dmg){}

    public int getHp(){
        return hp;
    }
}
