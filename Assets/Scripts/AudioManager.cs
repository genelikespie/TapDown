using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour {

    Dictionary<string, AudioSource> audioSources;

    private static AudioManager instance;
    private static Object instance_lock = new Object();

    void Awake ()
    {
        audioSources = new Dictionary<string, AudioSource>();
        if (audioSources != null)
            Debug.Log("created new audiosources dict");
        foreach (AudioSource a in GetComponentsInChildren<AudioSource>())
        {
            audioSources.Add(a.name, a);
        }
    }

    public static AudioManager Instance()
    {
        if (instance != null)
            return instance;
        lock (instance_lock)
        {
            instance = (AudioManager)FindObjectOfType(typeof(AudioManager));
            if (FindObjectsOfType(typeof(AudioManager)).Length > 1)
            {
                Debug.LogError("There can only be one instance!");
                return instance;
            }
            if (instance != null)
                return instance;
            Debug.LogError("Could not find a instance!");
            return null;
        }
    }

    public AudioSource GetAudioSource(string name)
    {
        AudioSource a = null;
        if (audioSources == null)
            Debug.LogError("Audio sources null");

        if (!audioSources.TryGetValue(name, out a))
        {
            Debug.LogError("Could not find audiosource: " + name);
        }
        return a;
    }
    
}
