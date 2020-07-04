using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ScenarioElementController : MonoBehaviour
{
    private Transform position;

    private float posX, posY, rotation;

    
    private GameObject ElementPrefab;
    public ScenarioElementController(GameObject prefab, float posX, float posY, float rotation){
        this.ElementPrefab = prefab;
        this.posX = posX;
        this.posY = posY;
        this.rotation = rotation;
    }

    public void place() {
        GameObject.Instantiate(ElementPrefab, new Vector3(posX, posY, 1), Quaternion.Euler(0,0,rotation));
    }

    public void destroy(){
        //destroir o elemento
    }

    public virtual void interact(){
    }
}
