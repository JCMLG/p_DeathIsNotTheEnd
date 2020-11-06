using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

   // public AudioMixerGroup mixerGroup;

    public Sound[] sounds;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
           // s.source.outputAudioMixerGroup = mixerGroup;
        }
    }

     void Start()
    {
        Play ("Ambience1");    
    }

    public void Play (string name)
    {
        Sound s = Array.Find(sounds, Sound => Sound.name == name);
        s.source.Play();

        if (s == null)
        return;
        {
            Debug.Log("Sound:" + name + "not found!");
        }

        s.source.Play();
    }
}
