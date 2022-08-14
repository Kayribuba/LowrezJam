using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayerScript : MonoBehaviour
{
    [System.Serializable]
    struct LevelSong
    {
        public bool isLooping;
        public int levelBuildIndexFrom;
        public int levelBuildIndexTo;
        public AudioClip songToPlay;
    }

    public static AudioPlayerScript instance;
    [Header("\"To\" included (ÖRN/from 0 to 0 sadece 0'da, 0 to 1 0 ve birde çalýþtýrýr).")]
    [SerializeField] LevelSong[] levelSongs;
    [SerializeField] AudioSource audioSource;

    int oldFrom = -1;
    int oldTo = -1;

    void Start()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            CheckSong(0);
        }

    }
    void OnLevelWasLoaded(int level)
    {
        CheckSong(level);
    }

    void CheckSong(int level)
    {
        foreach (LevelSong LS in levelSongs)
        {
            if (level >= LS.levelBuildIndexFrom && level <= LS.levelBuildIndexTo) // eðer istenen aralýktaysa
            {
                if (level >= oldFrom && level <= oldTo)
                { }
                else
                {
                    audioSource.Stop();
                    audioSource.loop = LS.isLooping;
                    audioSource.clip = LS.songToPlay;
                    audioSource.Play();
                    oldFrom = LS.levelBuildIndexFrom;
                    oldTo = LS.levelBuildIndexTo;
                    break;
                }
            }
        }
    }
}
