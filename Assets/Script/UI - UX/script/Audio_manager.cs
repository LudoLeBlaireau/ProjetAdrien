using UnityEngine;
using UnityEngine.UI;

public class Audio_manager : MonoBehaviour
{
    [Header("------Audio Source------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;
    [Header("------Audio clip------")]

    public AudioClip background;
    public AudioClip death;
    public AudioClip jump;
    public AudioClip dash;
    public AudioClip fin;

    public Slider VolumeMainSlider;




    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();

    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);

    }

   

}