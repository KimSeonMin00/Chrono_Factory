using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SoundManager : Singleton<SoundManager>
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource m_bgmSource;
    [SerializeField] private List<AudioSource> m_sfxSources;

    [SerializeField] private AudioSource m_alertSource;
    [SerializeField] private AudioSource m_portalSource;

    [Header("AudioClip")]
    public AudioClip m_clickSound;
    public AudioClip m_upgradeSound;
    public AudioClip m_alertSound;
    public AudioClip m_machineSound;
    public AudioClip m_portalSound;


    public void PlaySFX(AudioClip clip, float volume = 1.0f)
    {
        if (clip == null) 
            return;

        foreach(var source in m_sfxSources)
        {
            if(!source.isPlaying)
            {
                source.clip = clip;
                source.volume = volume;
                source.Play();
                return;
            }
        }

        m_sfxSources[0].PlayOneShot(clip, volume);
    }

    public void PlayBGM(AudioClip clip, bool loop = true)
    {
        m_bgmSource.clip = clip;
        m_bgmSource.loop = loop;
        m_bgmSource.Play();
    }

    public void PlayAlert()
    {
        if (!m_alertSource.isPlaying)
        {
            m_alertSource.clip = m_alertSound;
            m_alertSource.volume = 2.0f;
            m_alertSource.Play();
        }       
    }
    public void PlayPortal()
    {
        if (!m_portalSource.isPlaying)
        {
            m_portalSource.clip = m_portalSound;
            m_portalSource.volume = 1.0f;
            m_portalSource.Play();
        }
    }

    public void StopAllSound()
    {
        foreach (var source in m_sfxSources)
        {
            if (source.isPlaying)
            {
                source.Stop();
            }
        }

        if (m_alertSource.isPlaying)
            m_alertSource.Stop();

        Extractor.currentPlayingCount = 0;
    }
}
