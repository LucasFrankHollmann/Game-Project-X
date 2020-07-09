using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptPlayerCharacter : MonoBehaviour
{
    [SerializeField] private GameObject interactionCheck;
    [SerializeField] private LayerMask whatIsInteractable;

    public Animator animator;
    private float interactionCheckRadius = 0.2f;
    private float interactRate = 0.5f, interactCooldown = 0f;
    public float attackRange;
    public float attackRate; 
    private float attackCooldown = 0f;
    public GameObject gameController;
    Rigidbody2D body;
    public CircleCollider2D detectionRadius;
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

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(InteractionCheck.transform.position, InteractionCheckRadius);
        Gizmos.DrawWireSphere(interactionCheck.transform.position, attackRange);
    }

    void Start (){
        body = GetComponent<Rigidbody2D>();
        gameController =  GameObject.FindWithTag("GameController");
        GameObject.FindWithTag("MainCamera").GetComponent<CameraController>().player = GameObject.FindWithTag("Player");
    }

    void Update(){
        horizontal = Input.GetAxisRaw("Horizontal"); // -1 = pra esquerda
        vertical = Input.GetAxisRaw("Vertical"); // -1 = pra baixo 
        if(Input.GetKey(KeyCode.LeftShift)){ // Checa botao de corrida
            RunMove();
        }

        if(dodgeCooldown>0){
            dodgeCooldown -= Time.deltaTime;
        }
        if( ((horizontal != 0 || vertical != 0) && Input.GetKeyDown(KeyCode.Space) && dodgeCooldown<=0) || dodgeDuration>0){ //Esquiva.
            DodgeMove();
        }

        if(attackCooldown>0){
            attackCooldown -= Time.deltaTime;
        }
        if(Input.GetMouseButtonDown(0) && attackCooldown <=0){
            AttackPrimary();
        }

        faceDirection = UpdateFaceDirection();

        if(Input.GetKey(KeyCode.F) && interactCooldown <= 0){
            Interact();
        }
        else if(interactCooldown > 0){
            interactCooldown -= Time.deltaTime;
        }
    }

    void AttackPrimary(){
        animator.SetTrigger("AttackPrimary");

        //Detectar inimigos no alcance 
        Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(interactionCheck.transform.position, attackRange);

        foreach(Collider2D enemy in enemiesHit){
            if(enemy.tag.Equals("Enemy")){
                Debug.Log(enemy.name+" atingido");
            }
        }
    }

    void RunMove(){
        // altera velocidade, vira 150%
        horizontal *=1.5f;
        vertical *=1.5f;
    }

    void DodgeMove(){
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

    void Interact(){
        Collider2D[] ScElList = Physics2D.OverlapCircleAll(interactionCheck.transform.position, interactionCheckRadius , whatIsInteractable);
        if(ScElList.Length != 0){
            GameObject ScEl = ScElList[0].gameObject;
            if(ScEl.tag.Equals("InteractableElement")){
                ScEl.GetComponent<ScenarioElement>().onInteraction();
            }
        }
        interactCooldown = interactRate;
    }
    void FixedUpdate(){
        
        if (horizontal != 0 && vertical != 0){ // Checa movimento diagonal
            // limita velocidade diagonal, 70%
            horizontal *= moveLimiter;
            vertical *= moveLimiter;
        } 

        body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
    }

    void OnTriggerStay2D(Collider2D enemyCollider){
        if(enemyCollider.gameObject.tag.Equals("Enemy")){
            enemyCollider.gameObject.GetComponent<scriptEnemyBase>().EnemyInSight((Vector2)this.gameObject.transform.position);
        }        
    }

    void OnTriggerExit2D(Collider2D enemyCollider){
        if(enemyCollider.gameObject.tag.Equals("Enemy")){
            enemyCollider.gameObject.GetComponent<scriptEnemyBase>().EnemyLeftSight((Vector2)this.gameObject.transform.position);
        }
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
            animator.SetTrigger("WalkR");
            interactionCheck.transform.position = new Vector3(transform.position.x+ 0.8f, transform.position.y, 0);
        }
        else if(angle>=22.5f && angle<67.5f){
            faceDirection = 8; // Baixo-Direita
            animator.SetTrigger("WalkDR");
            interactionCheck.transform.position = new Vector3(transform.position.x+0.6f, transform.position.y-0.6f, 0);
        }
        else if(angle>=67.5f && angle<112.5f){
            faceDirection = 7; // Baixo
            animator.SetTrigger("WalkD");
            interactionCheck.transform.position = new Vector3(transform.position.x, transform.position.y-0.8f, 0);
        }
        else if(angle>=112.5f && angle<157.5f){
            faceDirection = 6; // Baixo-Esquerda
            animator.SetTrigger("WalkDL");
            interactionCheck.transform.position = new Vector3(transform.position.x-0.6f, transform.position.y-0.6f, 0);
        }
        else if(angle>=157.5f || angle<-157.5f){
            faceDirection = 4; // Esquerda
            animator.SetTrigger("WalkL");
            interactionCheck.transform.position = new Vector3(transform.position.x-0.8f, transform.position.y, 0);
        }
        else if(angle>=-67.5f && angle<-22.5f){
            faceDirection = 3; // Topo-Direita
            animator.SetTrigger("WalkUR");
            interactionCheck.transform.position = new Vector3(transform.position.x+0.6f, transform.position.y+0.6f, 0);
        }
        else if(angle>=-112.5f && angle<-67.5f){
            faceDirection = 2; // Topo
            animator.SetTrigger("WalkU");
            interactionCheck.transform.position = new Vector3(transform.position.x, transform.position.y+0.8f, 0);
        }
        else if(angle>=-157.5f && angle<-112.5f){
            faceDirection = 1; // Topo-Esquerda
            animator.SetTrigger("WalkUL");
            interactionCheck.transform.position = new Vector3(transform.position.x-0.6f, transform.position.y+0.6f, 0);
        }
        return faceDirection;
    }
    float AngleBetween(Vector2 A){
        float sin = A.x * 0 - 1 * A.y;  
        float cos = A.x * 1 + A.y * 0;
        return Mathf.Atan2(sin, cos) * (180 / Mathf.PI);
    }
}