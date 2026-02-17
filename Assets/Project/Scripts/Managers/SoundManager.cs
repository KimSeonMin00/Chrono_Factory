using UnityEngine;
using System.Collections.Generic;

public class SoundManager : Singleton<SoundManager>
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource m_BgmSource;
    [SerializeField] private List<AudioSource> m_SFXSources;

    [Header("AudioClip")]
    public AudioClip m_ClickSound;

    public void PlaySFX(AudioClip clip, float volume = 1.0f)
    {
        if (clip == null) 
            return;

        foreach(var source in m_SFXSources)
        {
            if(!source.isPlaying)
            {
                source.clip = clip;
                source.volume = volume;
                source.Play();
                return;
            }
        }

        m_SFXSources[0].PlayOneShot(clip, volume);
    }

    public void PlayBGM(AudioClip clip, bool loop = true)
    {
        m_BgmSource.clip = clip;
        m_BgmSource.loop = loop;
        m_BgmSource.Play();
    }
}
