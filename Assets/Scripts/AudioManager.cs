using UnityEngine.Audio;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;

public class AudioManager : MonoBehaviour
{
    [SerializeField] Slider MusicVolumeSlider;
    [SerializeField] Slider EffectsVolumeSlider;

    private float previousMusicVolume;
    private float previousEffectsVolume;

    public Sound[] musicList;
    public Sound[] effectsList;


    // Start is called before the first frame update
    void Awake()
    {
        MusicVolumeSlider.value = Data.musicVolume;
        EffectsVolumeSlider.value = Data.effectsVolume;

        previousMusicVolume = MusicVolumeSlider.value;
        previousEffectsVolume = EffectsVolumeSlider.value;

        Initialise(MusicVolumeSlider, musicList);
        Initialise(EffectsVolumeSlider, effectsList);
    }

    private void Initialise(Slider slider, Sound[] sounds)
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.pitch = 1;
            s.source.loop = s.loop;

            s.source.volume = slider.value;
        }
    }

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (MusicVolumeSlider.value != previousMusicVolume)
        {
            UpdateMusicVolume();
        }
        if (EffectsVolumeSlider.value != previousEffectsVolume)
        {
            UpdateEffectsVolume();
        }
    }

    private void UpdateMusicVolume()
    {
        previousMusicVolume = MusicVolumeSlider.value;
        Data.musicVolume = previousMusicVolume;
        foreach(Sound s in musicList)
        {
            s.source.volume = previousMusicVolume;
        }
    }

    private void UpdateEffectsVolume()
    {
        previousEffectsVolume = EffectsVolumeSlider.value;
        Data.effectsVolume = previousEffectsVolume;
        foreach (Sound s in effectsList)
        {
            s.source.volume = previousEffectsVolume;
        }
    }

    public void ChangeMusic(string prevBG, string currBG)
    {
        StartCoroutine(UpdateBGMPortal(prevBG, currBG));
    }

    public IEnumerator UpdateBGMPortal(string prevBG, string currBG)
    {
        Sound prev = Array.Find(musicList, sound => sound.name == prevBG);
        Sound curr = Array.Find(musicList, sound => sound.name == currBG);

        for (int i = (int)(prev.source.volume * 100); i > 0; i--)
        {
            prev.source.volume -= 0.01f;
            yield return null;
        }

        prev.source.Stop();
        curr.source.volume = 0;
        curr.source.Play();

        for (int i = 0; i < MusicVolumeSlider.value; i++)
        {
            curr.source.volume += 0.01f;
        }

        curr.source.volume = MusicVolumeSlider.value;
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicList, sound => sound.name == name);
        if (s == null || s.source.isPlaying) return;
        s.source.Play();
    }

    public void PlayEffect(string name)
    {
        Sound s = Array.Find(effectsList, sound => sound.name == name);
        if (s == null || s.source.isPlaying) return;
        s.source.Play();
    }

    public void StopMusic(string name)
    {
        Sound s = Array.Find(musicList, sound => sound.name == name);
        if (s == null) return;
        s.source.Stop();
    }

    public void StopEffect(string name)
    {
        Sound s = Array.Find(effectsList, sound => sound.name == name);
        if (s == null) return;
        s.source.Stop();
    }
}
