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

    public float gameTime = 30.0f;  // ����Ϸʱ��
    public bool gameEnded = false;  // ��Ϸ�Ƿ����

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

        // ֹͣ���������
        MoleSpawner[] spawners = FindObjectsOfType<MoleSpawner>();
        foreach (var spawner in spawners)
        {
            spawner.StopAllCoroutines();
        }

        // ��ʾ��������
        GameHUD.Instance.ShowEndScreen(message);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameEnded)
        {
            // �ر���Ϸ
            StartCoroutine(CloseGame());
            return; // ��ִ������Ĵ���
        }
        if (!gameEnded)
        {
            gameTime -= Time.deltaTime;  // ������Ϸʱ��
            if (gameTime <= 0 || gameScore >= 500) // ��ӵ÷ּ��
            {
                gameEnded = true;

                // ��ʾ������Ļ���ı�
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
        yield return new WaitForSeconds(1f); // �ȴ�1��
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // ����ڹ����İ汾�����У��ر���Ϸ
#endif
    }

}
