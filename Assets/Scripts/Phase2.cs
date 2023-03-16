using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase2 : MonoBehaviour
{
    public GameObject rangeEnemy;
    private int xPos,zPos1,zPos2;
    private void Start() {
        xPos = 30;
        zPos1 = 15;
        zPos2 = -15;
        Instantiate(rangeEnemy, new Vector3(xPos, 2f, zPos1), Quaternion.identity);
        Instantiate(rangeEnemy, new Vector3(xPos, 2f, zPos2), Quaternion.identity);
    }
}
