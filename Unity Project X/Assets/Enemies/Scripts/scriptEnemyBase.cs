using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptEnemyBase : MonoBehaviour
{
    
    [SerializeField] private GameObject InteractionCheck;
    public GameObject gameController;
    public GameObject forestController;
    int faceDirection = 7; //direcao que o personagem esta virado. P = personagem: 
    // 1 2 3
    // 4 P 5
    // 6 7 8
    public Sprite spriteTop;
    public Sprite spriteTopLeft;
    public Sprite spriteTopRight;
    public Sprite spriteLeft;
    public Sprite spriteRight;
    public Sprite spriteDown;
    public Sprite spriteDownLeft;
    public Sprite spriteDownRight;



    enum enumAwareness{
        passiveUnaware = 0, //NAO SABE sabe onde o player ta e NAO ESTA agindo contra ele
        passiveAware = 1, //SABE onde o player esta mas NAO ESTA agindo contra ele
        aggressiveUnaware = 2, //NAO SABE onde o player esta mas ESTA cacando ele
        aggressiveAwake = 3 // SABE onde player esta e ESTA agindo contra ele
    }

    enumAwareness currentAwareness;
    float moveLimiter = 0.7f; //ver FixedUpdate para explicacao
    public float runSpeed; //px por segund
    Rigidbody2D body;
    // Start is called before the first frame update
    void Start()
    {
        currentAwareness = enumAwareness.aggressiveUnaware;
        lastKnownPlayerPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentAwareness == enumAwareness.aggressiveAwake){
            transform.position = Vector2.MoveTowards(transform.position, lastKnownPlayerPosition, runSpeed*Time.deltaTime);
        }
        else if(currentAwareness == enumAwareness.aggressiveUnaware){
            if(lastKnownPlayerPosition != null){
                if(Vector2.Distance((Vector2)transform.position, lastKnownPlayerPosition) > 1){
                    transform.position = Vector2.MoveTowards(transform.position, lastKnownPlayerPosition, runSpeed*Time.deltaTime);
                }
                else{
                    float x;
                    float y;
                    do{
                        x = transform.position.x + Random.Range(-10f,10f);
                        y = transform.position.y + Random.Range(-10f,10f);
                    }while( (x<forestController.GetComponent<scriptForestController>().getMinBounds().x || 
                             x>forestController.GetComponent<scriptForestController>().getMaxBounds().x) || 
                            (y<forestController.GetComponent<scriptForestController>().getMinBounds().y ||
                             y>forestController.GetComponent<scriptForestController>().getMaxBounds().y ));

                    lastKnownPlayerPosition = new Vector2(x , y);
                }
            }
            else{
                lastKnownPlayerPosition = new Vector2(
                    transform.position.x + Random.Range(-10f,10f), 
                    transform.position.y + Random.Range(-10f,10f)
                );
            }
        }
        UpdateFaceDirection(lastKnownPlayerPosition);
    }

    int UpdateFaceDirection(Vector2 target){
        //Olhar para o mouse
        Vector2 t = target;
        t = Camera.main.ScreenToWorldPoint(t);
        Vector2 direction = new Vector2( //Direcao do mouse em relacao ao personagem
            target.x - transform.position.x,
            target.y - transform.position.y
        );
        float angle = AngleBetween(direction);
        int faceDirection = 2;
        if(angle>=-22.5f && angle<22.5f){
            faceDirection = 5; // Direita
            this.GetComponent<SpriteRenderer>().sprite = spriteRight; 
            InteractionCheck.transform.position = new Vector3(transform.position.x+ 0.6f, transform.position.y, 0);
        }
        else if(angle>=22.5f && angle<67.5f){
            faceDirection = 8; // Baixo-Direita
            this.GetComponent<SpriteRenderer>().sprite = spriteDownRight; 
            InteractionCheck.transform.position = new Vector3(transform.position.x+0.42f, transform.position.y-0.42f, 0);
        }
        else if(angle>=67.5f && angle<112.5f){
            faceDirection = 7; // Baixo
            this.GetComponent<SpriteRenderer>().sprite = spriteDown;
            InteractionCheck.transform.position = new Vector3(transform.position.x, transform.position.y-0.6f, 0);
        }
        else if(angle>=112.5f && angle<157.5f){
            this.GetComponent<SpriteRenderer>().sprite = spriteDownLeft; 
            faceDirection = 6; // Baixo-Esquerda
            InteractionCheck.transform.position = new Vector3(transform.position.x-0.42f, transform.position.y-0.42f, 0);
        }
        else if(angle>=157.5f || angle<-157.5f){
            faceDirection = 4; // Esquerda
            this.GetComponent<SpriteRenderer>().sprite = spriteLeft; 
            InteractionCheck.transform.position = new Vector3(transform.position.x-0.6f, transform.position.y, 0);
        }
        else if(angle>=-67.5f && angle<-22.5f){
            faceDirection = 3; // Topo-Direita
            this.GetComponent<SpriteRenderer>().sprite = spriteTopRight; 
            InteractionCheck.transform.position = new Vector3(transform.position.x+0.42f, transform.position.y+0.42f, 0);
        }
        else if(angle>=-112.5f && angle<-67.5f){
            faceDirection = 2; // Topo
            this.GetComponent<SpriteRenderer>().sprite = spriteTop; 
            InteractionCheck.transform.position = new Vector3(transform.position.x, transform.position.y+0.6f, 0);
        }
        else if(angle>=-157.5f && angle<-112.5f){
            faceDirection = 1; // Topo-Esquerda
            this.GetComponent<SpriteRenderer>().sprite = spriteTopLeft; 
            InteractionCheck.transform.position = new Vector3(transform.position.x-0.42f, transform.position.y+0.42f, 0);
        }
        return faceDirection;
    }

    float AngleBetween(Vector2 A){
        float sin = A.x * 0 - 1 * A.y;  
        float cos = A.x * 1 + A.y * 0;
        return Mathf.Atan2(sin, cos) * (180 / Mathf.PI);
    }

    void FixedUpdate(){

    }

    Vector2 lastKnownPlayerPosition;

    public void EnemyInSight(Vector2 playerPosition){
        lastKnownPlayerPosition = playerPosition;
        if(currentAwareness == enumAwareness.passiveUnaware){
            this.currentAwareness = enumAwareness.aggressiveAwake;
        }
        else if(currentAwareness == enumAwareness.aggressiveUnaware){
            this.currentAwareness = enumAwareness.aggressiveAwake;
        }
    }

    public void EnemyLeftSight(Vector2 playerPosition){
        lastKnownPlayerPosition = playerPosition;
        if(currentAwareness == enumAwareness.passiveAware){
            currentAwareness = enumAwareness.passiveUnaware;
        }
        else if(currentAwareness == enumAwareness.aggressiveAwake){
            currentAwareness = enumAwareness.aggressiveUnaware;
        }
    }
}
