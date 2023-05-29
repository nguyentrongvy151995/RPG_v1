using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    //public TMP_InputField inputField;

    //public string displayName;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public async void LoadScene(string sceneName)
    {
        // SetPlayerName();
        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = true;
    }

    /*public void SetPlayerName()
    {
        displayName = inputField.text;
    }*/

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
