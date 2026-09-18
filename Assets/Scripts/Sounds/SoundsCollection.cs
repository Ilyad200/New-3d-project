using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundsCollection", menuName = "Sounds/SoundsCollection")]
public class SoundsCollection : ScriptableObject
{
    public List<Audio> collection;

    public Dictionary<string, AudioClip> CollectionAsDictionary()
    {
        if (collection == null || collection.Count == 0) return null;

        return collection.ToDictionary(audio => audio.name, audio => audio.clip);
    }

}
[Serializable]
public class Audio
{
    public string name;
    public AudioClip clip;
}