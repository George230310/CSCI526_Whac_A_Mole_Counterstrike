using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameHUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    
    // Update is called once per frame
    void Update()
    {
        scoreText.text = CursorManager.Instance.gameScore.ToString();
    }
}
