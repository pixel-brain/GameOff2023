using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FauxBeatManagerScript : MonoBehaviour
{
    private readonly UnityEvent BeatEvent = new();

    public static FauxBeatManagerScript Instance { get; private set; }
    private void Awake() 
    { 
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        InvokeRepeating(nameof(Beat), 0f, 1f);        
    }

    public void AttachBeatEvent(UnityAction actionToAdd)
    {
        BeatEvent.AddListener(actionToAdd);
    }
    public void RemoveBeatEvent(UnityAction actionToRemove)
    {
        BeatEvent.RemoveListener(actionToRemove);
    }

    private void Beat()
    {
        BeatEvent?.Invoke();
    }
}
