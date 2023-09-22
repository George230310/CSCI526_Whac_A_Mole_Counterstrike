using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameHUD : MonoBehaviour
{
    public static GameHUD Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private GameObject endScreen;  // ��������
    [SerializeField] private TextMeshProUGUI endMessageText;  // ��ʾ"You Win!"��"You Lose!"���ı�

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
        timeText.text = "Time: " + Mathf.Ceil(CursorManager.Instance.gameTime).ToString();  // ��ʾ����ʱ��
    }

    public void ShowEndScreen(string message)
    {
        endScreen.SetActive(true);
        endMessageText.text = message;
    }
}
