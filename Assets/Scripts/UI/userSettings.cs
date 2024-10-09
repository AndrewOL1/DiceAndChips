using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using Unity.VisualScripting;
using TMPro;

public class userSettings : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;
    [SerializeField] Slider ambientSlider;
    GameObject sliders;
    GameObject highScore;
    int hiscore;
    // Start is called before the first frame update
    public const string MIXER_MUSIC = "MusicVolume";
    public const string MIXER_SFX = "SFXVolume";
    public const string MIXER_AMBIENT = "AmbientVolume";
    public const string HIGHSCORE = "HighScore";
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
        highScore = GameObject.Find("HighScore");
        sliders.SetActive(false);
        highScore.SetActive(false);
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
        PlayerPrefs.SetFloat(MIXER_SFX, volume);
    }
    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        audioMixer.SetFloat("Music", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat(MIXER_MUSIC, volume);
    }
    public void SetAmbientVolume()
    {
        float volume = ambientSlider.value;
        audioMixer.SetFloat("Ambient", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat(MIXER_AMBIENT, volume);
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
    public void toggleHighScore()
    {
        if (sliders != null)
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

    private void loadHignScore()
    {
        hiscore= PlayerPrefs.GetInt(HIGHSCORE);
    }
    public void newScore(int i)
    {
        setHighScore(i);
    }
    private void setHighScore(int i)
    {
        bool t;
        if(i> hiscore)
        {
            hiscore = i;
            t = true;
            displayHighScore(t,i);
        }
        else
        {
            t = false;
            displayHighScore(t, i);
        }
    }
    private void displayHighScore(bool n,int i)
    {
        if (n)
        {
            highScore.transform.GetChild(0).GetComponent<TextMeshPro>().text=( "New HighScore : "+ hiscore);
        }
        else
        {
            highScore.transform.GetChild(0).GetComponent<TextMeshPro>().text = ("HighScore : " + hiscore+"\n" +"Your Score : " +i);
        }
    }

}
