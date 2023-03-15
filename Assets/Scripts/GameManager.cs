using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]PlayerController player;
    public static GameManager ins;
    public CamFollow cam;
    public bool phase1;
    public bool phase2;
    public bool phase3;
    public int stage1KillCount;
    public int stage2killCount;
    public bool bossDead;
    public GameObject deadUI;
    public TextMeshProUGUI Anou;
    bool isOver = false;
    private void Instance()
    {
        if (ins == null)
        {
            ins = this;
        }
    }
    private void Awake() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Instance();
        stage1KillCount = 0;
        stage2killCount = 0;
        bossDead = false;
        phase1 = false;
        phase2 = false;
        phase3 = false;
        
    }
    
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
