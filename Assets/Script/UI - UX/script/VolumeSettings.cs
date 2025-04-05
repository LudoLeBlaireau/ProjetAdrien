using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    public Slider volumeSlider;
    public AudioSource musicSource;

    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider MainSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider SFXSlider;

    public void SetMainVolume()
    {
        float volume = MainSlider.value;
        myMixer.SetFloat("Main",Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("MainVolume", volume);


    }
    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        myMixer.SetFloat("music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MusicVolume", volume);

    }
    public void SetSfxVolume()
    {
        float volume = SFXSlider.value;
        myMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVolume", volume);

    }


    private void Start()
    {
        // Charger les valeurs sauvegardées
        MainSlider.value = PlayerPrefs.GetFloat("MainVolume", 1f);
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
        SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1f);

        ApplyVolumes();
    }

    private void ApplyVolumes()
    {
        SetMainVolume();
        SetMusicVolume();
        SetSfxVolume();
    }

}
