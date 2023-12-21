using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.CompilerServices;





public class UI_Manager : MonoBehaviour
{
    [SerializeField] private TextWriter textWriter;  
    [SerializeField] float textspeed;
    [SerializeField] GameObject paySlip;
    [SerializeField] GameObject chatBox;
    [SerializeField] GameObject payslip_IMG;  
    private PetRex petRex;  
    public AudioSource talkingAudioSource;
    public Animator talkingAnimation;
    public Animator UiAnimation;
    private Text messageText;
    private int currentIndex = 0;
    [SerializeField] public AudioClip[] talkingClips;
    [SerializeField] public AnimationClip[] talkingAnimations;
    [SerializeField] private Transform Startlocation;
    [SerializeField] private Transform Paysliplocation;
    [SerializeField] private GameObject[] fashingUiList;
    [SerializeField] private GameObject QuitApplication;
    private bool isTextDisplaying = false;
    private GameObject Rex;
    private GameObject Hints;
    

   
    private void Awake() {
        
        petRex = GameObject.Find("PatPoint").GetComponent<PetRex>();
        Hints = GameObject.Find("Hints");
        messageText = GameObject.Find("MessageText").GetComponent<Text>();
        paySlip = GameObject.Find("Payslip");
        GameObject.Find("Next").GetComponent<Button>().onClick.AddListener(OnClickNext);
        talkingAudioSource = GameObject.Find("TalkingAudio").GetComponent<AudioSource>();
        talkingAnimation = GameObject.Find("Fox").GetComponent<Animator>();
        UiAnimation = GameObject.Find("Payslip_IMG").GetComponent<Animator>();
        paySlip = GameObject.Find("Payslip");
        chatBox.transform.position = Startlocation.position;
        paySlip.SetActive(false);
        QuitApplication.SetActive(false);  
        foreach (GameObject ui in fashingUiList)
        {
            ui.SetActive(false);
        }
        Rex = GameObject.Find("PatPoint");
    }



    /// <summary>
    /// Box Width = 773.1414 
    /// Box Height = 792.3731
    /// </summary>

    private void OnClickNext()
    {
        
        if (!isTextDisplaying)
        {
            textWriter.DisplayWholeTextImmediately();
            isTextDisplaying = true;
            return;

        }
        
        string[] messageArray = new string[] {
            "Hello User!", // 1
            "Today  I'll help you understand how to read a payslip!", // 2
            "It's like a special note that tells you about the money you get when you work.", // 3
            "let's take a look at this pretend payslip. It's got lots of important things on it", // 4
            "This is just a pretend payslip, lets go through the important information one by one!", // 5
            "Your Employee number! this is how the company registers you in their system",// 6
            "Hey look it's my Creators name! but this is where your name would be!", //7
            "This is the date period for your payment", // 8
            "This is your National Insurance Number! it's a unique number that the government gives you", // 9
            "Here are your earnings! this is how much you get paid for working + A free Fox Star ! ^_^", // 10
            "Now, here's an important one Tax and National Insurance.These are some amounts taken out of your pay by the government to help pay for things like schools and hospitals", // 11
            "This is where your tax number might be, this tell the government what tax bracket you are one.", // 12
            "OH! the Fun part is coming up - YOUR TOTAL NET PAY! This is how much total you take home!", // 13
            "Lets have a rest! that was a lot of information", // 14
            "Reading a payslip is like solving a puzzle! It helps you understand how much you've earned and what's been taken out.", // 15
            "Understanding your payslip is a great way to learn about money and how working helps you earn it.", //16
            "Well done, User! Now you know how to read a payslip!", // 17
            "GOODBYE ^_^",// 18
            "" // 19
            //the last message will be 0 in the array.
        };
        string message = messageArray[currentIndex];
        currentIndex = (currentIndex + 1) % messageArray.Length;
        StartTalkingSound();    
        // PlaySoundClip at index
        PlaySoundClip(talkingClips[currentIndex]);
        // PlayAnimation at index
        PlayAnimation(talkingAnimations[currentIndex]);

        if (currentIndex == 0){
            PlaySoundClipDelayed(talkingClips[7], 0.5f);
        }

        foreach (GameObject ui in fashingUiList)
        {
            ui.SetActive(false);
        }
    
        
        // Case for each animation in th ui
        switch (currentIndex)
        {
            case 3:
                    
                    if(petRex.hasPetRexed)
                    {
                        Destroy(Hints);
                    }
                    else if(!petRex.hasPetRexed)
                    if (Hints != null)
                    Hints.GetComponent<Animator>().Play("ShowHints");
                    if(petRex.hasPetRexed)
                    {
                        Hints.GetComponent<Animator>().Play("HideHints");
                    }

                    break;
            case 4:
                    if (Hints != null)
                    Hints.GetComponent<Animator>().Play("HideHints");
                    break;
            case 5:
                    Debug.Log("Show Payslip");
                    Rex.gameObject.SetActive(false);
                    ShowPayslip();
                    break;
            case 6:
                    fashingUiList[0].SetActive(true);
                    ShowPayslipIMG();
                    break;
            case 7:
                    fashingUiList[1].SetActive(true);
                    ShowPayslipIMG();
                    break;
            case 8:
                    fashingUiList[2].SetActive(true);
                    ShowPayslipIMG();
                    break;
            case 9: 
                    fashingUiList[3].SetActive(true);
                    ShowPayslipIMG();
                    break;
            case 10:
                    fashingUiList[4].SetActive(true);
                    chatBox.transform.position = Startlocation.position;
                    break;
            case 11: 
                    fashingUiList[5].SetActive(true);
                    ShowPayslipIMG();
                    break;
            case 12:
                    fashingUiList[6].SetActive(true);
                    ShowPayslipIMG();
                    break;
            case 13: 
                    fashingUiList[8].SetActive(true);
                    ShowPayslipIMG();
                    Rex.gameObject.SetActive(true);
                    break;
            case 14:
                    HidePayslip();
                    break;
            case 18:
                    GameObject.Find("Next").SetActive(false);
                    QuitApplication.SetActive(true);
                    break;
            

            
            default:
                    chatBox.transform.position = Startlocation.position;
                    HidePayslip();
                    break;
        }


        Debug.Log("Current Index: " + currentIndex + "Current Animation: " + talkingAnimations[currentIndex] + "Current Clip: " + talkingClips[currentIndex]);
        textWriter.AddWriter(messageText, message, textspeed, true, StopTalkingSound, StopAnimation);
        isTextDisplaying = false;
    }
    public void PlayAnimation(AnimationClip animation)
    {
        talkingAnimation.Play(animation.name);
    }

    private void PlaySoundClip(AudioClip clip)
    {
        talkingAudioSource.clip = clip;
        talkingAudioSource.Play();
    }
    private void PlaySoundClipDelayed(AudioClip clip ,float delay)
    {
        talkingAudioSource.clip = clip;
        talkingAudioSource.PlayDelayed(delay);
    }
    private void StartTalkingSound()
    {
        if (talkingAudioSource == null)
        {
            talkingAudioSource = GameObject.Find("Fox").GetComponent<AudioSource>();
            if (talkingAudioSource == null)
            {
                talkingAudioSource = gameObject.AddComponent<AudioSource>();
            }
        }

        talkingAudioSource.Play();
    }

    private void StopTalkingSound()
    {
        talkingAudioSource.Stop();
    }

    private void StopAnimation()
    {
        talkingAnimation.StopPlayback();
    }

    private void ShowPayslip()
    {
        paySlip.SetActive(true);
        chatBox.transform.position = Paysliplocation.position;

    }
    private void ShowPayslipIMG()
    {
        payslip_IMG.SetActive(true);
    }
    private void HidePayslip()
    {
        paySlip.SetActive(false);
        chatBox.transform.position = Startlocation.position;
    }
    private string FormatWord(string message, string word, Color color, bool bold, bool italic)
    {
        string startTag = "<color=" + ColorUtility.ToHtmlStringRGB(color) + ">";
        string endTag = "</color>";

        if (bold)
        {
            startTag += "<b>";
            endTag = "</b>" + endTag;
        }

        if (italic)
        {
            startTag += "<i>";
            endTag = "</i>" + endTag;
        }

        message = message.Replace(word, startTag + word + endTag);
        return message;
    }

    public void ExitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    void Update()
    {

    }



}
