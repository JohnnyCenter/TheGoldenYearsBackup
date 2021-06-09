using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Changes automatically when going to bed")]
    [SerializeField] int currentDay;


    [Header("Start Day Quests")]
    [SerializeField] string day1Quest, day2Quest, day3Quest, day4Quest, day5Quest, day6Quest, day7Quest;

    //[Header("References")]
    TMPro.TextMeshProUGUI questTextReference;

    [Header("Colors")]
    [SerializeField] Color questActive;
    [SerializeField] Color questDone;

    private void Awake()
    {
        currentDay = 0;
        UpdateDay();
        questTextReference = GameObject.Find("QuestText").GetComponent<TextMeshProUGUI>();
    }


    public void EndDay()
    {
        questTextReference.color = questDone;
    }

    public void GeneralStartDay()
    {
        //KALLES NÅR MAN STÅR OPP FRA SENGA
        questTextReference.color = questActive;

        UpdateDay();
    }

    private void UpdateDay()
    {
        currentDay++;

        if(currentDay == 1)
        { //JONATHAN SKRIV DIN KODE HER
            questTextReference.text = day1Quest;

        }
        if(currentDay == 2)
        {
            questTextReference.text = day2Quest;
        }
        if(currentDay == 3)
        {
            questTextReference.text = day3Quest;
        }
        if(currentDay == 4)
        {
            questTextReference.text = day4Quest;
        }
        if(currentDay == 5)
        {
            questTextReference.text = day5Quest;
        }
        if(currentDay == 6)
        {
            questTextReference.text = day6Quest;
        }
        if(currentDay == 7)
        {
            questTextReference.text = day7Quest;
        }
    }
}
