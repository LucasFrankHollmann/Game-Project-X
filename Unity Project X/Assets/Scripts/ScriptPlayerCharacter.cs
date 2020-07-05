using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptPlayerCharacter : MonoBehaviour
{
    public GameObject gameController;
    Rigidbody2D body;
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

    float horizontal; //input horizontal
    float vertical; //input vertical
    float moveLimiter = 0.7f; //ver FixedUpdate para explicacao
    public float runSpeed; //px por segund
    float dodgeCooldown = 0f; //dodge Cooldown in seconds
    float dodgeDuration = 0f; //dodgeDuration in seconds

    void Start (){
        body = GetComponent<Rigidbody2D>();
    }

    void Update(){
        if(dodgeCooldown>0){
            dodgeCooldown -= Time.deltaTime;
        }

        horizontal = Input.GetAxisRaw("Horizontal"); // -1 = pra esquerda
        vertical = Input.GetAxisRaw("Vertical"); // -1 = pra baixo 
        if(Input.GetKey(KeyCode.LeftShift)){ // Checa botao de corrida
            // altera velocidade, vira 150%
            horizontal *=1.5f;
            vertical *=1.5f;
        }
        if( ((horizontal != 0 || vertical != 0) && Input.GetKeyDown(KeyCode.Space) && dodgeCooldown<=0) || dodgeDuration>0){ //Esquiva.
            horizontal *=4f;
            vertical *=4f;
            if(dodgeDuration>0){
                dodgeDuration -= Time.deltaTime;
            }
            else{
                dodgeCooldown = 2f;
                dodgeDuration = 0.15f;
            }
        }
        faceDirection = UpdateFaceDirection();
    }

    void FixedUpdate(){
        if (horizontal != 0 && vertical != 0){ // Checa movimento diagonal
            // limita velocidade diagonal, 70%
            horizontal *= moveLimiter;
            vertical *= moveLimiter;
        } 

        body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
    }



    int UpdateFaceDirection(){
        //Olhar para o mouse
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 direction = new Vector2( //Direcao do mouse em relacao ao personagem
            mousePosition.x - transform.position.x,
            mousePosition.y - transform.position.y
        );
        float angle = AngleBetween(direction);
        int faceDirection = 2;
        if(angle>=-22.5f && angle<22.5f){
            faceDirection = 5; // Direita
            this.GetComponent<SpriteRenderer>().sprite = spriteRight; 
        }
        else if(angle>=22.5f && angle<67.5f){
            faceDirection = 8; // Baixo-Direita
            this.GetComponent<SpriteRenderer>().sprite = spriteDownRight; 
        }
        else if(angle>=67.5f && angle<112.5f){
            faceDirection = 7; // Baixo
            this.GetComponent<SpriteRenderer>().sprite = spriteDown;
        }
        else if(angle>=112.5f && angle<157.5f){
            this.GetComponent<SpriteRenderer>().sprite = spriteDownLeft; 
            faceDirection = 6; // Baixo-Esquerda
        }
        else if(angle>=157.5f || angle<-157.5f){
            faceDirection = 4; // Esquerda
            this.GetComponent<SpriteRenderer>().sprite = spriteLeft; 
        }
        else if(angle>=-67.5f && angle<-22.5f){
            faceDirection = 3; // Topo-Direita
            this.GetComponent<SpriteRenderer>().sprite = spriteTopRight; 
        }
        else if(angle>=-112.5f && angle<-67.5f){
            faceDirection = 2; // Topo
            this.GetComponent<SpriteRenderer>().sprite = spriteTop; 
        }
        else if(angle>=-157.5f && angle<-112.5f){
            faceDirection = 1; // Topo-Esquerda
            this.GetComponent<SpriteRenderer>().sprite = spriteTopLeft; 
        }
        return faceDirection;
    }
    float AngleBetween(Vector2 A){
        float sin = A.x * 0 - 1 * A.y;  
        float cos = A.x * 1 + A.y * 0;
        return Mathf.Atan2(sin, cos) * (180 / Mathf.PI);
    }
}
