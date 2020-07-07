using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptForestController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject gameController; 
    public GameObject forestController;
    [SerializeField] BoxCollider2D boundBox;
    public List<GameObject> resourceSpawnables; //recursos que PODEM ser spawnados no mapa
    public List<GameObject> enemiesSpawnables; //inimigos que PODEM ser spawnados no mapa

    public LinkedList<GameObject> enemiesOnMap; //inimigos no mapa
    int day;

    Vector3 maxBounds;
    Vector3 minBounds;

    void Start(){
        minBounds = boundBox.bounds.min;
        maxBounds = boundBox.bounds.max;
        day = gameController.GetComponent<ScriptGameController>().getDay();
        int numberOfEnemiesToSpawn = day * 2; //só pra teste
        for(int i=0; i<numberOfEnemiesToSpawn; i++){
            int chosen = Random.Range(0,enemiesSpawnables.Count-1);
            Vector2 position = new Vector2(Random.Range(minBounds.x, maxBounds.x), Random.Range(minBounds.y, maxBounds.y));
            GameObject enemy = Instantiate(enemiesSpawnables[chosen], position, Quaternion.identity, forestController.transform);
            enemy.GetComponent<scriptEnemyBase>().gameController = gameController;
            enemy.GetComponent<scriptEnemyBase>().forestController = forestController;
            enemy.name= "enemy"+i;
            //enemiesOnMap.AddFirst(enemy);
        }
        int numberOfSpawnablesToSpawn = 40;
        for(int i=0; i< numberOfSpawnablesToSpawn; i++){
            int chosen = Random.Range(0,resourceSpawnables.Count-1);
            Vector2 position = new Vector2(Random.Range(minBounds.x, maxBounds.x), Random.Range(minBounds.y, maxBounds.y));
            Instantiate(resourceSpawnables[chosen], position, Quaternion.identity, forestController.transform);
        }
    }
    
    public Vector3 getMaxBounds(){
        return maxBounds;
    }
    public Vector3 getMinBounds(){
        return minBounds;
    }
}
