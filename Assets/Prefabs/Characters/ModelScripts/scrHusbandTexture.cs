using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrHusbandTexture : MonoBehaviour
{
    [Range(0, 1)]
    [SerializeField] private int husbandTextureDistorter;

    private List<Material> husbandMaterials;

    private void Update()
    {
        if(husbandTextureDistorter < 0.5)
        {
            foreach(Material material in husbandMaterials)
            {
                material.color = Color.white;
            }
        }
        else if (husbandTextureDistorter > 0.5)
        {
            foreach (Material material in husbandMaterials)
            {
                material.color = Color.black;
            }
        }

    }
}
