using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemy : MonoBehaviour
{
    private GameObject player;
    private Rigidbody enemy;
    public GameObject bullet;
    public Transform attackPoint;
    public float shootSpeed =100f;
    private bool attacked;
    public float speed;
    public float health = 150f;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemy = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.transform);
        if(enemy.transform == player.transform)
            enemy.position = transform.position;
        enemy.AddForce(speed * Time.deltaTime * transform.forward, ForceMode.Impulse);
        ShootPlayer();
    }
    private void ShootPlayer(){
        if(!attacked){
        GameObject currentBullet = Instantiate(bullet,attackPoint.position,Quaternion.identity);
        Rigidbody rigBullet = currentBullet.GetComponent<Rigidbody>();
        rigBullet.AddForce(transform.forward *shootSpeed);
        attacked = true;
        StartCoroutine(Cooldown());
        }
    }
    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(2f);
        attacked = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag== "PlayerBullet")
        {
            health -= 20;
            if (health <= 0)
        {
            Destroy(gameObject);
            GameManager.ins.stage2killCount++;
        }
        }
    }
}
