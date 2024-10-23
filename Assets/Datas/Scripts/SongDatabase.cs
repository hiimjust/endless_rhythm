using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SongDatabase", menuName = "SongDatabase")]
public class SongDatabase : ScriptableObject
{
    [SerializeField] public SongData[] songData;
}
