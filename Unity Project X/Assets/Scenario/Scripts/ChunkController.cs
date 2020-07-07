using System.Collections;
using System.Collections.Generic;
using UnityEngine;


struct SceneElement{
    public int id;
    public float posX, posY, rotation;
    public int hp;
    public bool breakable;
}

public class ChunkController : MonoBehaviour
{

    [SerializeField] private ScenarioElement TestElement;
    private List<SceneElement> ScenarioElements; //Elementos nesse chunk

    // Start is called before the first frame update
    void Start()
    {
        SceneElement a = new SceneElement();
        a.id = 1;

        

        
        if(a.id == 1)
        {
           new ScenarioElementController(TestElement, 2, 2, 30,100,false).place();
           new ScenarioElementController(TestElement, 6, 2, 30,100,false).place();
           new ScenarioElementController(TestElement, 10, 5, 30,100,false).place();
           new ScenarioElementController(TestElement, 9, 7, 30,100,false).place();
        }

        

        
        
    }
}
