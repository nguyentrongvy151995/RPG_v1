using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heart : MonoBehaviour
{
    public float maxHealth;
    public Slider playerHealthSlider;

    // Start is called before the first frame update
    void Start()
    {
        playerHealthSlider.maxValue = maxHealth;
        playerHealthSlider.value = maxHealth;
        playerHealthSlider.enabled = false;
    }

    public void Decrease()
    {
        playerHealthSlider.value -= 50f;
    }

    public float GetMaxHealth()
    {
        return playerHealthSlider.value;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
