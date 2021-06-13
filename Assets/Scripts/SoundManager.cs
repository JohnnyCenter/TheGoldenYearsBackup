using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager
{
    private static Dictionary<Sound, float> soundTime;
    private static GameObject soundGameObject;
    private static AudioSource soundAudioSource;
    private static GameObject oneShotObject;
    private static AudioSource oneShotAudioSource;
    private static float clipLength;
    public enum Sound
    {
        SFX_FootstepsParquet,
        SFX_FootstepsCarpet,
        SFX_FootstepsPlaster,
        SFX_DoorOpen,
        SFX_DoorHandle,
        SFX_DoorClose,
        SFX_Inspect,
        SFX_Place,
        SFX_Bed,
        SFX_StartMusic,
        SFX_StopMusic,
        UI_StartGame,
        UI_Quit,
        UI_Pause,
        UI_Unpause,
        UI_PauseResume,
        UI_PauseQuit,
        VO_Helga,
        VO_Benjamin,
        VO_Robert,
        STING_Stage1,
        STING_Stage2,
        STING_Stage3
    }
    /// <summary>
    /// 3D audio sound from position.
    /// </summary>
    /// <param name="sound"></param>
    /// <param name="position"></param>
    public static void PlaySFX(Sound sound, Vector3 position)
    {
        if (CanPlay(sound))
        {
            if (soundGameObject == null)
            {
                soundGameObject = new GameObject("3D Sound");
                soundGameObject.transform.position = position;
                soundAudioSource = soundGameObject.AddComponent<AudioSource>();
                //soundAudioSource.outputAudioMixerGroup.audioMixer.FindMatchingGroups("Sound");
                soundAudioSource.maxDistance = 100;
                soundAudioSource.spatialBlend = 1;
                soundAudioSource.rolloffMode = AudioRolloffMode.Linear;
                soundAudioSource.dopplerLevel = 0;
            }
            else
            {
                soundAudioSource.transform.position = position;
            }
            soundAudioSource.PlayOneShot(GetAudioClip(sound));
        }
    }
    /// <summary>
    /// 2D audio sound.
    /// </summary>
    /// <param name="sound"></param>
    public static void PlaySFX(Sound sound)
    {
        if (CanPlay(sound))
        {
            if (oneShotObject == null)
            {
                oneShotObject = new GameObject("2D Sound");
                oneShotAudioSource = oneShotObject.AddComponent<AudioSource>();
            }
        
        
        oneShotAudioSource.PlayOneShot(GetAudioClip(sound));
        }
    }
    public static void PlayVoice(Sound sound, Vector3 position, int index)
    {
        if (CanPlay(sound))
        {
            if (soundGameObject == null)
            {
                soundGameObject = new GameObject("3D Sound");
                soundGameObject.transform.position = position;
                soundAudioSource = soundGameObject.AddComponent<AudioSource>();
                soundAudioSource.maxDistance = 100;
                soundAudioSource.spatialBlend = 1;
                soundAudioSource.rolloffMode = AudioRolloffMode.Linear;
                soundAudioSource.dopplerLevel = 0;
            }
            else
            {
                soundAudioSource.transform.position = position;
            }
            soundAudioSource.PlayOneShot(GetVoiceClip(sound, index));
        }
    }
    public static void PlayVoice(Sound sound, int index)
    {
        if (CanPlay(sound))
        {
            if (oneShotObject == null)
            {
                oneShotObject = new GameObject("2D Sound");
                oneShotAudioSource = oneShotObject.AddComponent<AudioSource>();
            }
            oneShotAudioSource.PlayOneShot(GetVoiceClip(sound, index));
        }
    }
    public static void Initialize()
    {
        soundTime = new Dictionary<Sound, float>();
        soundTime[Sound.SFX_FootstepsParquet] = 0;
        soundTime[Sound.SFX_FootstepsCarpet] = 0;
    }
    private static bool CanPlay(Sound sound)
    {
        switch (sound)
        {
            default: return true;
            case Sound.SFX_FootstepsParquet:
                if (soundTime.ContainsKey(sound))
                {
                    float lastTimePlayed = soundTime[sound];
                    float playerMoveTimerMax = .8f;
                    if (lastTimePlayed + playerMoveTimerMax < Time.time)
                    {
                        soundTime[sound] = Time.time;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }
            /*case Sound.SFX_FootstepsCarpet:
                if (soundTime.ContainsKey(sound))
                {
                    float lastTimePlayed = soundTime[sound];
                    float playerMoveTimerMax = .08f;
                    if (lastTimePlayed + playerMoveTimerMax > Time.time)
                    {
                        soundTime[sound] = Time.time;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }*/
        }
    }
    private static AudioClip GetAudioClip(Sound sound)
    {
        foreach (GameAssets.SoundAudioClip soundAudioClip in GameAssets.i.soundAudioClips)
        {
            if (soundAudioClip.sound == sound)
            {
                int sfxIndex = Random.Range(0, soundAudioClip.audioClips.Length - 1);
                return soundAudioClip.audioClips[sfxIndex];
            }
        }
        return null;
    }
    private static AudioClip GetVoiceClip(Sound sound, int index)
    {
        foreach (GameAssets.VOAudioClip VoiceClip in GameAssets.i.VoiceClips)
        {
            if (VoiceClip.sound == sound)
            {
                return VoiceClip.audioClips[index];
            }
        }
        return null;
    }

    public static float GetVoiceDuration(Sound sound, int index)
    {
        foreach (GameAssets.VOAudioClip VoiceClip in GameAssets.i.VoiceClips)
        {
            if (VoiceClip.sound == sound)
            {
                clipLength = VoiceClip.audioClips[index].length;
            }
        }
        return clipLength;
    }
}
