using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;
    public static float masterVolume = 1.7f;

    [SerializeField]
    float ambienceVolume = 0.3f;
    [SerializeField]
    AudioSource ambience = null;


    // Start is called before the first frame update
    void Start()
    {
        // Don't Destroy on load for manager. Not sure if needed.
        if (_instance)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
        //PlaySoundLoop(ambience, ambienceVolume);
    }

    public static AudioManager instance
    {
        get { return _instance; }
        set { instance = value; }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void PlaySoundOnce(AudioSource aSource)
    {
        aSource.volume = masterVolume;
        //Plays the sound clip once. Not Spammable
        if (aSource.isPlaying == false)
        aSource.PlayOneShot(aSource.clip);

    }
    public void PlaySoundLoop (AudioSource aSource, float volume)
    {
        aSource.volume = volume;
        // Plays sound clip, and loops it
        aSource.loop = true;
        aSource.PlayOneShot(aSource.clip);
        
    }
    public void StopSoundLoop(AudioSource aSource)
    {
        aSource.volume = masterVolume;
        // Stops sound clip, and shuts off the looping, too
        aSource.loop = false;
        aSource.Stop();
    }
   public void StopSound(AudioSource aSource)
    {

        //stops sound clip
        aSource.Stop();
    }
   public void PlaySoundSpam(AudioSource aSource)
    {
        //plays sound clip once. Spammable.
        aSource.volume = masterVolume;
        aSource.PlayOneShot(aSource.clip);
    }

    public void PlayAudioAtPoint(AudioClip aClip, Transform transform)
    {
        //Makes an audio source to play a sound clip, deletes itself
        AudioSource.PlayClipAtPoint(aClip, transform.position, masterVolume);
    }
}
