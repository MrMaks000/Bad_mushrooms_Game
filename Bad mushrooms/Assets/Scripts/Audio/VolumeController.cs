using UnityEngine;

public class VolumeController : MonoBehaviour
{
    private AudioSource audioSource;
    private static float musicVolume;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (musicVolume == 0f) musicVolume = 1;
    }

    void Update()
    {
        audioSource.volume = musicVolume;
    }

    public void SetVolume(float volume)
    {
        musicVolume = volume;
    }

    public float GetVolume()
    {
        return musicVolume;
    }
}
