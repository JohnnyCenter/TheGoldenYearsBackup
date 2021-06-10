using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;
using UnityEngine.InputSystem.UI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.DualShock;
using UnityEngine.InputSystem.XInput;
using UnityEngine.InputSystem.Switch;

public class MainMenu : MonoBehaviour
{
    public GameObject Play, Options, OptionsMenu, Quit, soundSlider, musicSlider, fade;
    public GameObject mainMenu;
    public InputSystemUIInputModule input;
    public AudioMixer Mixer;
    public Image fadetoblack;
    private float start = 0;
    private bool newGame = false;
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
        if (newGame)
        {
            fade.SetActive(true);
            float alpha = start += 0.33f * Time.deltaTime;
            fadetoblack.color = new Color(0, 0, 0, alpha);
            if (alpha >= 1.3)
            {
                SceneManager.LoadScene(1);
            }
        }
    }
    public void PlayGame()
    {
        newGame = true;
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
    public void QuitGame()
    {
        Application.Quit();
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

