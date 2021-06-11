using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnInvisible : MonoBehaviour
{
    bool canDestroy = false;
    bool canDestroy2 = false;

    [SerializeField] int howLongUntiilCanDestroy;

    private void Update()
    {

        if(Time.time > howLongUntiilCanDestroy)
        {
            canDestroy2 = true;
        }
    }
    private void OnBecameVisible()
    {
        canDestroy = true;
    }

    private void OnBecameInvisible()
    {
        if (canDestroy && canDestroy2)
        {
            Destroy(gameObject);
        }
    }
}
