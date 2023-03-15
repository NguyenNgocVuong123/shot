using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomBullet : MonoBehaviour
{
    public GameObject explosion;
    public Rigidbody bulletBody;
    public LayerMask WhatisEnemy;

    //bullet stat
    [Range(0,1)]
    public float bounciness;
    public bool useGravity;

    //
    public int explosionDmg;
    public float explosionRange;

    //
    public float maxLifeTime;
    public int maxCollision;
    public bool expOnTouch = true;

    int collision;
    PhysicMaterial physicMaterial;
    private void Start() {
        Setup();
    }
    private void Update() {
        //khi nào thì đạn nổ
        if(collision >maxCollision)
            Explode();
        //đếm ngược nổ
        maxLifeTime -= Time.deltaTime;
        if(maxLifeTime <=0)
            Explode();
    }
    private void Explode(){
        //tao vu no
        if(explosion != null)
            Instantiate(explosion,transform.position,Quaternion.identity);
        //Kiểm tra chạm enemy
        Collider[] enemy = Physics.OverlapSphere(transform.position, explosionRange, WhatisEnemy);
        for(int i = 0; i<enemy.Length;i++){//gay dmg len tat ca cac doi tuong trong array duoc danh dau la whatisenemy
            
            enemy[i].GetComponent<EnemyAI>().TakeDmg(explosionDmg);
        }
        
        //   enemy.GetComponent<EnemyAI>().TakeDmg(explosionDmg);
        //delete bullet
        Invoke("Delay",1f);
    }
    private void Delay(){
        Destroy(gameObject);
    }
    private void Setup(){
        //tao material
        physicMaterial = new PhysicMaterial();
        physicMaterial.bounciness = bounciness;
        physicMaterial.frictionCombine = PhysicMaterialCombine.Minimum;
        physicMaterial.bounceCombine = PhysicMaterialCombine.Maximum;
        //gan material vao collider
        GetComponent<SphereCollider>().material = physicMaterial;
        //độ rơi của đạn/trọng lực lên đạn
        bulletBody.useGravity = useGravity;

    }
    private void OnCollisionEnter(Collision other) {
        if(other.collider.CompareTag("Bullet")) //để đạn k colli với nhau
            return;
        collision++;
        //kiem tra xem dan có chạm trực tiếp hay k và trạng thái nổ
        if(other.collider.CompareTag("Enemy") && expOnTouch)
            Explode();
    }
}
