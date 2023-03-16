using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoor : MonoBehaviour
{
    public GameObject doorOpen;
    public GameObject spawnBoss;
    public GameObject bossHealthBar;
    public GameObject KeyUI;
    

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag =="Player"){
            doorOpen.GetComponent<Animator>().SetBool("IsOen",true);//gắn ani và trạng thái doorOpen vào door
            spawnBoss.SetActive(true);
            bossHealthBar.SetActive(true);
            KeyUI.SetActive(false);
            Debug.Log("opened");
        }
    }
}
