using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNoteEvent : MonoBehaviour
{

    public void CreateNote()
    {
        Debug.Log("New Note");
    }
    public void EditNote(NodeScript factory, List<GameObject> notes)
    {
        Debug.Log("Note value changed by: " + ((EditableNodeScript)factory).GetSliderValue());
    }
}
