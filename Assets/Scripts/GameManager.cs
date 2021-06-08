using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Changes automatically when going to bed")]
    [SerializeField] int currentDay;


    //[Header("References")]
    TMPro.TextMeshProUGUI questTextReference;

    [Header("Colors")]
    [SerializeField] Color questActive;
    [SerializeField] Color questDone;
    [SerializeField] string nextQuestText;

    private void Awake()
    {
        currentDay = 1;
        questTextReference = GameObject.Find("QuestText").GetComponent<TextMeshProUGUI>();
    }


    public void EndDay()
    {
        questTextReference.color = questDone;
    }

    public void GeneralStartDay()
    {
        questTextReference.color = questActive;
        questTextReference.text = nextQuestText;

        UpdateDay();
    }

    private void UpdateDay()
    {
        //public Day _day = Day.day1;
        //Day += 1;
        //if (Day == numberOfWeaponTypes) weaponType = 0;

        ////CALL NEXT DAY FUNCTION
        //if (_day == Day.day1)
        //{

        //}        
        
        //if (_day == Day.day2)
        //{

        //}
    }
}
