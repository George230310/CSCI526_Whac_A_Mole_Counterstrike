using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CursorManager : MonoBehaviour
{
    public static CursorManager Instance { get; private set; }

    public GameObject endScreen; 
    public TextMeshProUGUI endText;

    public float gameTime = 30.0f;  // game time span
    public bool gameEnded;  // is game ended?

    public MoleType cursorMoleType = MoleType.Rock;
    public Texture2D[] cursorTextures;
    public float gameScore = 0.0f;

    public float targetScore = 500f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        Cursor.SetCursor(cursorTextures[2], Vector2.zero, CursorMode.Auto);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameEnded)
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            return;
        }
        
        if (!gameEnded)
        {
            gameTime -= Time.deltaTime;  // decrement game time
            if (gameTime <= 0 || gameScore >= targetScore) // score check
            {
                gameEnded = true;

                // display endgame screen
                endScreen.SetActive(true);
                if (gameScore >= targetScore)
                {
                    endText.text = "You Win!";
                }
                else
                {
                    endText.text = "You Lose!";
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            cursorMoleType = MoleType.Rock;
            Cursor.SetCursor(cursorTextures[2], Vector2.zero, CursorMode.Auto);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            cursorMoleType = MoleType.Paper;
            Cursor.SetCursor(cursorTextures[0], Vector2.zero, CursorMode.Auto);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            cursorMoleType = MoleType.Scissor;
            Cursor.SetCursor(cursorTextures[1], Vector2.zero, CursorMode.Auto);
        }
    }
    
    void EndGame(string message)
    {
        gameEnded = true;

        // stop spawning
        MoleSpawner[] spawners = FindObjectsOfType<MoleSpawner>();
        foreach (var spawner in spawners)
        {
            spawner.StopAllCoroutines();
        }

        // display endgame message
        GameHUD.Instance.ShowEndScreen(message);
    }

    IEnumerator CloseGame()
    {
        yield return new WaitForSeconds(1f);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // shut down in build
#endif
    }

}
