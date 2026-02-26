using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SoundManager : Singleton<SoundManager>
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource m_BgmSource;
    [SerializeField] private List<AudioSource> m_SFXSources;

    [SerializeField] private AudioSource m_AlertSource;
    [SerializeField] private AudioSource m_PortalSource;

    [Header("AudioClip")]
    public AudioClip m_ClickSound;
    public AudioClip m_UpgradeSound;
    public AudioClip m_AlertSound;
    public AudioClip m_MachineSound;
    public AudioClip m_PortalSound;


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

    public void PlayAlert()
    {
        if (!m_AlertSource.isPlaying)
        {
            m_AlertSource.clip = m_AlertSound;
            m_AlertSource.volume = 2.0f;
            m_AlertSource.Play();
        }       
    }
    public void PlayPortal()
    {
        if (!m_PortalSource.isPlaying)
        {
            m_PortalSource.clip = m_PortalSound;
            m_PortalSource.volume = 1.0f;
            m_PortalSource.Play();
        }
    }

    public void StopAllSound()
    {
        foreach (var source in m_SFXSources)
        {
            if (source.isPlaying)
            {
                source.Stop();
            }
        }

        if (m_AlertSource.isPlaying)
            m_AlertSource.Stop();
    }
}
