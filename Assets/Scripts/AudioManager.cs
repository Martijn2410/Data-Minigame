using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFX;

    [Header("Audio Clip")]
    public AudioClip backgroundMusic;
    public AudioClip slashSFX;
    public AudioClip explosionSFX;

    private void Awake()
    {
        musicSource.clip = backgroundMusic;  // Assign the clip before playing
    }

    private void Start()
    {
        musicSource.Play();  // Now it plays with the correct clip assigned
    }

    public void PlaySFX(AudioClip clip)
    {
        SFX.PlayOneShot(clip);
    }
}
