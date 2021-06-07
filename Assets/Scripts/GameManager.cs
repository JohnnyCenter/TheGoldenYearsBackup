using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    enum Day{day1, day2, day3, day4, day5, day6, day7}
    int nrOfDays;
    //static Day[] vals = values();


    TMPro.TextMeshProUGUI questTextReference;

    [Header("Colors")]
    [SerializeField] Color questActive;
    [SerializeField] Color questDone;
    [SerializeField] string nextQuestText;

    private void Awake()
    {
        nrOfDays = System.Enum.GetValues(Day).Length;
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
    }

    private void UpdateDay()
    {
        public Day _day = Day.day1;
        Day += 1;
        if (Day == numberOfWeaponTypes) weaponType = 0;

        //CALL NEXT DAY FUNCTION
        if (_day == Day.day1)
        {

        }        
        
        if (_day == Day.day2)
        {

        }
    }
}
