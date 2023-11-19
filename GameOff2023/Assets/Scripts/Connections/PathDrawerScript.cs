using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathDrawerScript : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    
    private PathInfo _pathInfo;

    private bool assigned = false;

	private void Awake()
	{
		_lineRenderer = transform.GetComponent<LineRenderer>();
        if(!_lineRenderer)
        {
            Debug.LogError("A Line Renderer is required for this object to function: " + gameObject.name);
            return;
        }
	}

    private void Update()
    {
        if(!_lineRenderer)
        {
            return;
        }
        if(!assigned) return;
        _lineRenderer.positionCount = 4;
		_lineRenderer.SetPosition(0, new Vector3(_pathInfo.OutputTransform.position.x, _pathInfo.OutputTransform.position.y, 0));
		_lineRenderer.SetPosition(1, new Vector3(_pathInfo.OutputTransform.position.x, (float)Math.Round(_pathInfo.InputTransform.position.y - ((_pathInfo.InputTransform.position.y - _pathInfo.OutputTransform.position.y) / 2)), 0));
		_lineRenderer.SetPosition(2, new Vector3(_pathInfo.InputTransform.position.x, (float)Math.Round(_pathInfo.InputTransform.position.y - ((_pathInfo.InputTransform.position.y - _pathInfo.OutputTransform.position.y) / 2)), 0));
		_lineRenderer.SetPosition(3, new Vector3(_pathInfo.InputTransform.position.x, _pathInfo.InputTransform.position.y, 0));
    }

    public void AssignPath(PathInfo pathInfo)
    {
        _pathInfo = pathInfo;
        assigned = true;
    }

    public void RemovePath()
    {
        assigned = false;
    }
}
