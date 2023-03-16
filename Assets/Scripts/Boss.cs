using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private GameObject player;
    private Rigidbody enemy;
    public GameObject bullet;
    public Transform attackPoint1;
    public Transform attackPoint2;
    public float shootSpeed =100f;
    private bool attacked;
    public int speedRangeMode;
    public int speedMeleMode;
    public int speedMeleReady;
    public GameObject boss;
    private float distant;
    public Transform bossPos;
    //
    public int maxHealth = 1000;
    public int currentHealth;
    public BossHealthController bossHealthController;
    public static Boss ins;
    //
    // private Transform target;
    // private Vector3 moveDir;
    private void Awake() {
        Instance();
        
    }
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemy = GetComponent<Rigidbody>();
        currentHealth =maxHealth;
        bossHealthController.SetMaxBossHealth(currentHealth);
        
        // target = GameObject.Find("Player").transform;
    }
    private void Instance(){
        if(ins == null){
            ins = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // if(target){
        //     Vector3 dir = (target.position - transform.position).normalized;
        //     moveDir = dir;
        // }
        distant = Vector3.Distance(player.transform.position, bossPos.transform.position);
        transform.LookAt(player.transform);
        // enemy.velocity = new Vector3(moveDir.x,moveDir.y,moveDir.z) *speedRangeMode*Time.deltaTime;
        enemy.AddForce(speedRangeMode *Time.deltaTime* transform.forward, ForceMode.VelocityChange);
        
        // if(enemy.transform == player.transform)
        //     enemy.position = transform.position;
        if(!attacked){
            if(distant >=15 ){
            
            boss.GetComponent<Animator>().SetBool("IsMele", false);
            ShootPlayer();
            attacked = true;
            Invoke("resetAttack", 2f);
            }
            if(distant<15){
                boss.GetComponent<Animator>().SetBool("IsMele", true);
                enemy.AddForce(speedMeleMode * Time.deltaTime * transform.forward, ForceMode.VelocityChange);
                Debug.Log("attack ready");
                attacked = true;
                Invoke("MeleAttack", 3f);
                return;
                }
        }
    }
    private void ShootPlayer(){
            
            GameObject currentBullet1 = Instantiate(bullet,attackPoint1.position,Quaternion.identity);
            GameObject currentBullet2 = Instantiate(bullet,attackPoint2.position,Quaternion.identity);
            Rigidbody rigBullet1 = currentBullet1.GetComponent<Rigidbody>();
            Rigidbody rigBullet2 = currentBullet2.GetComponent<Rigidbody>();
            rigBullet1.AddForce(transform.forward *shootSpeed);
            rigBullet2.AddForce(transform.forward *shootSpeed);   
    }
    private void MeleAttack(){
            attacked = false;
            enemy.AddForce(speedMeleReady * Time.deltaTime * transform.forward, ForceMode.VelocityChange);
            // transform.position = player.transform.position;
            
        // enemy.velocity = new Vector3(moveDir.x,moveDir.y,moveDir.z) *speed*Time.deltaTime;
    }
    private void resetAttack(){
        attacked = false;
    }
    private void OnTriggerEnter(Collider other) {
    if (other.gameObject.tag =="PlayerBullet"){
        currentHealth-=30;
        bossHealthController.SetBossHealth(currentHealth);
        if(currentHealth<0){
            Destroy(gameObject);
            GameManager.ins.bossDead = true;
            }
        }
    }
}
