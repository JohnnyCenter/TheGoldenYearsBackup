using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering.PostProcessing;

public class GameManager : MonoBehaviour
{
    [Header("References")]
    public GameObject player;
    public DoorManager doorManager;
    public ObjectDistorter objectDistorter;
    public PostProcessVolume postProcessVolume;
    [SerializeField]
    public PostProcessProfile[] profiles;

    [Header("FOR PLAYTESTING ONLY - MUST BE AT 0")]
    public int currentDay;
    [Space]


    [Header("Start Day Quests")]
    public string day1Quest; public string day2Quest; public string day3Quest; public string day4Quest; public string day5Quest;
    [Space]
    [Header("Start Day First Object")]
    public GameObject day1QuestObj; public GameObject day2QuestObj; public GameObject day3QuestObj; public GameObject day4QuestObj; public GameObject day5QuestObj;
    [Space]
    [Header("Start Day Starting Pos")]
    public Transform day1StartPos; public Transform day2StartPos; public Transform day3StartPos; public Transform day4StartPos; public Transform day5StartPos;
    [Space]
    [Header("Delete Object")] 
    public GameObject day4fire, ktb_d4_collider;


    //[Header("References")]
    TMP_Text questTextReference;

    [Header("Colors")]
    [SerializeField] Color questActive;
    [SerializeField] Color questDone;

    private void Awake()
    {
        //currentDay = 0;
        questTextReference = GameObject.Find("QuestText").GetComponent<TextMeshProUGUI>();
        objectDistorter = GameObject.Find("ObjectDistorter").GetComponent<ObjectDistorter>();
        GeneralStartDay();
    }


    public void EndDay()
    {
        questTextReference.color = questDone;
    }

    public void GeneralStartDay()
    {
        day1QuestObj.SetActive(false); day2QuestObj.SetActive(false); day3QuestObj.SetActive(false); day4QuestObj.SetActive(false);
        //KALLES NÅR MAN STÅR OPP FRA SENGA
        questTextReference.color = questActive;
        doorManager.ResetDoors();

        UpdateDay();
    }

    private void UpdateDay()
    {
        currentDay++;

        if (currentDay == 1)
        {
            //JONATHAN SKRIV DIN KODE HER
            //EGIL SKRIV DIN KODE HER
            postProcessVolume.profile = profiles[0];
            questTextReference.text = day1Quest;
            day1QuestObj.SetActive(true);
            player.transform.position = day1StartPos.position;
        }
        if (currentDay == 2)
        {
            postProcessVolume.profile = profiles[1];
            objectDistorter.IncreaseDistortion(25f);
            questTextReference.text = day2Quest;
            day2QuestObj.SetActive(true);
            player.transform.position = day2StartPos.position;
        }
        if (currentDay == 3)
        {
            postProcessVolume.profile = profiles[2];
            objectDistorter.IncreaseDistortion(50f);
            questTextReference.text = day3Quest;
            day3QuestObj.SetActive(true);
            player.transform.position = day3StartPos.position;
        }
        if (currentDay == 4)
        {
            postProcessVolume.profile = profiles[3];
            objectDistorter.IncreaseDistortion(100f);
            questTextReference.text = day4Quest;
            day4QuestObj.SetActive(true);
            player.transform.position = day4StartPos.position;

            //special stuff
            doorManager.SwapDoors(7, 2);
            Destroy(day4fire);
            ktb_d4_collider.SetActive(true);
        }
        if (currentDay == 5)
        {
            postProcessVolume.profile = profiles[4];
            questTextReference.text = day5Quest;
            day5QuestObj.SetActive(true);
            player.transform.position = day5StartPos.position;
        }
    }
}
