using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MinerGame : MonoBehaviour
{
    [SerializeField] private GameObject rightPrompt;
    [SerializeField] private GameObject leftPrompt;
    [SerializeField] private List<Sprite> keyboardPrompts;
    [SerializeField] private List<Sprite> gamepadPrompts;
    [SerializeField] private float promptTime = 2.0f;
    [SerializeField] private int promptCount = 10;
    
    private SpriteRenderer lrend;
    private SpriteRenderer rrend;
    
    private bool isGamepad = true;
    private bool gameRunning = false;
    private int currentPrompt = 0;
    private int inputtedPrompt = -1;
    private float lastGeneratedPrompt = 0.0f;
    private int currentPromptCount = 0;
    
    private bool right = false;
    // Update is called once per frame
    private void Start()
    {
        this.rrend = rightPrompt.GetComponent<SpriteRenderer>();
        this.lrend = leftPrompt.GetComponent<SpriteRenderer>();
        this.generatePrompt(true);
    }

    void playerWrong()
    {
        Debug.Log("Wrong");
        right = !right;
    }
    void playerCorrect()
    {
        Debug.Log("Correct");
        this.currentPromptCount++;
        right = !right;
    }
    void Update()
    {
        if (gameRunning) 
        {
            if(currentPromptCount >= promptCount)
            {
                this.abortGame();
                return;
            }
            if (inputtedPrompt == currentPrompt)
            {
                this.playerCorrect();
                this.generatePrompt();
                refreshSprites();
                inputtedPrompt = -1;
            }
            else if(inputtedPrompt != -1)
            {
                this.playerWrong();
                this.generatePrompt();
                refreshSprites();
                inputtedPrompt = -1;
            }
            this.generatePrompt();
            refreshSprites();
        }
        else
        {
            this.updateSprite(null);
        }
    }

    void refreshSprites()
    {
        if (isGamepad)
        {
            this.updateSprite(this.gamepadPrompts[currentPrompt]);
        }
        else
        {
            this.updateSprite(this.keyboardPrompts[currentPrompt]);
        }
    }
    void updateSprite(Sprite sprite)
    {
        if (sprite == null)
        {
            this.lrend.sprite = sprite;
            this.rrend.sprite = sprite;
        }
        else if (right)
        {
            this.rrend.sprite = sprite;
            this.lrend.sprite = null;
        }
        else
        {
            this.rrend.sprite = null;
            this.lrend.sprite = sprite;
        }
        
    }
    
    void generatePrompt(bool passed = false)
    {
        if(Time.time - lastGeneratedPrompt < promptTime && !passed)
            return;
        this.currentPrompt = randomizePrompt();
        this.lastGeneratedPrompt = Time.time;
        if (!passed)
            this.playerWrong();
    }

    public void startGame()
    {
        Debug.Log("the start");
        this.gameRunning = true;
    }

    public void abortGame()
    {
        Debug.Log("the end");
        this.gameRunning = false;
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
}
