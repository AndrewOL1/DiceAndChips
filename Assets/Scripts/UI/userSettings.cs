using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class userSettings : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;
    [SerializeField] Slider ambientSlider;
    GameObject sliders;
    // Start is called before the first frame update
    public const string MIXER_MUSIC = "MusicVolume";
    public const string MIXER_SFX = "SFXVolume";
    public const string MIXER_AMBIENT = "AmbientVolume";
    void Awake()
    {
        if (PlayerPrefs.HasKey(MIXER_MUSIC))
        {
            LoadMusicVolume();
            LoadSFXVolume();
            LoadAmbientVolume();
        }
        else
        {
            SetMusicVolume();
            SetSFXVolume();
            SetAmbientVolume();
        }
        sliders = GameObject.Find("Sliders");
        sliders.SetActive(false);
    }
    private void Start()
    {

    }
    void LoadMusicVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat(MIXER_MUSIC);
        SetMusicVolume();
    }
    void LoadSFXVolume()
    {
        sfxSlider.value = PlayerPrefs.GetFloat(MIXER_SFX);
        SetSFXVolume();
    }
    void LoadAmbientVolume()
    {
        ambientSlider.value = PlayerPrefs.GetFloat(MIXER_AMBIENT);
        SetAmbientVolume();
    }
    public void SetSFXVolume()
    {
        float volume = sfxSlider.value;
        audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
    }
    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        audioMixer.SetFloat("Music", Mathf.Log10(volume)*20);
    }
    public void SetAmbientVolume()
    {
        float volume = ambientSlider.value;
        audioMixer.SetFloat("Ambient", Mathf.Log10(volume) * 20);
    }

    public void toggleSettings()
    {
        if(sliders != null) 
        {
            if (sliders.activeSelf == true)
            {
                sliders.SetActive(false);
                Time.timeScale = 1.0f;
            }
            else
            {
                sliders.SetActive(true);
                Time.timeScale = 0f;
            }
        }
    }
}
