using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthController : MonoBehaviour
{
    public Slider slider;
    
    public void SetMaxBossHealth(int bossHealth){
        slider.maxValue = bossHealth;
        slider.value = bossHealth;
    }
    public void SetBossHealth(int bossHealth){
        slider.value = bossHealth;
    }
}
