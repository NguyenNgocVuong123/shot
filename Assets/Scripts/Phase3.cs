using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase3 : MonoBehaviour
{
    public GameObject boss;
    private void Start() {
        Instantiate(boss, new Vector3(140, 2f, 0), Quaternion.identity);
    }
}
