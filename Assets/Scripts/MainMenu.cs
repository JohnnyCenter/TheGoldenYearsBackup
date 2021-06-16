using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.InputSystem.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject Play, Options, OptionsMenu, Quit, soundSlider, musicSlider, voiceSlider, fade;
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
    public float newVoice = -30f;

    public void Start()
    {
        fadetoblack = fade.GetComponent<Image>();
        Mixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("music", newMusic));
        Mixer.SetFloat("SoundVolume", PlayerPrefs.GetFloat("sound", newSound));
        Mixer.SetFloat("VoiceVolume", PlayerPrefs.GetFloat("voice", newSound));
        soundSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("sound", newSound);
        musicSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("music", newMusic);
        voiceSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("voice", newVoice);
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
    public void Voice(float soundlevel)
    {
        Mixer.SetFloat("VoiceVolume", soundlevel);
        newVoice = soundlevel;
        PlayerPrefs.SetFloat("voice", soundlevel);
    }
}

