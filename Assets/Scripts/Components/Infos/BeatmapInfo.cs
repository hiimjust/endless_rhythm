using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BeatmapInfo
{
    public string name;
    public int maxBlock;
    public int BPM;
    public int offset;
    public NoteInfo[] notes;
}