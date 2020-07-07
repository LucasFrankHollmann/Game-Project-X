using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ScenarioElementController
{
    private Transform position;
    private float posX, posY, rotation;
    private ScenarioElement ElementPrefab;

    public ScenarioElementController(ScenarioElement prefab, float posX, float posY, float rotation, int hp, bool breakable){
        this.ElementPrefab = prefab;
        this.posX = posX;
        this.posY = posY;
        this.rotation = rotation;

        ElementPrefab.setInitValues(hp, breakable);
    }

    public void place() {
        ScenarioElement.Instantiate(ElementPrefab, new Vector3(posX, posY, 1), Quaternion.Euler(0,0,rotation));
    }

}
