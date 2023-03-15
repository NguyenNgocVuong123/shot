using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]Phase1Spawn p1s;
    [SerializeField]PlayerController player;
    public CamFollow cam;

    public GameObject deadUI;
    public TextMeshProUGUI Anou;
    bool isOver = false;

    private void Awake() {
        p1s.isDead = GetComponent<Phase1Spawn>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
    }

    // public void Phase1ON(){
    //         player.speed = 0;
    //         player.jumpSpeed = 0;
    //         player.sprintSpeed = 0;
    // }
    // public void PhaseOFF(){
    //         player.speed = 3;
    //         player.jumpSpeed = 4;
    //         player.sprintSpeed = 5;
    // }
    public void GameOver(){
        if(isOver == false){
        isOver = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        cam.enabled = false;
        
        player.enabled = false;
        Time.timeScale = 0f;
        Anou.text = "You DIE";
        deadUI.SetActive(true);
        
        }
    }
    public void Restart(){
        //load lại sceen đang hoạt động - sceen hiện tại
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }
    public void HomeMenu(){ // quay lại trang Home
        SceneManager.LoadScene("MainMenu");
    }
}
