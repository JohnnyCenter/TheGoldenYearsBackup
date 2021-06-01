using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRaycast : MonoBehaviour
{

    [Header("Script References")]
    [SerializeField] PlayerManagerTemporary playerManager;
    [SerializeField] PlayerInventory p_inventory;
    [SerializeField] PlayerLook p_look;

    [HideInInspector] public GameObject pickup_obj;
    Transform placePos;
    bool canAddToInventory = false, canPutBack = false;

    [Header("Gameobject References")]
    [SerializeField] Transform cam;
    [SerializeField] Transform rightHand;
    [SerializeField] Transform inspectObjPos;
    [SerializeField] GameObject isLoadingScreen;

    Transform teleportBedObject; 
    [SerializeField] Vector3 returnToPlayerPos;

    [Header("Other References")]
    [SerializeField] Animator blackscreen_anim;
    [SerializeField] LayerMask interactableMask;

    [Header("Variables")]
    [SerializeField] float raycastLength = 4f;

    bool seesDoor = false, seesBed = false, seesPickup = false, canPlaceObject = false, seesAnimation = false;
    Vector3 pickupOriginalPos; Quaternion pickupOriginalRot;
    [HideInInspector] public bool rayIsDisabled = false;
    [HideInInspector] GameObject animationObject;

    void Update()
    {
        if (!rayIsDisabled)
        {
            #region raycasts

            //IF RAYCAST IS EMPTY (no layermask)
            RaycastHit hit;
            if (Physics.Raycast(cam.position, cam.forward, out hit, raycastLength, interactableMask))
            {
                if (!playerManager.objectsSeen.Contains(hit.collider.gameObject))
                {
                    playerManager.objectsSeen.Add(hit.collider.gameObject);
                }

                if(hit.collider.tag == "Bed")
                {
                    seesBed = true;
                    teleportBedObject = hit.transform.GetComponent<BedScript>().bedTransform;
                }   
                
                if(hit.collider.tag == "Door")
                {

                    seesDoor = true;
                } 
                
                if(hit.collider.tag == "PlaceObject" && p_inventory.pickupList.Count > 0)
                {

                    canPlaceObject = true;
                    placePos = hit.transform;
                    p_inventory.placePickup = hit.transform.GetComponent<PlacePickup>();
                } 

                if(hit.collider.tag == "Animation")
                {
                    seesAnimation = true;
                    animationObject = hit.transform.gameObject;
                }


                if(hit.collider.tag == "Pickup")
                {

                    seesPickup = true;

                    if (canAddToInventory)
                    {
                        canPutBack = true;
                    } else
                    {
                        canPutBack = false;
                    }
                }

            } else
            {
                seesDoor = false;
                seesBed = false;
                canPlaceObject = false;
                playerManager.objectsSeen.Clear();
            }

            #endregion
        }
    }


    public void InteractPressed()
    {
        if (!rayIsDisabled)
        {
            if (seesDoor)
            {
                StartCoroutine(DoorAnim());
            }            
            
            if (seesBed)
            {
                StartCoroutine(BedAnim());
            }

            if (seesAnimation)
            {
                animationObject.GetComponent<Animation>().Play();
            }

            if (seesPickup)
            {
                RaycastHit pickup_hit;
                if (Physics.Raycast(cam.position, cam.forward, out pickup_hit, raycastLength, interactableMask))
                {
                    if (pickup_hit.collider.tag == "Pickup")
                    {

                        if(p_inventory.pickupList.Count == 0 && canAddToInventory)
                        {
                            StartCoroutine(AddObjectToInventory());
                            pickup_obj = pickup_hit.transform.gameObject;
                            //Destroy(pickup_hit.transform.gameObject);
                            
                        }

                        if (!canAddToInventory)
                        {
                            pickupOriginalPos = pickup_hit.transform.position;
                            pickupOriginalRot = pickup_hit.transform.rotation;

                            pickup_obj = pickup_hit.transform.gameObject;
                            StartCoroutine(InspectObject());
                        }
                    }
                }
            }

            if (canPlaceObject)
            {
                p_inventory.OnTriggerPlaceObject();
            }
        }
    }

    public void RightClickPressed()
    {
        if (canPutBack && canAddToInventory)
        {
            PutObjectBack();
            playerManager.EnablePlayerAll();
            canAddToInventory = false;
            canPutBack = false;
        }
    }

    void PutObjectBack()
    {
        if (seesPickup)
        {
            pickup_obj.transform.position = pickupOriginalPos;
            pickup_obj.transform.rotation = pickupOriginalRot;
            pickup_obj.transform.SetParent(null);
            canAddToInventory = false;
            p_inventory.RemovePickup();
        }
    }

    public void PlaceObject()
    {
        pickup_obj.transform.position = placePos.position;
        pickup_obj.transform.rotation = placePos.rotation;
        pickup_obj.transform.SetParent(null);
        p_inventory.RemovePickup();
        canAddToInventory = false;
    }

    private IEnumerator AddObjectToInventory()
    {
        p_inventory.OnTriggerPickup();
        playerManager.EnablePlayerAll();
        //PLAY SOUND

        yield return new WaitForSeconds(.1f);
        canAddToInventory = false;

        yield return null;
    }

    private IEnumerator InspectObject()
    {
        cam.localEulerAngles = new Vector3(0, cam.rotation.y, cam.rotation.z);
        //ERSTATT LINJA OVER MED ANIMASJON
        playerManager.DisablePlayerMoveLook();
        p_look.canInspectObj = true;
        pickup_obj.transform.position = inspectObjPos.position;
        p_look.inspectObj = pickup_obj;

        //PLAY SOUND
        canAddToInventory = true;
        yield return new WaitForSeconds(.1f);

        yield return null;
    }

    #region bed things
    private IEnumerator BedAnim()
    {
        playerManager.DisablePlayerAll();


        blackscreen_anim.SetBool("Fade", true);
        yield return new WaitForSeconds(1f);
        blackscreen_anim.SetBool("Fade", false);

        GoToBed();

        //TEMPORARY
        //isLoadingScreen.SetActive(true);


        //HOW LONG IS ANIMATION
        yield return new WaitForSeconds(2f);
        blackscreen_anim.SetBool("Fade", true);
        isLoadingScreen.SetActive(false);

        
        yield return new WaitForSeconds(.5f);
        gameObject.transform.position = returnToPlayerPos;
        yield return new WaitForSeconds(3f);

        blackscreen_anim.SetBool("Fade", false);
        //MAYBE TEMPORARY THIS UNDER:
        EndDay();

        playerManager.EnablePlayerAll();
        yield return null;
    }

    void GoToBed()
    {
        returnToPlayerPos = gameObject.transform.position;
        transform.position = teleportBedObject.position; 
        transform.rotation = teleportBedObject.rotation; 

        EndDay();
    }

    void EndDay()
    {
        //SWITCH SCENE
        //PROGRESSION COUNTER
        //MAYBE HAVE THIS IN THE GAME MANAGER
    }

    
    #endregion

    #region door things
    //HER KAN DU KALLE PÅ FUNKSJONEN FOR Å ÅPNE DØRA OSV
    void OpenDoor()
    {
        Debug.Log("OPEN DOOR");
        isLoadingScreen.SetActive(true);
        //PLAY ANIM
        //PLAY SOUND EFFECT
    }

    private IEnumerator DoorAnim()
    {
        playerManager.DisablePlayerAll();
        OpenDoor();

        //HVOR MANGE SEKUNDER VARER ANIM;
        yield return new WaitForSeconds(2f);

        isLoadingScreen.SetActive(false);
        playerManager.EnablePlayerAll();

        yield return null;
    }
    #endregion
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(cam.position, cam.forward * raycastLength);
    }
}
