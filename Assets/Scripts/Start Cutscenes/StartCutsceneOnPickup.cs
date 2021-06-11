using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using TMPro;

public class StartCutsceneOnPickup : MonoBehaviour
{
    PlayerManagerTemporary p_manager;
    GameObject target;
    [SerializeField] float cutsceneLenght;
    //[SerializeField] PlayableDirector timeline;
    [Header("Next Quest")]
    TMP_Text questTextReference;
    [SerializeField] GameObject nextQuest;
    [SerializeField] string nextQuestText;
    //[SerializeField] AudioSource audioClip;

    [SerializeField] bool destroyOnInvisible;
    bool canDestroyNow = false;
    Animator targetAnimator;


    [Header("Colors")]
    [SerializeField] Color questActive;
    [SerializeField] Color questDone;

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
    [Header("Speaker 2")]
    [Tooltip("None = 0, Helga = 1, Benjamin = 2, Robert = 3")]
    [Range(0, 3)]
    [SerializeField] int speaker2 = 0;
    [SerializeField] int speaker2startIndex = 0;
    [SerializeField] int s2AmountOfVoicelines = 0;
    int internalS1Played = 0, internalS2Played = 0;
    bool talkSwitch;
    float currentVoicelineTime = 10;
    #endregion

    private void Awake()
    {
        p_manager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManagerTemporary>();
        questTextReference = GameObject.Find("QuestText").GetComponent<TextMeshProUGUI>();
        target = transform.gameObject;
        //p_manager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManagerTemporary>();
    }

    public void StartCutscene()
    {
        StartCoroutine(PlayEvent());
    }

    private void OnBecameInvisible()
    {
        if (canDestroyNow)
        {
            //Destroy(gameObject);
        }
    }

    private IEnumerator PlayEvent()
    {
        questTextReference.color = questDone;
        p_manager.DisablePlayerAll();
        Camera.main.transform.LookAt(target.transform);

        //timeline.Play();
        if (target.GetComponent<Animator>() != null)
        {
            targetAnimator = target.GetComponent<Animator>();
        }
        if (target.GetComponent<Animation>() != null)
        {
            target.GetComponent<Animation>().Play();
        }
        if (target.GetComponent<AudioSource>() != null)
        {
            target.GetComponent<AudioSource>().Play();
        }

        if (playVoice)
        {
            PlayVoiceLines();
        }
        else
        {
            ContinueGame();
        }


        //yield return new WaitForSeconds(cutsceneLenght);
        
        yield return null;
    }

    void ContinueGame()
    {
        questTextReference.color = questActive;
        questTextReference.text = nextQuestText;

        p_manager.EnablePlayerAll();
        nextQuest.SetActive(true);
        canDestroyNow = true;
    }



    void PlayVoiceLines()
    {
        Debug.Log("Called general VoiceLine Function");
        if (!isTwoSpeakers) { speaker2 = 0; speaker2startIndex = 0; s2AmountOfVoicelines = 0; }
        if (playVoice)
        {
            if (speaker2 > 0) //IF 2 SPEAKERS
            {
                Debug.Log("2 People should be talking now");
                if (talkSwitch)
                {
                    if (internalS1Played > s1AmountOfVoicelines && internalS2Played > s2AmountOfVoicelines)
                    {
                        ContinueGame();
                    }

                    if (internalS1Played <= s1AmountOfVoicelines)
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
                else
                {
                    if (internalS1Played > s1AmountOfVoicelines && internalS2Played > s2AmountOfVoicelines)
                    {
                        ContinueGame();
                    }

                    if (internalS2Played <= s2AmountOfVoicelines)
                    {
                        if (speaker2 == 1)
                        {
                            SoundManager.PlayVoice(SoundManager.Sound.VO_Helga, speaker1startIndex);
                            currentVoicelineTime = SoundManager.GetVoiceDuration(SoundManager.Sound.VO_Helga, speaker1startIndex);
                        }
                        if (speaker2 == 2)
                        {
                            SoundManager.PlayVoice(SoundManager.Sound.VO_Benjamin, target.transform.position, speaker1startIndex);
                            currentVoicelineTime = SoundManager.GetVoiceDuration(SoundManager.Sound.VO_Benjamin, speaker1startIndex);
                        }
                        if (speaker2 == 3)
                        {
                            SoundManager.PlayVoice(SoundManager.Sound.VO_Robert, target.transform.position, speaker1startIndex);
                            currentVoicelineTime = SoundManager.GetVoiceDuration(SoundManager.Sound.VO_Robert, speaker1startIndex);
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
                if (internalS1Played > s1AmountOfVoicelines)
                {
                    ContinueGame();
                }

                if (internalS1Played <= s1AmountOfVoicelines)
                {
                    Debug.Log("1 Person should be talking now");
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
                if (talkSwitch)
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
            Debug.Log("WaitForSecondsShitCalled");
            yield return new WaitForSeconds(currentVoicelineTime);
            AddInternalCount();
            if (!talkSwitch)
            {
                if (target.GetComponent<Animator>() != null)
                {
                    targetAnimator.SetBool("isTalking", false);
                }
            }
            talkSwitch ^= true;
            yield return new WaitForSeconds(.7f);
            PlayVoiceLines();

            yield return null;
        }
    }
}

