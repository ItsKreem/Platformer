using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int CurrentLevel = 1;
    public static GameManager Instance
    {
        get
        {
            if(m_Instance == null)
            {
                m_Instance = (GameManager) FindObjectOfType(typeof(GameManager));

                if(m_Instance == null )
                {
                    GameObject go = new GameObject();
                    go.name = "GAMEMANAGER";
                    m_Instance = go.AddComponent<GameManager>();
                }

                DontDestroyOnLoad(m_Instance.gameObject);
            }
            return m_Instance;
        }
    }

    private static GameManager m_Instance = null;

    public void StartGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void GoToNextLevel()
    {
        if (CurrentLevel == 2)
        {
            CurrentLevel = 1;
            SceneManager.LoadScene(0);
            return;
        }
        CurrentLevel++;
        SceneManager.LoadScene(CurrentLevel);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
