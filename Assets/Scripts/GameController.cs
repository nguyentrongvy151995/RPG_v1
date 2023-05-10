using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject gold;
    UIManager m_ui;
    int gold_score;

    // Start is called before the first frame update
    void Start()
    {
        SpawnGold();
        m_ui = FindObjectOfType<UIManager>();
        m_ui.SetScoreText("Gold: " + gold_score);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnGold()
    {
        for(int i = 0; i < 5; i++)
        {
            Vector2 spawnPos = new Vector2(Random.Range(-12, 12), Random.Range(-12, 12));
            if (gold)
            {
                Instantiate(gold, spawnPos, Quaternion.identity);
            }
        }
    }

    public void SetGoldScore(int value)
    {
        gold_score = value;
    }

    public int GetGoldScore()
    {
        return gold_score;
    }

    public void IncrementGold()
    {
        gold_score += 100;
        m_ui.SetScoreText("Gold: " + gold_score);
    }
}
