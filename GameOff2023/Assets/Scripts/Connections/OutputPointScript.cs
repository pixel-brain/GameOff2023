using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public struct PathTargetInfo
{
    public List<Vector2> _path;
    public InputPointScript _target;
}

public class OutputPointScript : MonoBehaviour
{
    private List<PathTargetInfo> _paths = new();

    [SerializeField]
    private LayerMask _notesMask;

    private int _currentPath = 0;

    private void Start()
    {
        FauxBeatManagerScript.Instance.AttachEnterEditEvent(ClearConnections);
    }

    //Testing
    public void SpawnNote(GameObject note)
    {
        Collider2D[] results = Physics2D.OverlapCircleAll(transform.position, 0.2f, _notesMask);
        foreach (Collider2D result in results)
        {
            if(result.GetComponent<NoteScript>().WillBlock(gameObject)) return;
        }

        Instantiate(note, transform.position,Quaternion.identity).GetComponent<NoteScript>().Initialize(_paths[_currentPath], gameObject);
        _currentPath = _currentPath + 1 < _paths.Count ? _currentPath + 1 : 0;
    }

    public void CreatePath(Transform endPoint)
    {
        PathTargetInfo newPath;

        newPath._path = new();

        Vector2 finalPosition = endPoint.position;
        Vector2 startPosition = transform.position;
        Vector2 currentPosition = startPosition;
        do
        {
            newPath._path.Add(currentPosition);
            currentPosition = GetNextPoint(startPosition, currentPosition, finalPosition);
        }while(Vector2.Distance(currentPosition, finalPosition) > 0.5f);
        newPath._path.Add(finalPosition);
        newPath._target = endPoint.GetComponent<InputPointScript>();
        _paths.Add(newPath);
    }

    private Vector2 GetNextPoint(Vector2 start, Vector2 current, Vector2 target)
    {
        if(start.y < target.y)
        {
            if(current.y >= (float)Math.Round((target.y + start.y) / 2) && current.x != target.x)
            {
                if(start.x < target.x)
                {
                    return new Vector2(current.x + 1, current.y);
                }
                else
                {
                    return new Vector2(current.x - 1, current.y);
                }
            }
            else
            {
                return new Vector2(current.x, current.y + 1);
            }
        }
        else
        {
            if(current.y <= (float)Math.Round((target.y + start.y) / 2) && current.x != target.x)
            {
                if(start.x < target.x)
                {
                    return new Vector2(current.x + 1, current.y);
                }
                else
                {
                    return new Vector2(current.x - 1, current.y);
                }
            }
            else
            {
                return new Vector2(current.x, current.y - 1);
            }
        }
    }

    private void ClearConnections()
    {
        _paths.Clear();
    }
}
