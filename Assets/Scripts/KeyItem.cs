using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItem : MonoBehaviour
{
    public float speed;
    public Component bossDoor;
    public GameObject keyUI;
    public GameObject key;


    private void Update() {
        gameObject.transform.Rotate(Vector3.up * speed *Time.deltaTime);
    }
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Player"){
            bossDoor.GetComponent<BoxCollider>().enabled =true;
            keyUI.SetActive(true);
            key.SetActive(false);
        }

    }
}
