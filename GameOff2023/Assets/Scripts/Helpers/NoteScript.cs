using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteScript : MonoBehaviour
{
    private List<Vector2> _path = new();

    private int _step = 0;

    public void Initialize(List<Vector2> path)
    {
        _path = path;
        _step = 0;

        FauxBeatManagerScript.Instance.AttachBeatEvent(Move);
        FauxBeatManagerScript.Instance.AttachEnterEditEvent(Clear);
    }

    private void Move()
    {
        if(_step >= _path.Count || _path.Count <= 0) 
        {
            Clear();
            return;
        }
        transform.position = _path[_step];
        _step++;
    }

    private void Clear()
    {
        FauxBeatManagerScript.Instance.RemoveBeatEvent(Move);
        FauxBeatManagerScript.Instance.RemoveEnterEditEvent(Clear);
        Destroy(gameObject);
    }
}
