using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance { get; private set; }

    [SerializeField] private TutorialMoleSpawner tutorialMoleSpawner;
    [SerializeField] private GameObject moleSpawner;
    [SerializeField] private TextMeshProUGUI tutorialText;
    [SerializeField] private String[] allTutorialText;
    [SerializeField] private GameObject tutorialBg;

    private int _currentTutorialTextIndex;
    
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
        tutorialBg.SetActive(true);
        tutorialText.gameObject.SetActive(true);
        tutorialText.text = allTutorialText[_currentTutorialTextIndex] + " (Left click to continue)";

        StartCoroutine(RunTutorial());
    }

    private IEnumerator RunTutorial()
    {
        // introduction phase
        yield return WaitForLeftClick();
        
        tutorialText.text = allTutorialText[++_currentTutorialTextIndex] + " (Left click to continue)";
        yield return WaitForLeftClick();
        
        tutorialText.text = allTutorialText[++_currentTutorialTextIndex] + " (Left click to continue)";
        yield return WaitForLeftClick();
        
        // teach cursor switching
        tutorialText.text = allTutorialText[++_currentTutorialTextIndex] + " (Left click to continue)";
        yield return WaitForLeftClick();

        tutorialText.text = allTutorialText[++_currentTutorialTextIndex] + " (Press '2' to continue)";
        yield return WaitForKeyPress(KeyCode.Alpha2);
        
        tutorialText.text = allTutorialText[++_currentTutorialTextIndex] + " (Press '3' to continue)";
        yield return WaitForKeyPress(KeyCode.Alpha3);
        
        // teach smash mole
        tutorialMoleSpawner.SpawnTutorialMole(0);
        tutorialText.text = allTutorialText[++_currentTutorialTextIndex] + " (Left click to continue)";
        yield return WaitForLeftClick();

        tutorialText.text = allTutorialText[++_currentTutorialTextIndex] + " (Defeat mole to continue)";
        yield return WaitForTutorialMoleDeath();
        
        // teach score and time limit
        tutorialText.text = allTutorialText[++_currentTutorialTextIndex] + " (Left click to continue)";
        yield return WaitForLeftClick();
        
        tutorialText.text = allTutorialText[++_currentTutorialTextIndex] + " (Left click to continue)";
        yield return WaitForLeftClick();
        
        tutorialText.text = allTutorialText[++_currentTutorialTextIndex] + " (Left click to continue)";
        yield return WaitForLeftClick();
        
        tutorialText.text = allTutorialText[++_currentTutorialTextIndex] + " (Left click to continue)";
        yield return WaitForLeftClick();
        
        // teach random mole spawn
        tutorialText.text = allTutorialText[++_currentTutorialTextIndex] + " (Left click to continue)";
        moleSpawner.SetActive(true);
        yield return WaitForLeftClick();
        
        // teach finish game
        tutorialText.text = allTutorialText[++_currentTutorialTextIndex] + " (Reach target score to finish tutorial)";
    }

    private IEnumerator WaitForKeyPress(KeyCode key)
    {
        bool done = false;
        while(!done)
        {
            if(Input.GetKeyDown(key))
            {
                done = true;
            }
            yield return null;
        }
    }

    private IEnumerator WaitForLeftClick()
    {
        bool done = false;
        while(!done)
        {
            if(Input.GetMouseButtonDown(0))
            {
                done = true;
            }
            yield return null;
        }
    }

    private IEnumerator WaitForTutorialMoleDeath()
    {
        bool done = false;
        while (!done)
        {
            if (tutorialMoleSpawner.myMole == null)
            {
                done = true;
            }

            yield return null;
        }
    }
}
