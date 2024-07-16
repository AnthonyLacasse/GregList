using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPool
{
    private List<AudioSource> m_AudioSources = new List<AudioSource>();
    public AudioSource GetAvailableAudioSource()
    {
        foreach (AudioSource source in m_AudioSources)
        {
            if (!source.isPlaying)
            {
                return source;
            }
        }

        GameObject newObject = new GameObject();
        AudioSource newSource = newObject.AddComponent<AudioSource>();
        m_AudioSources.Add(newSource);

        return newSource;
    }
}
