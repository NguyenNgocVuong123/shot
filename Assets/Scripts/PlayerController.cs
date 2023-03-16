using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float moveForward;
    float moveSide;
    float moveUp;
    bool isGrounded = false;
    public static PlayerController ins;
    public float speed;
    public float sprintSpeed;
    public float currenSpeed;
    public float jumpSpeed;
    public bool Stopped;
    public GameObject phase1Spawn;
    public GameManager gameManager;
    public GameObject keyItem;
    //
    //
    public int maxHealth = 100;
    public int currentHealth;
    public HealthController healthController;
    //
    [SerializeField] Rigidbody rb;

    
    private void Awake() {
        Instance();
        
    }
    private void Start() {
        currentHealth = maxHealth;
        healthController.SetMaxHealth(maxHealth);
        Stopped = false;
    }
    private void Instance(){
        if(ins == null){
            ins = this;
        }
    }
    private void Update() {
        MoveInput();
        Jumping();
        
        if(Stopped == true)
        {
            speed = 0;
            sprintSpeed = 0;
            jumpSpeed = 0;
        }else{
            speed = 5;
            sprintSpeed = 8;
            jumpSpeed = 4;
        }
        EndPhase1();
        EndPhase2();
    }
    private void LateUpdate() {
            Moving();
    }
    private void MoveInput(){
        if(Input.GetKey(KeyCode.LeftShift)){
            if(currenSpeed!=sprintSpeed)
                currenSpeed = sprintSpeed;
        }else
            currenSpeed = speed;

        moveForward = Input.GetAxis("Vertical") *currenSpeed;
        moveSide = Input.GetAxis("Horizontal")* currenSpeed;
        moveUp = Input.GetAxis("Jump") *jumpSpeed;
    }
    private void Moving(){
        rb.velocity = (transform.forward *moveForward) + (transform.right * moveSide) +(transform.up * rb.velocity.y);
        //jump
        
    }
    private void Jumping(){
        if(isGrounded  && moveUp !=0){
            isGrounded = false;
            Vector3 jumpDirection = new Vector3(0, jumpSpeed, 0); // Vector3 for the jump direction
            rb.AddForce(jumpDirection, ForceMode.VelocityChange);;
        }
    }
    public void TakeDmg(int dmg){
        currentHealth -= dmg;
        healthController.SetHealth(currentHealth);
        if(currentHealth<=0){
            FindObjectOfType<GameManager>().GameOver();
        }
    }
    private void EndPhase1()
    {
        if(gameManager.stage1KillCount == 5)
        {
            Stopped = false;
        }
    }
    private void EndPhase2()
    {
        if(gameManager.stage2killCount == 4)
        {
                keyItem.SetActive(true);
            
        }
    }
      
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Ground")
            isGrounded = true;
        if(other.gameObject.tag == "PickUpGun"){
            Stopped = true;
            phase1Spawn.SetActive(true);
        }
        if(other.gameObject.tag == "MeleEnemy"){
            TakeDmg(20);
        }
        if(other.gameObject.tag == "EnemyBullet"){
            TakeDmg(25);
        }
        if(other.gameObject.tag == "Boss"){
            TakeDmg(80);
        }
        if(other.gameObject.tag == "BossBullet"){
            TakeDmg(40);
        }
            

    }
}
