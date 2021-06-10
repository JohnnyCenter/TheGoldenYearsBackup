using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.InputSystem.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject Play, Options, OptionsMenu, Quit, soundSlider, musicSlider, fade;
    public GameObject mainMenu;
    public InputSystemUIInputModule input;
    public AudioMixer Mixer;
    public Image fadetoblack;
    private float start = 0;
    private bool exitGame = false;
    public bool newOptionMenu;
    public bool pressed;
    public float newSound = -30f;
    public float newMusic = -30f;

    public void Start()
    {
        fadetoblack = fade.GetComponent<Image>();
        Mixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("music", newMusic));
        Mixer.SetFloat("SoundVolume", PlayerPrefs.GetFloat("sound", newSound));
        soundSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("sound", newSound);
        musicSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("music", newMusic);
    }
    
    private void Update()
    {
        if (exitGame)
        {
            fade.SetActive(true);
            float alpha = start += 0.33f * Time.deltaTime;
            fadetoblack.color = new Color(0, 0, 0, alpha);
            if (alpha >= 1.3)
            {
                SceneManager.LoadScene(0);
            }
        }
    }
    public void ExitGame()
    {
        Time.timeScale = 1;
        exitGame = true;
    }
    public void Cancel()
    {
        if (newOptionMenu)
        {
            PlayerPrefs.Save();
            OptionsMenu.SetActive(false);
            mainMenu.SetActive(true);
            newOptionMenu = false;
        }
    }
    public void OptionsMenuPopUp()
    {
        mainMenu.SetActive(false);
        OptionsMenu.SetActive(true);
        newOptionMenu = true;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        mainMenu.SetActive(false);
    }
    public void PauseGame()
    {
        
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            mainMenu.SetActive(true);
        }
        else if (!newOptionMenu)
        {
            Debug.Log("paused");
            ResumeGame();
        }
        
    }

    public void Sound(float soundlevel)
    {
        //sfxSlider.Play();
        Mixer.SetFloat("SoundVolume", soundlevel);
        newSound = soundlevel;
        PlayerPrefs.SetFloat("sound", soundlevel);
    }
    public void Music(float soundlevel)
    {
        Mixer.SetFloat("MusicVolume", soundlevel);
        newMusic = soundlevel;
        PlayerPrefs.SetFloat("music", soundlevel);
    }
}

