using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask WhatisGround, WhatisPlayer;
    public int health;
    private bool isDead;
    public GameObject enemyBullet;
    private int currentHealth;
    //patrol
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //attack
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //
    public float sightRange, attackRange;
    public bool playerIsInSightRange,playerIsInAttackRange;

    private void Awake() {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();

        
    }
    private void Update() {
        //kiem tra xem player co o trong tam nhin hay tam tan cong hay khong
        playerIsInSightRange = Physics.CheckSphere(transform.position, sightRange, WhatisPlayer);
        playerIsInAttackRange = Physics.CheckSphere(transform.position, attackRange, WhatisPlayer);
        if(!playerIsInAttackRange && !playerIsInSightRange)
            Patroling();
        if(!playerIsInAttackRange && playerIsInSightRange)
            Chasing();
        if(playerIsInAttackRange && playerIsInSightRange)
            Attacking();
        currentHealth = health;

    }
    private void Patroling(){
        if(!walkPointSet)
            SearchWalkPoint();
        if(walkPointSet)
            agent.SetDestination(walkPoint);
        //tinh khoang cach den vi tri di tuan
        Vector3 distentToWalkPoint = transform.position -walkPoint;//vi tri hen tai cua quai - vi tri đích
        if(distentToWalkPoint.magnitude <1f)
            walkPointSet = false;
    }
    private void Chasing(){
        agent.SetDestination(player.position);
    }
    private void Attacking(){
        //khi đến tầm tấn công thì đứng yên để tấn công
        agent.SetDestination(transform.position);
        transform.LookAt(player);
        if(!alreadyAttacked){
            //range
                // Rigidbody rb = Instantiate(enemyBullet, transform.position,Quaternion.identity).GetComponent<Rigidbody>();
                // rb.AddForce(transform.forward *20f,ForceMode.Impulse);
                // rb.AddForce(transform.up *2f,ForceMode.Impulse);
                player.GetComponent<PlayerController>().TakeDmg(20);
            //
            alreadyAttacked = true;
            Invoke("ResetAttack",timeBetweenAttacks);
        }

    }
    private void SearchWalkPoint(){
        float randomZ = Random.Range(-walkPointRange,walkPointRange);
        float randomX = Random.Range(-walkPointRange,walkPointRange);
        //đặt vị trí hiện tại của mình +random
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y,transform.position.z+randomZ);
        //để quái k chạy rơi khỏi khu vực thì kiểm tra xem quái có đi trên khu vực ground hay k
        if(Physics.Raycast(walkPoint,transform.up,0f,WhatisGround))
            walkPointSet = true;
    }
    private void ResetAttack(){
        alreadyAttacked = false;
    }

    public void TakeDmg(int dmg){
        health -= dmg;
        if(health<=0){
            Invoke("EnemyDead",0.2f);
        }
    }
    private void EnemyDead(){
        Destroy(gameObject);
    }
}
