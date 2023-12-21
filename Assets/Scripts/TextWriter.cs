using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class TextWriter : MonoBehaviour
{
    private Text messageText;
    private string textToWrite;
    private int characterIndex;
    private float timePerCharacter;
    private float timer;
    private bool invisableCharacters;
    private Action onComplete;
    private Action onPoseComplete;
    public bool isDisplayingText;

    
    public void AddWriter(Text uiText, string textToWrite, float timePerCharacter, bool invisableCharacters, Action onComplete, Action onPoseComplete )
    {
        this.messageText = uiText;
        this.textToWrite = textToWrite;
        this.timePerCharacter = timePerCharacter;
        this.invisableCharacters = invisableCharacters;
        characterIndex = 0;
        this.onComplete = onComplete;
        this.onPoseComplete = onPoseComplete;
        isDisplayingText = true;

    }
    

    public void Update()
    {
        if (messageText != null)
        {
            timer -= Time.deltaTime;
            while (timer <= 0f)
            {
                // Display next character
                timer += timePerCharacter;
                characterIndex++;
                string text = messageText.text = textToWrite.Substring(0, characterIndex);
                if(invisableCharacters)
                {
                    text += "<color=#00000000>" + textToWrite.Substring(characterIndex) + "</color>";
                }
                messageText.text = text;

                if (characterIndex >= textToWrite.Length)
                {
                    // The entire string has been displayed
                    isDisplayingText = false;
                    messageText = null;
                    return;
                }
            }
        }
    }

    public void DisplayWholeTextImmediately()
    {
        if (isDisplayingText && messageText != null)
        {
            characterIndex = textToWrite.Length; // Set characterIndex to the length of the text
            string text = textToWrite;
            if (invisableCharacters)
            {
                text += "<color=#00000000></color>"; // Add invisible characters if needed
            }
            messageText.text = text; // Display the entire text
            isDisplayingText = false; // Set the flag to false when text is displayed immediately
        }
    }

}