using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputPointScript : MonoBehaviour
{
    private NoteScript _heldNote = null;

    public NoteScript GetNote()
    {
        return _heldNote;
    }

    public bool ReceiveNote(NoteScript note)
    {
        if (_heldNote != null)
        {
            return false;
        }
        _heldNote = note;
        return true;
    }

    public void ClearHeldNote()
    {
        _heldNote.Clear();
        _heldNote = null;
    }
}
