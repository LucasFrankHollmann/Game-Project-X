using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementoTesteController : MonoBehaviour
{

    private bool spin = false;
   
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(spin){
            transform.Rotate(0,0,20);
        }
    }

    void OnMouseEnter() {
        spin = true;
    }

    void OnMouseExit() {
        spin = false;
    }
}
