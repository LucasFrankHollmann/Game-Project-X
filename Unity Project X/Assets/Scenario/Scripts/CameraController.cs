using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CameraController : MonoBehaviour
{

    [SerializeField] public GameObject player;
    [SerializeField] private BoxCollider2D boundBox;
    [SerializeField] private Camera camera;
    [SerializeField]private Vector3 minBounds;
    [SerializeField]private Vector3 maxBounds;
    [SerializeField]private float halfHeight;
    [SerializeField]private float halfWidth;


    // Start is called before the first frame update
    void Start()
    {
        minBounds = boundBox.bounds.min;
        maxBounds = boundBox.bounds.max;

        halfHeight = camera.orthographicSize;
        halfWidth = halfHeight * ((float)Screen.width/(float)Screen.height);
    }

    // Update is called once per frame
    void Update(){
        if(player != null){
            if(player.transform.position.x-halfWidth > minBounds.x && player.transform.position.x+halfWidth < maxBounds.x){
                if(player.transform.position.y-halfHeight > minBounds.y && player.transform.position.y+halfHeight < maxBounds.y){
                    transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
                }
                else{
                    transform.position = new Vector3(player.transform.position.x, transform.position.y, -10);
                }
            }
            else{
                if(player.transform.position.y-halfHeight > minBounds.y && player.transform.position.y+halfHeight < maxBounds.y){
                    transform.position = new Vector3(transform.position.x, player.transform.position.y, -10);
                }
                else{
                    transform.position = new Vector3(transform.position.x, transform.position.y, -10);
                }
            }   
        }
    }
}