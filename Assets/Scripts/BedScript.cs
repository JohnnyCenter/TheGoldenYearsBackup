using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedScript : MonoBehaviour
{
    [SerializeField] public Transform bedTransform;
    GameManager gm;

    private void Awake()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void GoToBed()
    {
        gm.EndDay();
    }
}
