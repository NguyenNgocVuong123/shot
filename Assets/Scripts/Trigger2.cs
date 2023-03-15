using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger2 : MonoBehaviour
{
    public GameObject SpawnerPhaseRound2;
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag =="Player"){
            Destroy(gameObject);
            SpawnerPhaseRound2.SetActive(true);
        }
    }
}
