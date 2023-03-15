using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float moveForward;
    float moveSide;
    float moveUp;
    bool isGrounded = false;

    public float speed;
    public float sprintSpeed;
    public float currenSpeed;
    public float jumpSpeed;
    //
    public int maxHealth = 100;
    public int currentHealth;
    public HealthController healthController;
    //
    [SerializeField] Rigidbody rb;

    private void Start() {
        currentHealth = maxHealth;
        healthController.SetMaxHealth(maxHealth);
    }
    private void Update() {
        MoveInput();
        Jumping();
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
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Ground")
            isGrounded = true;

    }
}
