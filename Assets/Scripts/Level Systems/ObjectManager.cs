using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public List<GameObject> objects;
    public List<Transform> tpObjectPoints;

    private void Awake()
    {
        SoundManager.Initialize();
    }
    public void SwapObject(int objectIndex, int tpPointIndex)
    {
        objects[objectIndex].transform.position = tpObjectPoints[tpPointIndex].transform.position;
        objects[objectIndex].transform.rotation = tpObjectPoints[tpPointIndex].transform.rotation;
    }
    public void EnableObject(int objectIndex)
    {
        objects[objectIndex].SetActive(false);
    }
    public void DisableObject(int objectIndex)
    {
        objects[objectIndex].SetActive(true);
    }
    public void ResetObjects()
    {
        for (int i = 0; i < objects.Count; i++)
        {
            objects[i].transform.position = tpObjectPoints[i].transform.position;
            objects[i].SetActive(true);
        }
    }
}