using System.Collections;
using UnityEngine;

public class RadioEffect : MonoBehaviour
{
    public AudioSource levelMusic;
    public AudioSource areaSound;
    public float maxVolumeDistance = 10f; // Adjust the maximum distance where the volume is at max
    public float minVolumeDistance = 2f;  // Adjust the minimum distance where the volume is at min
    public float volumeChangeSpeed = 0.5f; // Adjust the speed of volume change

    private bool areaSoundPlaying = false;
    private float originalMusicVolume;

    void Start()
    {
        originalMusicVolume = levelMusic.volume;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !areaSoundPlaying)
        {
            areaSound.Play();
            areaSoundPlaying = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            areaSoundPlaying = false;
            StartCoroutine(MuteAreaSoundSmoothly());
            StartCoroutine(RestoreMusicVolumeSmoothly());
        }
    }

    void Update()
    {
        if (areaSoundPlaying)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, levelMusic.transform.position);
            float targetVolume = 1f;

            if (distanceToPlayer > maxVolumeDistance)
            {
                targetVolume = 0f;
            }
            else if (distanceToPlayer < minVolumeDistance)
            {
                targetVolume = 1f;
            }
            else
            {
                targetVolume = 1f - Mathf.InverseLerp(minVolumeDistance, maxVolumeDistance, distanceToPlayer);
            }

            StartCoroutine(ChangeVolumeSmoothly(levelMusic, targetVolume));
        }
    }

    IEnumerator ChangeVolumeSmoothly(AudioSource audioSource, float targetVolume)
    {
        float currentVolume = audioSource.volume;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * volumeChangeSpeed;
            audioSource.volume = Mathf.Lerp(currentVolume, targetVolume, t);
            yield return null;
        }
    }

    IEnumerator MuteAreaSoundSmoothly()
    {
        float currentVolume = areaSound.volume;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * volumeChangeSpeed;
            areaSound.volume = Mathf.Lerp(currentVolume, 0f, t);
            yield return null;
        }
    }

    IEnumerator RestoreMusicVolumeSmoothly()
    {
        float currentVolume = levelMusic.volume;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * volumeChangeSpeed;
            levelMusic.volume = Mathf.Lerp(currentVolume, originalMusicVolume, t);
            yield return null;
        }
    }
}
