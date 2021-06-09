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
    [Header("Drag all assets that will be distorted into this array")]
    [SerializeField] private GameObject[] distortableObjects;
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
        if(Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
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
    public void IncreaseDistortion(float _value) //Use this function to increase the distortion with a value. 
    //The distortion is set to 0 at the start of the game. 100 is max distortion.
    {
        if (!overideScriptWithInputFromInspector)
        {
            foreach (SkinnedMeshRenderer renderer in meshRenderers)
            {
                float currentDistortion = renderer.GetBlendShapeWeight(0); //Gets the current level of distortion
                float newDistortion = currentDistortion + _value; //Adds the new distortion to the current level
                Mathf.Clamp(newDistortion, 0, 100); //Clamps the values
                renderer.SetBlendShapeWeight(0, newDistortion); //Sets the distortion to the new clamped value
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

    #region Initialization
    private void InitializeLists()
    {
        meshRenderers = new List<SkinnedMeshRenderer>();
        foreach(GameObject _object in distortableObjects)
        {
            //Get the mesh renderers and add them
            _object.TryGetComponent<SkinnedMeshRenderer>(out SkinnedMeshRenderer localRenderer);
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
