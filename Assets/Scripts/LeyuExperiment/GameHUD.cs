using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameHUD : MonoBehaviour
{
    public static GameHUD Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private GameObject endScreen;  // end game screen
    [SerializeField] private TextMeshProUGUI endMessageText;  // end game message
    [SerializeField] private TextMeshProUGUI targetScoreText;

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
        if (CursorManager.Instance)
        {
            scoreText.text = "Current Score: " + CursorManager.Instance.gameScore;
            timeText.text = "Time Remaining: " + Mathf.Ceil(CursorManager.Instance.gameTime) + "s";  // display time remaining
            targetScoreText.text = "Target Score: " + CursorManager.Instance.targetScore;
        }
    }

    public void ShowEndScreen(string message)
    {
        endScreen.SetActive(true);
        endMessageText.text = message;
    }
}
