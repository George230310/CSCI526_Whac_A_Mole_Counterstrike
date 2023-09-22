using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameHUD : MonoBehaviour
{
    public static GameHUD Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private GameObject endScreen;  // 结束界面
    [SerializeField] private TextMeshProUGUI endMessageText;  // 显示"You Win!"或"You Lose!"的文本

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + CursorManager.Instance.gameScore.ToString();
        timeText.text = "Time: " + Mathf.Ceil(CursorManager.Instance.gameTime).ToString();  // 显示整数时间
    }

    public void ShowEndScreen(string message)
    {
        endScreen.SetActive(true);
        endMessageText.text = message;
    }
}
