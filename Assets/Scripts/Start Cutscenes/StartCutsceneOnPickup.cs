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
    //[SerializeField] bool destroyOnInvisible;

    bool canDestroyNow = false;

    [Header("Colors")]
    [SerializeField] Color questActive;
    [SerializeField] Color questDone;

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

    private IEnumerator PlayEvent()
    {
        questTextReference.color = questDone;
        p_manager.DisablePlayerAll();
        Camera.main.transform.LookAt(target.transform);

        //timeline.Play();
        if (target.GetComponent<Animation>() != null)
        {
            target.GetComponent<Animation>().Play();
        }
        if (target.GetComponent<AudioSource>() != null)
        {
            target.GetComponent<AudioSource>().Play();
        }


        yield return new WaitForSeconds(cutsceneLenght);

        //if (destroyOnInvisible)
        //{
        //    canDestroyNow = true;
        //}

        questTextReference.color = questActive;
        questTextReference.text = nextQuestText;

        p_manager.EnablePlayerAll();
        nextQuest.SetActive(true);

        //Destroy(gameObject);
        
        yield return null;
    }

    //private void OnBecameInvisible()
    //{
    //    if (canDestroyNow)
    //    {
    //        Destroy(gameObject);
    //    }
    //}
}

