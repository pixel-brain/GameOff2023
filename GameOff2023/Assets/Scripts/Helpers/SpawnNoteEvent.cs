using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNoteEvent : MonoBehaviour
{
    [SerializeField]
    private GameObject note;
    [SerializeField]
    private int _delay = 2;

    private int _timer = 0;

    public void CreateNote(NodeScript node)
    {
        _timer++;
        if(_timer < _delay) return;
        Transform[] outputs = node.GetOutputs();
        foreach (Transform output in outputs)
        {
            output.GetComponent<OutputPointScript>().SpawnNote(note);
        }
        _timer = 0;
    }
    public void EditNote(NodeScript factory)
    {
        Debug.Log("Note value changed by: " + ((EditableNodeScript)factory).GetSliderValue());
    }
}
