using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PathInfo
{
    private Transform _inputTransform;
    private Transform _outputTransform;

    public PathInfo(Transform inputTransform, Transform outputTransform)
    {
        _inputTransform = inputTransform;
        _outputTransform = outputTransform;
    }
    public Transform OutputTransform { readonly get => _outputTransform; set => _outputTransform = value; }
    public Transform InputTransform { readonly get => _inputTransform; set => _inputTransform = value; }
}
public class ConnectionsManagerScript : MonoBehaviour
{
	[Header("Required Components")]
	[Space]

	[SerializeField]
	private LayerMask _inputOutputLayer;

	private LineRenderer _lineRenderer;

	
	[Header("Visualization")]
	[Space]
	[SerializeField]
	private List<PathInfo> _paths;

	private Transform _startPoint = null; 

	private void Awake()
	{
		_lineRenderer = transform.GetComponent<LineRenderer>();
	}

	private void Start()
	{
		FauxBeatManagerScript.Instance.AttachEnterPlayEvent(CreatePaths);
	}

	private void CreatePaths()
	{
		foreach (PathInfo path in _paths)
		{
			path.OutputTransform.GetComponent<OutputPointScript>().CreatePath(path.InputTransform);
		}
	}

	private void Update()
	{
		if(!_lineRenderer)
        {
            Debug.LogError("A Line Renderer is required for this object to function: " + gameObject.name);
            return;
        }
		if(FauxBeatManagerScript.Instance.CurrentGameState != GameState.Edit)
		{
			return;
		}
		if(_startPoint)
		{
			Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			_lineRenderer.positionCount = 4;
			_lineRenderer.SetPosition(0, new Vector3(_startPoint.position.x, _startPoint.position.y, 0));
			_lineRenderer.SetPosition(1, new Vector3(_startPoint.position.x, (float)Math.Round(mousePos.y - ((mousePos.y - _startPoint.position.y) / 2)), 0));
			_lineRenderer.SetPosition(2, new Vector3(mousePos.x, (float)Math.Round(mousePos.y - ((mousePos.y - _startPoint.position.y) / 2)), 0));
			_lineRenderer.SetPosition(3, new Vector3(mousePos.x, mousePos.y, 0));
		}
		else
		{
			_lineRenderer.positionCount = 0;
		}
		if (Input.GetKeyDown(KeyCode.Mouse0))
        {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit2D hitInfo = Physics2D.Raycast(ray.origin, ray.direction,Mathf.Infinity,_inputOutputLayer);
			if (hitInfo)
			{
				Transform objectSelected = hitInfo.collider.transform;
				_startPoint = objectSelected;
			}
		}
		if (Input.GetKeyUp(KeyCode.Mouse0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit2D hitInfo = Physics2D.Raycast(ray.origin, ray.direction,Mathf.Infinity,_inputOutputLayer);
			if (hitInfo)
			{
				Transform objectSelected = hitInfo.collider.transform;
				if(_startPoint != null && !objectSelected.CompareTag(_startPoint.tag))
				{
					CreatePath(objectSelected);
				}
			}
			_startPoint = null;
		}
    }

	private void CreatePath(Transform endPoint)
	{
		PathInfo pathInfo = new(null, null);
		switch(_startPoint.tag)
		{
			case "Input":
				pathInfo.InputTransform = _startPoint;
				pathInfo.OutputTransform = endPoint;
				break;
			case "Output":
				pathInfo.InputTransform = endPoint;
				pathInfo.OutputTransform = _startPoint;
				break;
			default:
				Debug.LogWarning("Tried to create a path with something invalid");
				return;
		}
		int index = _paths.FindIndex(path => path.InputTransform == pathInfo.InputTransform);
		if(index >= 0)
		{
			_paths.RemoveAt(index);
		}
		PathDrawerScript drawerScript = pathInfo.InputTransform.GetComponent<PathDrawerScript>();
		drawerScript.AssignPath(pathInfo);
		_paths.Add(pathInfo);
	}
}
