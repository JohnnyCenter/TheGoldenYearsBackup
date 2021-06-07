using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using TMPro;

public class StartCutscene : MonoBehaviour
{
    PlayerManagerTemporary p_manager;
    [SerializeField] GameObject target;
    [SerializeField] float cutsceneLenght;
    //[SerializeField] PlayableDirector timeline;
    [Header("Next Quest")]
    TMPro.TextMeshProUGUI questTextReference;
    [SerializeField] GameObject nextQuest;
    [SerializeField] string nextQuestText;

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
}
