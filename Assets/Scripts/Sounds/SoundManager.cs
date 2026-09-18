using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [SerializeField] private SoundsCollection soundsCollection;

    [SerializeField] private AudioSource soundsSource;

    private Dictionary<string, AudioClip> soundsDictionary;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            soundsDictionary = soundsCollection.CollectionAsDictionary();

        }
        else Destroy(gameObject);
    }
    public void PlaySound(string soundName)
    {
        if (soundsDictionary.TryGetValue(soundName, out var clip)) soundsSource.PlayOneShot(clip);
    }
}
