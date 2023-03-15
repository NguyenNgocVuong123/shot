using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase1 : MonoBehaviour
{
    public GameObject meleEnemy;
    public int xPos,zPos;
    private void Start() {
        StartCoroutine(SpawnMele());
    }

    IEnumerator SpawnMele(){
        for(int i = 0; i<5; i++){
            xPos = Random.Range(-15,-31);
            zPos = Random.Range(-15,16);
            Instantiate(meleEnemy, new Vector3(xPos, 2f, zPos), Quaternion.identity);
            yield return new WaitForSeconds(1.5f);
        }
    }
}
