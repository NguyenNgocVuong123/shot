using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItem : MonoBehaviour
{
    public float speed;
    private void Update() {
        gameObject.transform.Rotate(Vector3.up * speed *Time.deltaTime);
    }
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Player")
            Destroy(gameObject);
    }
}
