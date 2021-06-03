using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This class is for distorting objects, and is setup to be a singleton for ease of acess from other classes.
/// </summary>

public class ObjectDistorter : MonoBehaviour
{
    //For setting up singleton
    public static ObjectDistorter Instance;

    //Creates a list of all objects in the scene marked as "CanBeDistorted"
    private GameObject[] distortableObjects;
    private List<SkinnedMeshRenderer> meshRenderers;

    //For testing
    [Header("For testing and demonstration")]
    [Tooltip("Toggle this on to distort models manually")]
    [SerializeField] private bool overideScriptWithInputFromInspector;
    [Range(0f, 100f)]
    [Tooltip("How much to distort the models")]
    [SerializeField] private float ManualInputForDistortion;

    private void Awake()
    {
        Instance = this;
        InitializeLists();
    }
    private void Start()
    {
        ResetShapeKeys();
    }

    private void Update()
    {
        if(!overideScriptWithInputFromInspector)
        {
            return;
        }
        DistortManually(ManualInputForDistortion);
    }
    //Use this function to increase the distortion with a value. The distortion is set to 0 at the start of the game. 100 is max distortion.
    public void IncreaseDistortion(float _value)
    {
        if(!overideScriptWithInputFromInspector)
        {
            foreach (SkinnedMeshRenderer renderer in meshRenderers)
            {
                float currentDistortion = renderer.GetBlendShapeWeight(0);
                float newDistortion = currentDistortion + _value;
                renderer.SetBlendShapeWeight(0, newDistortion);
            }
        }

    }

    #region For testing and demonstration

    private void DistortManually(float _value)
    {
        foreach (SkinnedMeshRenderer renderer in meshRenderers)
        {
            float currentDistortion = renderer.GetBlendShapeWeight(0);
            Mathf.Clamp(_value, 0, 100);
            renderer.SetBlendShapeWeight(0, _value);
            
        }
    }
    #endregion

    #region Initialization and stuff
    private void InitializeLists()
    {
        distortableObjects = GameObject.FindGameObjectsWithTag("CanBeDistorted");
        meshRenderers = new List<SkinnedMeshRenderer>();
        foreach(GameObject _object in distortableObjects)
        {
            //Get the mesh renderers and add them
            SkinnedMeshRenderer localRenderer = _object.GetComponent<SkinnedMeshRenderer>();
            meshRenderers.Add(localRenderer);
        }
        //print("Distortable objects contains: " + distortableObjects.Length);
        //print("Found this many skinned mesh renderers: " + meshRenderers.Count);
    }
    
    private void ResetShapeKeys()
    {
        foreach(SkinnedMeshRenderer renderer in meshRenderers)
        {
            renderer.SetBlendShapeWeight(0, 0);
        }
    }
    #endregion

}
