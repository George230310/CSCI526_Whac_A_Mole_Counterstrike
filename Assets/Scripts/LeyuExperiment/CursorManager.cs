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

    public float gameTime = 30.0f;  // 总游戏时间
    public bool gameEnded = false;  // 游戏是否结束

    public MoleType cursorMoleType = MoleType.Scissor;
    public Texture2D[] cursorTextures;
    public float gameScore = 0.0f;

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
        Cursor.SetCursor(cursorTextures[1], Vector2.zero, CursorMode.Auto);
    }

    void EndGame(string message)
    {
        gameEnded = true;

        // 停止鼹鼠的生成
        MoleSpawner[] spawners = FindObjectsOfType<MoleSpawner>();
        foreach (var spawner in spawners)
        {
            spawner.StopAllCoroutines();
        }

        // 显示结束界面
        GameHUD.Instance.ShowEndScreen(message);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameEnded)
        {
            // 关闭游戏
            StartCoroutine(CloseGame());
            return; // 不执行下面的代码
        }
        if (!gameEnded)
        {
            gameTime -= Time.deltaTime;  // 减少游戏时间
            if (gameTime <= 0 || gameScore >= 500) // 添加得分检查
            {
                gameEnded = true;

                // 显示结束屏幕和文本
                endScreen.SetActive(true);
                if (gameScore >= 500)
                {
                    endText.text = "You Win!";
                }
                else
                {
                    endText.text = "You Lose!";
                }
            }
        }

        if (Input.GetAxis("Paper") > 0f)
        {
            cursorMoleType = MoleType.Paper;
            Cursor.SetCursor(cursorTextures[0], Vector2.zero, CursorMode.Auto);
        }
        else if (Input.GetAxis("Scissor") > 0f)
        {
            cursorMoleType = MoleType.Scissor;
            Cursor.SetCursor(cursorTextures[1], Vector2.zero, CursorMode.Auto);
        }
        else if (Input.GetAxis("Rock") > 0f)
        {
            cursorMoleType = MoleType.Rock;
            Cursor.SetCursor(cursorTextures[2], Vector2.zero, CursorMode.Auto);
        }
    }

    IEnumerator CloseGame()
    {
        yield return new WaitForSeconds(1f); // 等待1秒
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 如果在构建的版本中运行，关闭游戏
#endif
    }

}
