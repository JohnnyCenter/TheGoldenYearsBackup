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

    GameObject teleportBedObject;
    GameObject musicObject;
    [SerializeField] Vector3 returnToPlayerPos;

    [Header("Other References")]
    [SerializeField] Animator blackscreen_anim;
    [SerializeField] LayerMask interactableMask;

    [Header("Variables")]
    [SerializeField] float raycastLength = 4f;

    bool seesDoor = false, seesBed = false, seesPickup = false, canPlaceObject = false, seesAnimation = false, seesMusic = true;
    Vector3 pickupOriginalPos; Quaternion pickupOriginalRot;
    [HideInInspector] public bool rayIsDisabled = false;
    [HideInInspector] GameObject animationObject;
    public GameObject door;
    public bool doorAnimPlay;
    public bool doorWalkThrough;

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
                    teleportBedObject = hit.transform.GetComponent<BedScript>().bedTransform.gameObject;
                }   
                
                if(hit.collider.tag == "Door")
                {
                    if (door == null)
                    {
                        door = hit.collider.gameObject;
                    }
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

                if (hit.collider.tag == "Music")
                {
                    seesMusic = true;
                    musicObject = hit.transform.gameObject;
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
                seesMusic = false;
                playerManager.objectsSeen.Clear();
                if (door != null && !doorWalkThrough)
                {
                    door = null;
                }
            }

            #endregion
        }
        
    }
    private void FixedUpdate()
    {
        if (doorAnimPlay) //starts walking to transform position, then changes the registered door and teleports
        {
            cam.transform.LookAt(door.GetComponent<NOTLonely_Door.DoorScript>().knob.transform.position);
            transform.position = Vector3.MoveTowards(transform.position, door.GetComponent<NOTLonely_Door.DoorScript>().playRoom.transform.position, 3 * Time.fixedDeltaTime);
            if (Vector3.Distance(transform.position, door.GetComponent<NOTLonely_Door.DoorScript>().playRoom.transform.position) < 0.001f)
            {
                transform.position = door.GetComponent<NOTLonely_Door.DoorScript>().teleportRoom.transform.position;
                transform.rotation = door.GetComponent<NOTLonely_Door.DoorScript>().teleportRoom.transform.rotation;
                OpenDoor();
            }
        }
        if (doorWalkThrough)//when animation is finished, walk through door and close it
        {
            if (!door.GetComponent<Animation>().isPlaying)
            {
                transform.position = Vector3.MoveTowards(transform.position, door.GetComponent<NOTLonely_Door.DoorScript>().playRoom.transform.position, 3 * Time.fixedDeltaTime);
                if (Vector3.Distance(transform.position, door.GetComponent<NOTLonely_Door.DoorScript>().playRoom.transform.position) < 0.001f)
                {
                    CloseDoor();
                }
            }
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

            if (seesMusic)
            {
                if (!musicObject.GetComponent<RecordPlayer>().recordPlayerActive)
                {
                    musicObject.GetComponent<RecordPlayer>().recordPlayerActive = true;
                    musicObject.GetComponent<RecordPlayer>().PlayMusic();
                }
                else
                {
                    musicObject.GetComponent<RecordPlayer>().recordPlayerActive = false;
                }
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
        if (pickup_obj.GetComponent<Pickups>().canAddToInventory)
        {

            p_inventory.OnTriggerPickup();
            playerManager.EnablePlayerAll();
            //PLAY SOUND

            yield return new WaitForSeconds(.1f);
            canAddToInventory = false;

        }
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

        //PLAY PICKUPSOUND

        if (pickup_obj.GetComponent<StartCutsceneOnPickup>() != null)
        {
            pickup_obj.GetComponent<StartCutsceneOnPickup>().StartCutscene();
        }

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
        teleportBedObject.GetComponent<BedScript>().GoToBed();

        playerManager.EnablePlayerAll();
        yield return null;
    }

    void GoToBed()
    {
        returnToPlayerPos = gameObject.transform.position;
        transform.position = teleportBedObject.transform.position; 
        transform.rotation = teleportBedObject.transform.rotation;

        teleportBedObject.GetComponent<BedScript>().GoToBed();
    }

    
    #endregion

    #region door things
    //HER KAN DU KALLE PÅ FUNKSJONEN FOR Å ÅPNE DØRA OSV
    void OpenDoor()
    {
        doorAnimPlay = false;//fixed update bool
        door = door.GetComponent<NOTLonely_Door.DoorScript>().teleportRoom.parent.gameObject;//swap door to tp point door
        //isLoadingScreen.SetActive(true);
        cam.transform.LookAt(door.GetComponent<NOTLonely_Door.DoorScript>().knob.transform.position);
        door.GetComponent<NOTLonely_Door.DoorScript>().OpenDoor();
        doorWalkThrough = true;//fixed update bool
        //PLAY ANIM
        //PLAY SOUND EFFECT
    }
    void CloseDoor()
    {
        doorWalkThrough = false;//fixed update bool
        door.GetComponent<NOTLonely_Door.DoorScript>().CloseDoor();
        door = null;
    }

    private IEnumerator DoorAnim()
    {
        playerManager.DisablePlayerAll();
        doorAnimPlay = true;//fixed update bool

        //HVOR MANGE SEKUNDER VARER ANIM;
        yield return new WaitForSeconds(3f);

        //isLoadingScreen.SetActive(false);
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

