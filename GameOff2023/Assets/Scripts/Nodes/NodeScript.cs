using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//TO-DO Replace all GameObject instances with their eventual values

[System.Serializable]
public class BeatEvent : UnityEvent<NodeScript>{}
public class NodeScript : MonoBehaviour
{
    //The system works based on Events, so we can define different behaviours to different actions when creating the nodes.
    [Header("Events")]
    [Space]
    [SerializeField]
    //Defines what happens when a beat is played. Passes a reference to all held Notes.
    private BeatEvent _beatEvent = new();

    [Space]
    [Header("Regular Data")]
    [Space]
    [SerializeField]
    //TO-DO Replace transform with the proper reference.
    private Transform[] _outputs;
    [SerializeField]
    private Transform[] _inputs;

    private void Start()
    {
        //TO-DO Replace the Faux manager with the real one.
        FauxBeatManagerScript.Instance.AttachBeatEvent(Beat);
    }

    //The Beat function is attached to the Beat Manager on start and is called every Beat
    private void Beat()
    {
        _beatEvent.Invoke(this);
    }

    public Transform[] GetOutputs()
    {
        return _outputs;
    }
    public Transform[] GetInputs()
    {
        return _inputs;
    }
}
