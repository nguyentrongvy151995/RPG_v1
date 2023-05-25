using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject gold;
    public GameObject bomb;
    UIManager m_ui;
    int gold_score;

    // Start is called before the first frame update
    void Start()
    {
        SpawnGold();
        SpawnBomb();
        m_ui = FindObjectOfType<UIManager>();
        m_ui.SetScoreText("Gold: " + gold_score);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnGold()
    {
        for(int i = 0; i < 100; i++)
        {
            Vector2 spawnPos = new Vector2(Random.Range(-68, 80), Random.Range(38, -33));
            if (gold)
            {
                Instantiate(gold, spawnPos, Quaternion.identity);
            }
        }
    }

    public void SpawnBomb()
    {
        for (int i = 0; i < 50; i++)
        {
            Vector2 spawnPos = new Vector2(Random.Range(-68, 80), Random.Range(38, -33));
            if (bomb)
            {
                Instantiate(bomb, spawnPos, Quaternion.identity);
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
