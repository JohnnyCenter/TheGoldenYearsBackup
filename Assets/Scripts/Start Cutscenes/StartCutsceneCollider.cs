using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Playables;
using UnityEngine.UI;
using TMPro;

public class StartCutsceneCollider : MonoBehaviour
{


    PlayerManagerTemporary p_manager;
    PlayerRaycast p_ray;
    [SerializeField] GameObject target;
    [SerializeField] Transform lookAtPoint;
    Animator targetAnimator;
    [SerializeField] float cutsceneLenght;
    //[SerializeField] PlayableDirector timeline;
    [Header("Next Quest")]
    [SerializeField] bool isQuest;
    TMP_Text questTextReference;
    [SerializeField] GameObject nextQuest;
    [SerializeField] string nextQuestText;
    //[SerializeField] AudioSource audioClip;

    [Header("Colors")]
    [SerializeField] Color questActive;
    [SerializeField] Color questDone;

    [Header("SwapDoors")]
    [SerializeField] bool shouldSwapDoors;
    [SerializeField] int doorIndex;
    [SerializeField] int tpToIndex;
    DoorManager doorManager;
    Animator blackScreen;
    Transform backToPos;

    private void Awake()
    {

        blackScreen = GameObject.Find("BlackScreen").GetComponent<Animator>();
        doorManager = GameObject.Find("Door/ObjectManager").GetComponent<DoorManager>();
        p_manager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManagerTemporary>();
        p_ray = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerRaycast>();
        questTextReference = GameObject.Find("QuestText").GetComponent<TextMeshProUGUI>();
        //p_manager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManagerTemporary>();

        if(s1AmountOfVoicelines < 2.5f)
        {
            internalS1Played = 1;
        }
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
        if (isQuest)
        {
            Camera.main.transform.LookAt(lookAtPoint);
            questTextReference.color = questDone;
        }
        if(target != null)
        {
            if (target.GetComponent<Animation>() != null)
            {
                target.GetComponent<Animation>().Play();
            }
            if (target.GetComponent<AudioSource>() != null)
            {
                target.GetComponent<AudioSource>().Play();
            }
            if (target.GetComponent<Animator>() != null)
            {
                targetAnimator = target.GetComponent<Animator>();
                backToPos = target.transform;
                target.transform.LookAt(transform);
            }
        }

        if (shouldSwapDoors)
        {
            doorManager.SwapDoors(doorIndex, tpToIndex);
        }

        if (playVoice)
        {
            PlayVoiceLines();
        } else
        {
            ContinueGame();
        }

        yield return null;
    }

    void ContinueGame()
    {
        if (!p_ray.doorAnimPlay && !p_ray.doorWalkThrough)
        {
            p_manager.EnablePlayerAll();
        }
        if (isQuest)
        {
            questTextReference.color = questActive;
            questTextReference.text = nextQuestText;
        }
            nextQuest.SetActive(true);

        if (target != null)
        {
            if (target.tag == "Son")
        {
            blackScreen.SetBool("Fade", true);
            Invoke("StopBlackScreenAnim", 1.5f);
        }
        }
    }

    void StopBlackScreenAnim()
    {
        Debug.Log("Stop BlackScreen");
        blackScreen.SetBool("Fade", false);
        Destroy(target);
        Destroy(gameObject);
    }

    #region Voice References
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
    [SerializeField] bool isTwoSpeakers;
    [SerializeField] bool speaker1isTalkingFirst;
    [Header("Speaker 2")]
    [Tooltip("None = 0, Helga = 1, Benjamin = 2, Robert = 3")]
    [Range(0, 3)]
    [SerializeField] int speaker2 = 0;
    [SerializeField] int speaker2startIndex = 0;    
    [SerializeField] int s2AmountOfVoicelines = 0;
    int internalS1Played = 0, internalS2Played = 0;
    float currentVoicelineTime = 10;
    #endregion

    void PlayVoiceLines()
    {
        if (!isTwoSpeakers) {speaker2 = 0; speaker2startIndex = 0; s2AmountOfVoicelines = 0; }
        if (playVoice)
        {
            if(speaker2 > 0) //IF 2 SPEAKERS
            {

                if (speaker1isTalkingFirst)
                {
                    if (internalS1Played > s1AmountOfVoicelines && internalS2Played > s2AmountOfVoicelines)
                    {
                        ContinueGame();
                    }

                    if(internalS1Played <= s1AmountOfVoicelines)
                    {
                        if (speaker1 == 1)
                        {
                            SoundManager.PlayVoice(SoundManager.Sound.VO_Helga, speaker1startIndex);
                            currentVoicelineTime = SoundManager.GetVoiceDuration(SoundManager.Sound.VO_Helga, speaker1startIndex);
                        }
                        if (speaker1 == 2)
                        {
                            SoundManager.PlayVoice(SoundManager.Sound.VO_Benjamin, target.transform.position, speaker1startIndex);
                            currentVoicelineTime = SoundManager.GetVoiceDuration(SoundManager.Sound.VO_Benjamin, speaker1startIndex);
                        }
                        if (speaker1 == 3)
                        {
                            SoundManager.PlayVoice(SoundManager.Sound.VO_Robert, target.transform.position, speaker1startIndex);
                            currentVoicelineTime = SoundManager.GetVoiceDuration(SoundManager.Sound.VO_Robert, speaker1startIndex);
                        }

                        StartCoroutine(WaitForSecondsCoroutine());
                    }

                } else
                {
                    if (internalS1Played > s1AmountOfVoicelines && internalS2Played > s2AmountOfVoicelines)
                    {
                        ContinueGame();
                    }

                    if (internalS2Played <= s2AmountOfVoicelines)
                    {
                        if (speaker2 == 1)
                        {
                            SoundManager.PlayVoice(SoundManager.Sound.VO_Helga, speaker2startIndex);
                            currentVoicelineTime = SoundManager.GetVoiceDuration(SoundManager.Sound.VO_Helga, speaker2startIndex);
                        }
                        if (speaker2 == 2)
                        {
                            SoundManager.PlayVoice(SoundManager.Sound.VO_Benjamin, target.transform.position, speaker2startIndex);
                            currentVoicelineTime = SoundManager.GetVoiceDuration(SoundManager.Sound.VO_Benjamin, speaker2startIndex);
                        }
                        if (speaker2 == 3)
                        {
                            SoundManager.PlayVoice(SoundManager.Sound.VO_Robert, target.transform.position, speaker2startIndex);
                            currentVoicelineTime = SoundManager.GetVoiceDuration(SoundManager.Sound.VO_Robert, speaker2startIndex);
                        }

                        if (target.GetComponent<Animator>() != null)
                        {
                            targetAnimator.SetBool("isTalking", true);
                        }

                        StartCoroutine(WaitForSecondsCoroutine());
                    }

                }
            }

            else //IF 1 SPEAKER
            {
                if(internalS1Played > s1AmountOfVoicelines)
                {
                    ContinueGame();
                }

                if(internalS1Played <= s1AmountOfVoicelines)
                {
                    if (speaker1 == 1)
                    { 
                        SoundManager.PlayVoice(SoundManager.Sound.VO_Helga, speaker1startIndex);
                        currentVoicelineTime = SoundManager.GetVoiceDuration(SoundManager.Sound.VO_Helga, speaker1startIndex);
                    }
                    if (speaker1 == 2)
                    {
                        SoundManager.PlayVoice(SoundManager.Sound.VO_Benjamin, target.transform.position, speaker1startIndex); 
                        currentVoicelineTime = SoundManager.GetVoiceDuration(SoundManager.Sound.VO_Benjamin, speaker1startIndex);
                    }
                    if (speaker1 == 3)
                    { 
                        SoundManager.PlayVoice(SoundManager.Sound.VO_Robert, target.transform.position, speaker1startIndex);
                        currentVoicelineTime = SoundManager.GetVoiceDuration(SoundManager.Sound.VO_Robert, speaker1startIndex);
                    }

                    StartCoroutine(WaitForSecondsCoroutine());
                }

            }
        }

        void AddInternalCount()
        {

            if (!isTwoSpeakers)
            {
                internalS1Played++;
                speaker1startIndex++;
            }

            if (isTwoSpeakers)
            {
                if (speaker1isTalkingFirst)
                {
                    internalS1Played++;
                    speaker1startIndex++;
                }
                else
                {
                    internalS2Played++;
                    speaker2startIndex++;
                }
            }
            
        }

        IEnumerator WaitForSecondsCoroutine()
        {
            yield return new WaitForSeconds(currentVoicelineTime);
            AddInternalCount();
            if (!speaker1isTalkingFirst)
            {
                if(target != null)
                {
                    if (target.GetComponent<Animator>() != null)
                    {
                        targetAnimator.SetBool("isTalking", false);
                    }
                }
            }
            speaker1isTalkingFirst ^= true;
            yield return new WaitForSeconds(.7f);
            PlayVoiceLines();

            yield return null;
        }
    }








}
