using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Note
{
    public Duration duration;
    public NoteType type;

    public static bool operator ==(Note lhs, Note rhs) => (lhs.duration == rhs.duration) && (lhs.type == rhs.type);
    public static bool operator !=(Note lhs, Note rhs) => !(lhs == rhs);
    public override bool Equals(object obj)
    {
        if (obj is Note other)
        {
            return this == other;
        }
        return false;
    }
}

public enum Duration
{
    Quarter = 1,
    Half = 2,
    ThreeQuarter = 3,
    Whole = 4
}

public enum NoteType
{
    Rest,
    One,
    Two,
    Three,
    Four,
    Five,
    Six,
    Seven,
    Eight,
    Nine,
    Ten,
    Eleven,
    Twelve
}