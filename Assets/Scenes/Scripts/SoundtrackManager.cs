using UnityEngine;
using UnityEngine.UI;

public class SoundtrackManager : MonoBehaviour
{
    // Audio rotates from a selection based on death.
    // Shld choose songs that hv a good beat and orbs collected will complement beat.

    AudioSource soundtrackSrc;
    int currentTrack = 0;

    public AudioClip[] soundtrackList;

    private void Start() {
        soundtrackSrc = this.GetComponent<AudioSource>();
        currentTrack = PlayerPrefs.GetInt("trackNum", currentTrack);
        Debug.Log(currentTrack);

        PlaySong();
    }

    private void PlaySong() {
        soundtrackSrc.clip = soundtrackList[currentTrack];
        soundtrackSrc.Play();

        currentTrack++;
        if (currentTrack > soundtrackList.Length - 1) {
            currentTrack = 0;
        } 
        
        PlayerPrefs.SetInt("trackNum", currentTrack);
    }

    public string GetSoundtrackName() {
        return "hello";
    }
 }
