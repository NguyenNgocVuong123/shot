using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase2 : MonoBehaviour
{
    public GameObject rangeEnemy1;
    private void Start() {
        
    }
    private void Update(){
        Instantiate(rangeEnemy1, new Vector3(26,3,20), Quaternion.identity);
        Instantiate(rangeEnemy1, new Vector3(26,3,-10), Quaternion.identity);
    }
}
