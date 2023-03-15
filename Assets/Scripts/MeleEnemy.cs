using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleEnemy : MonoBehaviour
{
    private GameObject player;
    private Rigidbody enemy;
    public float speed;
    public float health = 40f;
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
        EnemyDie();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag== "PlayerBullet")
        {
            health -= 15;
        }
    }

    void EnemyDie()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
            GameManager.ins.stage1KillCount++;
        }
    }
}
