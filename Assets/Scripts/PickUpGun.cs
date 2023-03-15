using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpGun : MonoBehaviour
{
    public GameObject useGun;
    public GameObject arm;
    public GameObject SpawnerPhase1;
    public GameObject ammo;
    public float speed;
    private void Update() {
        gameObject.transform.Rotate(Vector3.up * speed *Time.deltaTime);
    }
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag =="Player"){
            Destroy(gameObject);
            useGun.SetActive(true);
            arm.SetActive(true);
            SpawnerPhase1.SetActive(true);
            ammo.SetActive(true);
            // FindObjectOfType<GameManager>().Phase1ON();
        }
    }
}
