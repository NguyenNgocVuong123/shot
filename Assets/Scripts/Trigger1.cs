using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger1 : MonoBehaviour
{
    public GameObject SpawnerPhase2;
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag =="Player"){
            Destroy(gameObject);
            SpawnerPhase2.SetActive(true);
        }
    }
}
