using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public Transform player;
    public float camSen;
    float xRotation;
    
    private void Start() {
        
    }

    private void Update() {
        float x = Input.GetAxis("Mouse X") * camSen;
        float y = Input.GetAxis("Mouse Y") * camSen;

        xRotation -= y;
        xRotation =Mathf.Clamp(xRotation, -80,80);
        transform.localRotation = Quaternion.Euler(xRotation,0,0);

        player.Rotate(Vector3.up *x);
    }
    
}