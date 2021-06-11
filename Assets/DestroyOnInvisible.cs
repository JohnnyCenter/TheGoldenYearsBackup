using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnInvisible : MonoBehaviour
{
    bool canDestroy = false;
    private void OnBecameVisible()
    {
        canDestroy = true;
    }

    private void OnBecameInvisible()
    {
        if (canDestroy)
        {
            Destroy(gameObject);
        }
    }
}
