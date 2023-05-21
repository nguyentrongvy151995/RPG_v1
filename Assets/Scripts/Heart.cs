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
    }

    public void Decrease()
    {
        playerHealthSlider.value -= 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
