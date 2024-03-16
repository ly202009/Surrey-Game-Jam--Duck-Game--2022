using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public AudioMixer sound;
    public AudioMixer music;
    public Slider soundSlider;
    public Slider musicSlider;

    void Start()
    {
        soundSlider.value = StaticVars.sound;
        musicSlider.value = StaticVars.music;
    }

    public void SetSound(float volume)
    {
        sound.SetFloat("Sounds", volume);
        StaticVars.sound = volume;
    }
    public void SetMusic(float volume)
    {
        music.SetFloat("Music", volume);
        StaticVars.music = volume;
    }
}
