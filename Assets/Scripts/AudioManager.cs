using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public GameManager gameManager;
    public AudioSource sfxAudio;
    public AudioSource musicAudio;
    public AudioClip[] menuMusic;
    public AudioClip[] levelMusic;
    public AudioClip[] footStepClips;
    public AudioClip[] spawnClips;
    public AudioClip[] ghostDieClips;

    // Start is called before the first frame update
    void Start()
    {
        if (!gameManager)
            gameManager = GameManager.GetGameManager();
        transform.parent = gameManager.player.transform;
        transform.localPosition = Vector3.zero;
        
    }

    public void PlayMenuMusic()
    {
        if (menuMusic.Length > 0)
        {
            musicAudio.Stop();
            musicAudio.loop = true;
            musicAudio.PlayOneShot(GetRandomClip(menuMusic));
        }
    }

    public void PlaySpawnSound()
    {
        Debug.Log("Playing Spawn sound");

        if (spawnClips.Length > 0)
        {
            sfxAudio.PlayOneShot(GetRandomClip(spawnClips));
        }
    }

    public void PlayLevelMusic()
    {
        Debug.Log("Playing Level Music");
        if (levelMusic.Length > 0)
        {
            musicAudio.Stop();
            musicAudio.loop = true;
            musicAudio.PlayOneShot(GetRandomClip(menuMusic));
        }
    }

    public void PlayGhostDieSound()
    {
        Debug.Log("Playing Ghost Die SOund");
        if (ghostDieClips.Length > 0)
        {
            sfxAudio.PlayOneShot(GetRandomClip(ghostDieClips));
        }
    }


    // Return random clip from the array passed
    public static AudioClip GetRandomClip(AudioClip[] clips)
    {
        if (clips.Length > 0) {
            int r = Mathf.FloorToInt(Random.Range(0, clips.Length));
            // Debug.Log("Returning clip # " + r);
            return clips[r];
        } else
        {
            return null;
        }
    }


}
