using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    

    // Start is called before the first frame update
    void Start()
    {
        // Don't Destroy on load for manager. Not sure if needed.
        if (instance != null && instance != this)
        {
            Destroy(this);
            return;
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this);
    }

    public static AudioManager GetInstance()
    {
        return instance;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void PlaySoundOnce( AudioSource aSource)
    {
        //Plays the sound clip once. Not Spammable
        if (aSource.isPlaying == false)
        aSource.PlayOneShot(aSource.clip);
        
    }
    public static void PlaySoundLoop (AudioSource aSource)
    {
        // Plays sound clip, and loops it
        aSource.PlayOneShot(aSource.clip);
        aSource.loop = true;
    }
    public static void StopSoundLoop(AudioSource aSource)
    {
        // Stops sound clip, and shuts off the looping, too
        aSource.loop = false;
        aSource.Stop();
    }
   public static void StopSound(AudioSource aSource)
    {
        //stops sound clip
        aSource.Stop();
    }
   public static void PlaySoundSpam(AudioSource aSource)
    {
        //plays sound clip once. Spammable.
        aSource.PlayOneShot(aSource.clip);
    }
}
