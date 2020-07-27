using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
    private static MusicPlayer _instance;

    private AudioSource _audio;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);
        
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        _audio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!_audio.isPlaying && SceneManager.GetActiveScene().buildIndex == 1)
            _audio.Play();
        
        if (_audio.isPlaying && SceneManager.GetActiveScene().buildIndex != 1)
            _audio.Stop();
    }
}
