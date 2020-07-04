using System.Collections;
using System.Collections.Generic;
using UnityEngine;


struct SceneElement{
    public int id;
    public float posX, posY, rotation;
}

public class ChunkController : MonoBehaviour
{

    [SerializeField] private GameObject TestElement;

    private const float size = 5f;
    private List<SceneElement> ScenarioElements; //Elementos nesse chunk

    // Start is called before the first frame update
    void Start()
    {
        SceneElement a = new SceneElement();
        a.id = 1;
        a.posX = 2;
        a.posY = 2;
        a.rotation = 6;

        if(a.id == 1)
        {
           new ScenarioElementController(TestElement, 2, 2, 30).place();
           new ScenarioElementController(TestElement, 6, 2, 30).place();
           new ScenarioElementController(TestElement, 10, 5, 30).place();
           new ScenarioElementController(TestElement, 9, 7, 30).place();
        }

        

        transform.localScale = new Vector3(size,size,1);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
