using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip carSound;
    public AudioClip helicopterSound;

    private AudioSource carsoundSource;
    public AudioSource helicopterSoundSource;
    public AudioSource carCrashSound;

    #region Singleton
    public static SoundManager instance;
    #endregion

    private void Awake()
    {
        instance = this;
        carsoundSource = GetComponent<AudioSource>();

    }
   

    // Start is called before the first frame update
    public void PlayCarSound()
    {
        carsoundSource.PlayOneShot(carSound);
    }
    public void StopCarSound()
    {
        carsoundSource.Stop();
    }
    public void PlayHelicopterSound()
    {
        helicopterSoundSource.PlayOneShot(helicopterSound);
    }
    public void StopHelicopterSound()
    {
        helicopterSoundSource.Stop();
    }

    public void PlayCarCollisionSound()
    {
        carCrashSound.Play();
    }
    public void StopCollisonSound()
    {
        carCrashSound.Stop();
    }
}
