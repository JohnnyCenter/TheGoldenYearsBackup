using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    TMPro.TextMeshProUGUI questTextReference;

    [Header("Colors")]
    [SerializeField] Color questActive;
    [SerializeField] Color questDone;
    [SerializeField] string nextQuestText;

    private void Awake()
    {
        questTextReference = GameObject.Find("QuestText").GetComponent<TextMeshProUGUI>();
    }

    public void SetNewQuest()
    {
        //SET QUESTTEXT QUEST
        //questText.text =
    }

    public void EndDay()
    {
        questTextReference.color = questActive;
    }

    public void GeneralStartDay()
    {
        questTextReference.color = questActive;
        questTextReference.text = nextQuestText;

    }
}
