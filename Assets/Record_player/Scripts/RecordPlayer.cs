using UnityEngine;
using System.Collections;

public class RecordPlayer : MonoBehaviour {
//--------------------------------------------------------------------------------------------
//--------------------------------------------------------------------------------------------
//--------------------------------------------------------------------------------------------

    public bool recordPlayerActive = false;

    GameObject disc;
    GameObject arm;
    GameManager gameManager;
    public AudioSource audioSource;
    public AudioClip[] audioClips1;
    public AudioClip[] audioClips2;
    public AudioClip[] audioClips3;
    public AudioClip[] audioClips4;
    public int currentClip = 0;

    int mode;
    float armAngle;
    float discAngle;
    float discSpeed;

//--------------------------------------------------------------------------------------------
//--------------------------------------------------------------------------------------------
//--------------------------------------------------------------------------------------------
void Awake()
{
    disc = gameObject.transform.Find("teller").gameObject;
    arm = gameObject.transform.Find("arm").gameObject;
    gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
}
//--------------------------------------------------------------------------------------------
//--------------------------------------------------------------------------------------------
//--------------------------------------------------------------------------------------------
void Start()
{
    mode = 0;
    armAngle = 0.0f;
    discAngle = 0.0f;
    discSpeed = 0.0f;
    PlayMusic();
}
//--------------------------------------------------------------------------------------------
//--------------------------------------------------------------------------------------------
//--------------------------------------------------------------------------------------------
void Update()
{
        if (recordPlayerActive && !audioSource.isPlaying)
        {
            if (currentClip >= audioClips1.Length - 1)
            {
                currentClip = 0;
                recordPlayerActive = false;
                audioSource.Pause();
            }
            else
            {
                currentClip += 1;
                PlayMusic();
            }
        }
    //-- Mode 0: player off
    if(mode == 0)
    {
            if (recordPlayerActive == true)
            mode = 1;
    }
    //-- Mode 1: activation
    else if(mode == 1)
    {
        if(recordPlayerActive == true)
        {
            armAngle += Time.deltaTime * 30.0f;
            if(armAngle >= 30.0f)
            {
                armAngle = 30.0f;
                mode = 2;
            }
            discAngle += Time.deltaTime * discSpeed;
            discSpeed += Time.deltaTime * 80.0f;
        }
        else
                audioSource.Pause();
            mode = 3;
    }
    //-- Mode 2: running
    else if(mode == 2)
    {
        if(recordPlayerActive == true)
            discAngle += Time.deltaTime * discSpeed;
        else
                audioSource.Pause();
            mode = 3;
    }
    //-- Mode 3: stopping
    else
    {
        if(recordPlayerActive == false)
        {
                audioSource.Pause();
                armAngle -= Time.deltaTime * 30.0f;
            if(armAngle <= 0.0f)
                armAngle = 0.0f;

            discAngle += Time.deltaTime * discSpeed;
            discSpeed -= Time.deltaTime * 80.0f;
            if(discSpeed <= 0.0f)
                discSpeed = 0.0f;

            if((discSpeed == 0.0f) && (armAngle == 0.0f))
                mode = 0;
        }
        else
            mode = 1;
    }

    //-- update objects
    arm.transform.localEulerAngles = new Vector3(0.0f, armAngle, 0.0f);
    disc.transform.localEulerAngles = new Vector3(0.0f, discAngle, 0.0f);
}
    //--------------------------------------------------------------------------------------------
    //--------------------------------------------------------------------------------------------
    //--------------------------------------------------------------------------------------------
    public void PlayMusic()
    {
        if (gameManager.currentDay == 1)
        {
            audioSource.spatialBlend = 0.7f;
            audioSource.clip = audioClips1[currentClip];
        }
        if (gameManager.currentDay == 2)
        {
            audioSource.spatialBlend = 0.7f;
            audioSource.clip = audioClips2[currentClip];
        }
        if (gameManager.currentDay == 3)
        {
            audioSource.clip = audioClips3[currentClip];
            audioSource.spatialBlend = 0.5f;
        }
        if (gameManager.currentDay == 4)
        {
            audioSource.clip = audioClips4[currentClip];
            audioSource.spatialBlend = 0.3f;
        }
        audioSource.Play();
    }
}
