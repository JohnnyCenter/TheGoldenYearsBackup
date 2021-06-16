using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("References")]
    public GameObject player;
    public DoorManager doorManager;
    public ObjectDistorter objectDistorter;
    public PostProcessVolume postProcessVolume;
    public RecordPlayer recordPlayer;
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
        recordPlayer = GameObject.FindGameObjectWithTag("Music").GetComponent<RecordPlayer>();
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
        recordPlayer.recordPlayerActive = false;
        recordPlayer.audioSource.Stop();
        recordPlayer.currentClip = 0;
        UpdateDay();
    }

    private void UpdateDay()
    {
        currentDay++;
        
        if (currentDay == 1)
        {
            player.transform.position = day1StartPos.position;
            postProcessVolume.profile = profiles[0];
            questTextReference.text = day1Quest;
            day1QuestObj.SetActive(true);
            recordPlayer.recordPlayerActive = true;
            recordPlayer.PlayMusic();

        }
        if (currentDay == 2)
        {
            player.transform.position = day2StartPos.position;
            postProcessVolume.profile = profiles[1];
            objectDistorter.IncreaseDistortion(25f);
            questTextReference.text = day2Quest;
            day2QuestObj.SetActive(true);
            recordPlayer.recordPlayerActive = true;
            recordPlayer.PlayMusic();

        }
        if (currentDay == 3)
        {
            player.transform.position = day3StartPos.position;
            postProcessVolume.profile = profiles[2];
            objectDistorter.IncreaseDistortion(50f);
            questTextReference.text = day3Quest;
            day3QuestObj.SetActive(true);
            recordPlayer.recordPlayerActive = true;
            recordPlayer.PlayMusic();

        }
        if (currentDay == 4)
        {
            player.transform.position = day4StartPos.position;
            postProcessVolume.profile = profiles[3];
            objectDistorter.IncreaseDistortion(100f);
            questTextReference.text = day4Quest;
            day4QuestObj.SetActive(true);
            recordPlayer.recordPlayerActive = true;
            recordPlayer.PlayMusic();

            //special stuff
            doorManager.SwapDoors(7, 2);
            Destroy(day4fire);
            ktb_d4_collider.SetActive(true);
        }
        if (currentDay == 5)
        {
            SceneManager.LoadScene(2);

        }
    }
}
