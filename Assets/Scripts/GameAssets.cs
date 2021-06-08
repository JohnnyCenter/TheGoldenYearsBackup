using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class GameAssets : MonoBehaviour
{
    private static GameAssets _i;

    public static GameAssets i
    {
        get
        {
            if (_i == null)
                _i = Instantiate(Resources.Load<GameAssets>("GameAssets"));
                return _i;
        }
    }
    public SoundAudioClip[] soundAudioClips;
    
    [System.Serializable]
    public class SoundAudioClip
    {
        public SFXManager.Sound sound;
        public AudioClip[] audioClips;
    }
}
