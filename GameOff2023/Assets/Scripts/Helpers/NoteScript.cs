using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteScript : MonoBehaviour
{
    private List<Vector2> _path = new();

    private int _step = 1;

    private Vector3 _targetPos;
    private Vector3 _startPos;

    private InputPointScript _targetInput;
    private GameObject _sender;

    [SerializeField]
    private float _timeToDestination = 0.5f;

    [SerializeField]
    private LayerMask _notesMask;

    private float _t;

    private void Update()
    {
        _t += Time.deltaTime/_timeToDestination;
        transform.position = Vector2.Lerp(_startPos, _targetPos, _t);
    }

    public void Initialize(PathTargetInfo info, GameObject sender)
    {
        _sender = sender;
        _path = info._path;
        _step = 1;
        _startPos = info._path[0];
        _targetPos = info._path[0];
        _targetInput = info._target;

        FauxBeatManagerScript.Instance.AttachBeatEvent(Move);
        FauxBeatManagerScript.Instance.AttachEnterEditEvent(Clear);
    }

    private void Move()
    {

        if(_step >= _path.Count || _path.Count <= 0) 
        {
            _targetInput.ReceiveNote(this);
            return;
        }

        Collider2D[] results = Physics2D.OverlapCircleAll(_path[_step], 0.2f, _notesMask);
        foreach (Collider2D result in results)
        {
            if(result.GetComponent<NoteScript>().WillBlock(_sender)) return;
        }

        _startPos = _targetPos;
        _targetPos = _path[_step];
        _t = 0;
        _step++;
    }

    public void Clear()
    {
        FauxBeatManagerScript.Instance.RemoveBeatEvent(Move);
        FauxBeatManagerScript.Instance.RemoveEnterEditEvent(Clear);
        Destroy(gameObject);
    }

    public bool WillBlock(GameObject sender)
    {
        return sender == _sender;
    }
}
