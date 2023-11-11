using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//TO-DO Replace all GameObject instances with their eventual values

[System.Serializable]
public class BeatEvent : UnityEvent<NodeScript, List<GameObject>>{}
[System.Serializable]
public class NoteReceivedEvent : UnityEvent<NodeScript, GameObject, bool>{}
[System.Serializable]
public class NoteExpelledEvent : UnityEvent<NodeScript, List<GameObject>, Transform[]>{}
public class NodeScript : MonoBehaviour
{
    //The system works based on Events, so we can define different behaviours to different actions when creating the nodes.
    [Header("Events")]
    [Space]
    [SerializeField]
    //Defines what happens when a beat is played. Passes a reference to all held Notes.
    private BeatEvent _beatEvent = new();
    [SerializeField]
    //Defines what happens when a note is received. Passes a reference to said note and if it was accepted or not.
    private NoteReceivedEvent _noteReceivedEvent = new();
    [SerializeField]
    //Defines what happens when a note is expelled.
    private NoteExpelledEvent _noteExpelledEvent = new();
    [Space]
    [Header("Regular Data")]
    [Space]
    [SerializeField]
    private int _maxHeldNotes = 1;
    [SerializeField]
    //TO-DO Replace transform with the proper reference.
    private Transform[] _outputs;
    private readonly List<GameObject> _heldNotes = new();

    private void Start()
    {
        //TO-DO Replace the Faux manager with the real one.
        FauxBeatManagerScript.Instance.AttachBeatEvent(Beat);
    }

    //The Beat function is attached to the Beat Manager on start and is called every Beat
    private void Beat()
    {
        _beatEvent.Invoke(this, _heldNotes);
    }

    public void ReceiveNote(GameObject note)
    {
        bool overflows = _heldNotes.Count >= _maxHeldNotes;
        _noteReceivedEvent.Invoke(this, note, overflows);
        if(overflows)
        {
            Debug.LogWarning("This node received too many notes.");
            return;
        }
        _heldNotes.Add(note);
    }
    public void OutputNote(List<GameObject> notes)
    {
        _noteExpelledEvent.Invoke(this, notes, _outputs);
    }
}
