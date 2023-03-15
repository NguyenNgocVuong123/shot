using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase1Spawn : MonoBehaviour
{
    public GameObject meleEnemy;
    public int xPos;
    public int zPos;
    public int enemyCount;
    public bool isDead;
    private int killcount;
    private void Start() {
        StartCoroutine(EnemySpawn());
    }
    private void Update(){
        
    }

    IEnumerator EnemySpawn(){
        while (enemyCount <5){
            xPos = Random.Range(-10,-41);
            zPos = Random.Range(-15,16);
            Instantiate(meleEnemy, new Vector3(xPos,3,zPos), Quaternion.identity);
            yield return new WaitForSeconds(1.5f);
            enemyCount+=1;
        }
    }
}
