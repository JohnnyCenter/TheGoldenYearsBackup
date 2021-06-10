using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Playables;
using UnityEngine.UI;
using TMPro;

public class StartCutsceneCollider : MonoBehaviour
{
    PlayerManagerTemporary p_manager;
    [SerializeField] GameObject target;
    [SerializeField] float cutsceneLenght;
    //[SerializeField] PlayableDirector timeline;
    [Header("Next Quest")]
    TMP_Text questTextReference;
    [SerializeField] GameObject nextQuest;
    [SerializeField] string nextQuestText;
    //[SerializeField] AudioSource audioClip;

    [Header("Colors")]
    [SerializeField] Color questActive;
    [SerializeField] Color questDone;

    private void Awake()
    {
        p_manager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManagerTemporary>();
        questTextReference = GameObject.Find("QuestText").GetComponent<TextMeshProUGUI>();
        //p_manager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManagerTemporary>();
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            StartCoroutine(PlayEvent());
        }
    }

    private IEnumerator PlayEvent()
    {
        p_manager.DisablePlayerAll();
        Camera.main.transform.LookAt(target.transform);
        questTextReference.color = questDone;
        //timeline.Play();
        if(target.GetComponent<Animation>() != null)
        {
            target.GetComponent<Animation>().Play();
        }
        if (target.GetComponent<AudioSource>() != null)
        {
            target.GetComponent<AudioSource>().Play();
        }


        yield return new WaitForSeconds(cutsceneLenght);


        questTextReference.color = questActive;
        questTextReference.text = nextQuestText;
        p_manager.EnablePlayerAll();
        nextQuest.SetActive(true);

        Destroy(gameObject);
        yield return null;
    }

    [Header("Voice Over Yes/No")]    
    [SerializeField] bool playVoice;

    [Header("Speaker 1")]
    [Range(1, 3)]
    [Tooltip("Helga = 1, Benjamin = 2, Robert = 3")]
    [SerializeField] int speaker1 = 1;
    [Tooltip("Set index they are starting with, AmountOfVoiceLines will say how many to continue for")]
    [SerializeField] int speaker1startIndex = 0;
    [SerializeField] int s1AmountOfVoicelines = 0;

    [Header("Is there a Speaker 2?")]
    [Tooltip("True = Yes, False = No")]
    [SerializeField] bool speaker2YesOrNo;
    [Header("Speaker 2")]
    [Tooltip("None = 0, Helga = 1, Benjamin = 2, Robert = 3")]
    [Range(0, 3)]
    [SerializeField] int speaker2 = 0;
    [SerializeField] int speaker2startIndex = 0;    
    [SerializeField] int s2AmountOfVoicelines = 0;
    int internalS1Played = 0, internalS2Played = 0;
    bool talkSwitch;

    void PlayVoiceLines()
    {
        if (!speaker2YesOrNo) {speaker2 = 0; speaker2startIndex = 0; s2AmountOfVoicelines = 0; }
        if (playVoice)
        {
            if(speaker2 > 0) //IF 2 SPEAKERS
            {
                if (talkSwitch)
                {
                    if(internalS1Played >= s1AmountOfVoicelines)
                    {
                        if (speaker1 == 1)
                        { SoundManager.PlayVoice(SoundManager.Sound.VO_Helga, speaker1startIndex); }
                        if (speaker1 == 2)
                        { SoundManager.PlayVoice(SoundManager.Sound.VO_Benjamin, speaker1startIndex); }
                        if (speaker1 == 3)
                        { SoundManager.PlayVoice(SoundManager.Sound.VO_Robert, speaker1startIndex); }

                        //talkSwitch = !talkSwitch;
                        talkSwitch ^= true;
                    }

                    if (internalS1Played >= s1AmountOfVoicelines && internalS2Played >= s2AmountOfVoicelines)
                    {
                        //CONTINUE GAME
                        Debug.Log("Countinue Game");
                    }
                    else
                    {
                        PlayVoiceLines();
                    }

                    //IF ERROR MAYBE THESE CAN BE PLACED BEFORE THE FUNCTION OVER
                    internalS1Played++;
                    speaker1startIndex++;
                } else
                {
                    if(internalS2Played >= s2AmountOfVoicelines)
                    {
                        if (speaker2 == 1)
                        { SoundManager.PlayVoice(SoundManager.Sound.VO_Helga, speaker1startIndex); }
                        if (speaker2 == 2)
                        { SoundManager.PlayVoice(SoundManager.Sound.VO_Benjamin, target.transform.position, speaker1startIndex); }
                        if (speaker2 == 3)
                        { SoundManager.PlayVoice(SoundManager.Sound.VO_Robert, target.transform.position, speaker1startIndex); }

                        talkSwitch ^= true;
                    }

                    if (internalS1Played >= s1AmountOfVoicelines && internalS2Played >= s2AmountOfVoicelines)
                    {
                        Debug.Log("Continue Game");
                    }
                    else
                    {
                        PlayVoiceLines();
                    }

                    internalS2Played++;
                    speaker2startIndex++;
                }
            }

            else //IF 1 SPEAKER
            {
                if(speaker1 == 1)
                { SoundManager.PlayVoice(SoundManager.Sound.VO_Helga, speaker1startIndex);}                
                if(speaker1 == 2)
                { SoundManager.PlayVoice(SoundManager.Sound.VO_Benjamin, target.transform.position, speaker1startIndex);}                
                if(speaker1 == 3)
                { SoundManager.PlayVoice(SoundManager.Sound.VO_Robert, target.transform.position, speaker1startIndex);}


                internalS1Played++;
                speaker1startIndex++;

                if(internalS1Played >= s1AmountOfVoicelines)
                {
                    //CONTINUE GAME
                }
                else
                {
                    PlayVoiceLines();
                }
            }
        }
    }








}
