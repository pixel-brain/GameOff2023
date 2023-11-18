using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OutputPointScript : MonoBehaviour
{
    private List<List<Vector2>> _paths = new();
    private void Start()
    {
        FauxBeatManagerScript.Instance.AttachEnterEditEvent(ClearConnections);
    }

    [SerializeField]
    private GameObject note;
    //Testing
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            foreach (List<Vector2> path in _paths)
            {
                Instantiate(note, transform.position,Quaternion.identity).GetComponent<NoteScript>().Initialize(path);
            }
        }
    }

    public void CreatePath(Transform endPoint)
    {
        List<Vector2> newPath = new();

        Vector2 finalPosition = endPoint.position;
        Vector2 startPosition = transform.position;
        Vector2 currentPosition = startPosition;
        do
        {
            newPath.Add(currentPosition);
            currentPosition = GetNextPoint(startPosition, currentPosition, finalPosition);
        }while(Vector2.Distance(currentPosition, finalPosition) > 0.5f);
        newPath.Add(finalPosition);

        _paths.Add(newPath);
    }
/*
_pathInfo.OutputTransform.position.x, _pathInfo.OutputTransform.position.y
_pathInfo.OutputTransform.position.x, (float)Math.Round(_pathInfo.InputTransform.position.y - ((_pathInfo.InputTransform.position.y - _pathInfo.OutputTransform.position.y) / 2))
_pathInfo.InputTransform.position.x, (float)Math.Round(_pathInfo.InputTransform.position.y - ((_pathInfo.InputTransform.position.y - _pathInfo.OutputTransform.position.y) / 2))
_pathInfo.InputTransform.position.x, _pathInfo.InputTransform.position.y
*/
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

    public List<List<Vector2>> GetPaths()
    {
        return _paths;
    }

    private void ClearConnections()
    {
        _paths.Clear();
    }
}
