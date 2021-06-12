using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetObjectsActive : MonoBehaviour
{
    [SerializeField] GameObject object1, object2;

    private void Awake()
    {
        object1.SetActive(true);
        object1.SetActive(true);
        //Destroy(gameObject);
    }
    private void OnEnable()
    {
        object1.SetActive(true);
        object1.SetActive(true);
    }
}
