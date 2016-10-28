using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Controllers;
using Assets.Scripts.Input.Gameplay;
using UnityEngine.UI;

public class StoryHandler : MonoBehaviour {

    [SerializeField]
    private string[] sentences;
    [SerializeField] 
    private Text text;
    [SerializeField]
    private int fadeInOutDuration = 3;
    [SerializeField]
    private LoadLevelAsync levelLoader;
    [SerializeField]
    private int nextLevel;

    private int currentAlphaValue = 0;

    private bool fadeIn = true;
    private bool fadeOut = false;
    private int sentencesCounter;
    //private int textRyhtm;
    private int nextSentence;
    private int showCpt;
    private int fadeDurationCpt;
    private float alphaValue;

    private GamepadInputHandler _inputHandler;
    private KeyboardInputHandler _keyboardInput;

    private void Awake()
    {
        //text.CrossFadeAlpha(0f, 0f, false);
    }

	private void Start () 
    {
        _inputHandler = GetComponent<GamepadInputHandler>();
        _inputHandler.OnAPressed += Skip;

        _keyboardInput = GetComponent<KeyboardInputHandler>();
        _keyboardInput.OnSkip += Skip;

        alphaValue = 0;
        text.GetComponent<CanvasRenderer>().SetAlpha(alphaValue);
        sentencesCounter = 0;
        fadeInOutDuration *= 60;
        text.text = sentences[sentencesCounter];
        //textRyhtm = (sentences[sentencesCounter].Length)*2;
        //text.CrossFadeAlpha(1, fadeInOutDuration, false);
        nextSentence = (sentences[sentencesCounter].Length); 
	}

    private void OnDestroy()
    {
        _inputHandler.OnAPressed -= Skip;
        _keyboardInput.OnSkip -= Skip;
    }

	private void Update ()
	{
	    if (fadeIn)
	    {
	        fadeDurationCpt++;
	        if (fadeDurationCpt >= fadeInOutDuration)
	        {
                showCpt++;
                if (showCpt >= nextSentence)
                {
                    fadeIn = false;
                    FadeOut();
                    showCpt = 0;
                    fadeDurationCpt = 0;
                }
	        }
	        else
	        {
	            FadeIn();
	        }
	    }
	    else
	    {
	        fadeDurationCpt++;
	        if (fadeDurationCpt >= fadeInOutDuration)
	        {
	            fadeIn = true;
	            sentencesCounter++;
	            if (sentencesCounter < sentences.Length)
	            {
                    nextSentence = (sentences[sentencesCounter].Length);
	                text.text = sentences[sentencesCounter];
	            }
	            else
	            {
	                levelLoader.ClickToLoadAsync(nextLevel);
	            }
                fadeDurationCpt = 0;
	        }
	        else
	        {
	            FadeOut();
	        }
	    }

	    /*if(Time.time>=nextSentence && fadeOut)
        {
            Debug.Log("NextSentence");
            sentencesCounter++;
            if (sentencesCounter < sentences.Length)
            {
                textRyhtm = sentences[sentencesCounter].Length / 20;
                nextSentence = Time.time + textRyhtm;
                text.text = sentences[sentencesCounter];
                text.CrossFadeAlpha(1, fadeInOutDuration, false);
                fadeOut = false;
                fadeIn = true;
                
            }
            else
            {
                levelLoader.ClickToLoadAsync(nextLevel);
            }
        }
        else if(Time.time>=nextSentence && fadeIn)
        {       
            nextSentence = Time.time + fadeInOutDuration;
            text.CrossFadeAlpha(0, fadeInOutDuration, false);
            fadeOut = true;
            fadeIn = false;
        }*/
	}
    private void Skip()
    {
        //nextSentence = Time.time;
        if (!fadeIn)
        {
            sentencesCounter++;
            if (sentencesCounter < sentences.Length)
            {
                nextSentence = (sentences[sentencesCounter].Length);
                text.text = sentences[sentencesCounter];
            }
            else
            {
                levelLoader.ClickToLoadAsync(nextLevel);
            }
            fadeDurationCpt = 0;
            alphaValue = 0;
            text.GetComponent<CanvasRenderer>().SetAlpha(alphaValue);
            fadeIn = true;
        }
        else
        {
            fadeIn = false;
            fadeDurationCpt = 0;
        }

    }

    private void FadeOut()
    {
        alphaValue -= 0.01f;
        text.GetComponent<CanvasRenderer>().SetAlpha(alphaValue);

        if (alphaValue <= 0)
        {
            fadeOut = false;
            fadeDurationCpt = fadeInOutDuration;
        }
    }

    private void FadeIn()
    {
        alphaValue += 0.01f;
        text.GetComponent<CanvasRenderer>().SetAlpha(alphaValue);

    }

    
}
