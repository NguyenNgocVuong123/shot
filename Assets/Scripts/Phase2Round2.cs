using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase2Round2 : MonoBehaviour
{
    public GameObject rangeEnemy2;
    private int xPos,zPos1,zPos2;
    private void Start() {
        xPos = 70;
        zPos1 = 15;
        zPos2 = -15;
        Instantiate(rangeEnemy2, new Vector3(xPos, 2f, zPos1), Quaternion.identity);
        Instantiate(rangeEnemy2, new Vector3(xPos, 2f, zPos2), Quaternion.identity);
    }
}
