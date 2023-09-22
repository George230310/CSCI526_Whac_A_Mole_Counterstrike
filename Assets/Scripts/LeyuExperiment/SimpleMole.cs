using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoleType
{
    Paper,
    Scissor,
    Rock
}

public class SimpleMole : MonoBehaviour
{
    public float myLifeSpan = 3.0f;
    public MoleSpawner mySpawner;

    [SerializeField] private MoleType moleType;
    
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, myLifeSpan);
    }

    private void OnDestroy()
    {
        mySpawner.myMole = null;
    }

    private void OnMouseDown()
    {
        if (CursorManager.Instance.cursorMoleType == MoleType.Paper)
        {
            if (moleType == MoleType.Scissor)
            {
                CursorManager.Instance.gameScore -= 50;
            }
            else if (moleType == MoleType.Rock)
            {
                CursorManager.Instance.gameScore += 100;
            }
        }
        else if (CursorManager.Instance.cursorMoleType == MoleType.Scissor)
        {
            if (moleType == MoleType.Paper)
            {
                CursorManager.Instance.gameScore += 100;
            }
            else if (moleType == MoleType.Rock)
            {
                CursorManager.Instance.gameScore -= 50;
            }
        }
        else if (CursorManager.Instance.cursorMoleType == MoleType.Rock)
        {
            if (moleType == MoleType.Paper)
            {
                CursorManager.Instance.gameScore -= 50;
            }
            else if (moleType == MoleType.Scissor)
            {
                CursorManager.Instance.gameScore += 100;
            }
        }
    }
}
