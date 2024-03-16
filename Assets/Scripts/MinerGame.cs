using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MinerGame : MonoBehaviour
{
    [SerializeField] private GameObject minigameRendererObject;
    [SerializeField] private GameObject minigameBackgroundRendererObject;
    [SerializeField] private List<Sprite> keyboardPrompts;
    [SerializeField] private List<Sprite> gamepadPrompts;
    [SerializeField] private List<Sprite> completionSprites;
    [SerializeField] private int promptCount = 8;
    
    private SpriteRenderer minigameRenderer;
    private SpriteRenderer backgroundRenderer;
    private playerCrystalManager playerCrystalManager;
    
    private bool isGamepad = true;
    private bool gameRunning = false;
    private int currentPrompt = 0;
    private int inputtedPrompt = -1;
    [SerializeField] public int currentPromptCount = 0;
    
    private void Start()
    {
        this.minigameRenderer = minigameRendererObject.GetComponent<SpriteRenderer>();
        this.playerCrystalManager = GetComponent<playerCrystalManager>();
        this.backgroundRenderer = minigameBackgroundRendererObject.GetComponent<SpriteRenderer>();
        this.isGamepad = GetComponent<Player>().isGamepadControl();
        minigameRenderer.sprite = null;
    }
    
    public void startGame()
    {
        this.gameRunning = true;
        this.currentPromptCount = 0;
        // Generate the first prompt for a gamepad
        this.generatePrompt(true, 0); // Assuming the first prompt is correct
    }


    public void abortGame()
    {
        Debug.Log("the end");
        this.gameRunning = false;
    }
    
    public void endGame()
    {
        Debug.Log("the end");
        this.gameRunning = false;
        minigameRenderer.sprite = null;
        backgroundRenderer.sprite = null;
        playerCrystalManager.stopMining();
    }
    
    private void generatePrompt(bool isGamepad, int correctButton)
    {
        if (currentPromptCount >= promptCount)
        {
            endGame();
            return;
        }
        this.isGamepad = isGamepad;
        this.currentPrompt = randomizePrompt();
        this.currentPromptCount++;
        Debug.Log($"Generated prompt: {this.currentPrompt}, Is Gamepad: {isGamepad}");
        if (isGamepad)
        {
            this.minigameRenderer.sprite = gamepadPrompts[this.currentPrompt];
        }
        else
        {
            this.minigameRenderer.sprite = keyboardPrompts[this.currentPrompt];
        }
        this.backgroundRenderer.sprite = completionSprites[this.currentPromptCount-1];
        Debug.Log($"Current sprite: {this.minigameRenderer.sprite}");
        
        this.inputtedPrompt = -1; // Reset inputted prompt
    }

    private void Update()
    {
        if (gameRunning && inputtedPrompt != -1)
        {
            if (inputtedPrompt == currentPrompt)
            {
                generatePrompt(isGamepad, randomizePrompt());
            }
            else
            {
                StartCoroutine(ShakePrompt());
            }
            inputtedPrompt = -1; // Reset inputted prompt
        }
    }

    void OnB1(InputValue value)
    {
        this.inputtedPrompt = 0;
    }
    
    void OnB2(InputValue value)
    {
        this.inputtedPrompt = 1;
    }
    
    void OnB3(InputValue value)
    {
        this.inputtedPrompt = 2;
    }
    
    void OnB4(InputValue value)
    {
        this.inputtedPrompt = 3;
    }
    
    private int randomizePrompt()
    {
        return UnityEngine.Random.Range(0, 4);
    }

    IEnumerator ShakePrompt()
    {
        Vector3 originalPosition = minigameRenderer.transform.position;
        for (int i = 0; i < 5; i++)
        {
            minigameRenderer.transform.position = originalPosition + new Vector3(UnityEngine.Random.Range(-0.1f, 0.1f), UnityEngine.Random.Range(-0.1f, 0.1f), 0);
            yield return new WaitForSeconds(0.1f);
        }
        minigameRenderer.transform.position = originalPosition;
    }

}
