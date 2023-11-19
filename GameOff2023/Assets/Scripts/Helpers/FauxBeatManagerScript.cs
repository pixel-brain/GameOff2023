using UnityEngine;
using UnityEngine.Events;

public enum GameState{
    Play,
    Edit,
    Pause
}

public class FauxBeatManagerScript : MonoBehaviour
{
    private readonly UnityEvent BeatEvent = new();
    private readonly UnityEvent EnterEditEvent = new();
    private readonly UnityEvent EnterPlayEvent = new();

    public static FauxBeatManagerScript Instance { get; private set; }

    [SerializeField]
    private GameState _currentGameState = GameState.Edit;
    public  GameState CurrentGameState { get => _currentGameState; private set => _currentGameState = value; }

    //Singleton Pattern
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
        _currentGameState = GameState.Edit;
    }



    //Testing stuff
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetModePlay();
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetModeEdit();
        }
    }



    //Game State Change Functions
    public void SetModePause()
    {
        _currentGameState = GameState.Pause;
        CancelInvoke(nameof(Beat));
    }

    public void SetModeEdit()
    {
        _currentGameState = GameState.Edit;
        CancelInvoke(nameof(Beat));
        EnterEditEvent?.Invoke();
    }

    public void SetModePlay()
    {
        _currentGameState = GameState.Play;
        InvokeRepeating(nameof(Beat), 1f, 1f);     
        EnterPlayEvent?.Invoke();
    }





    //Function Called Every Beat
    private void Beat()
    {
        if (_currentGameState != GameState.Play) return;
        BeatEvent?.Invoke();
    }






    //Beat Event Assigns
    public void AttachBeatEvent(UnityAction actionToAdd)
    {
        BeatEvent.AddListener(actionToAdd);
    }
    public void RemoveBeatEvent(UnityAction actionToRemove)
    {
        BeatEvent.RemoveListener(actionToRemove);
    }


    //Enter Edit Event Assigns
    public void AttachEnterEditEvent(UnityAction actionToAdd)
    {
        EnterEditEvent.AddListener(actionToAdd);
    }
    public void RemoveEnterEditEvent(UnityAction actionToRemove)
    {
        EnterEditEvent.RemoveListener(actionToRemove);
    }


    //Enter Play Event Assigns
    public void AttachEnterPlayEvent(UnityAction actionToAdd)
    {
        EnterPlayEvent.AddListener(actionToAdd);
    }
    public void RemoveEnterPlayEvent(UnityAction actionToRemove)
    {
        EnterPlayEvent.RemoveListener(actionToRemove);
    }
}
