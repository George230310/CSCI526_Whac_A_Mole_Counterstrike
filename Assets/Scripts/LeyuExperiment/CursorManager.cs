using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public static CursorManager Instance { get; private set; }

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

    // Update is called once per frame
    void Update()
    {
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
}
