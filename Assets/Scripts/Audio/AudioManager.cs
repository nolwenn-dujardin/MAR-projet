using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    
    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

    private void Awake() {
        if(Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }

    private void Start() {
        PlayMusic("WipeoutTheme");
    }

    public void PlayMusic(string name){
        Sound s = Array.Find(musicSounds, x => x.name == name);
        if(s == null){
            Debug.Log("Sound not found");
        }
        else {
            Debug.Log("Playing music "+name);
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    public void PlaySFX(string name){
        Sound s = Array.Find(sfxSounds, x => x.name == name);
        if(s == null){
            Debug.Log("Sound not found");
        }
        else {
            Debug.Log("Playing fx "+name);
            sfxSource.clip = s.clip;
            sfxSource.Play();
        }
    }

    public void ToggleMusic(){
        musicSource.mute = !musicSource.mute;
    }

    public void ToggleSFX(){
        sfxSource.mute = !sfxSource.mute;
    }

    public void MusicVolume(float volume){
        musicSource.volume = volume;
    }

    public void SFXVolume(float volume){
        sfxSource.volume = volume;
    }
}
