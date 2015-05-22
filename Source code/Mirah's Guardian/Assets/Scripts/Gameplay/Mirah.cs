using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Mirah : Character 
{
	private enum PathTagNames
	{
		WALKING_TO_PORTAL,
		WALKING_TO_POINT,
		RUNNING_TO_POINT
	}

	public event Action onReachPortal;

	private Rigidbody _rigidbody;
	private Transform _transform;
	private Vector2 _endPortalPosition;
	private List<AStar.Node> _pathableNodes;
	private float _originalSpeed;
	private bool _pauseMovement = false;
	private List<Vector2> _pathToFollow;
	private bool _isGoingToPortal = false;
	private int _currentNode = 0;
	private Coroutine _currentProcessingPath;

	void Start()
	{
		_originalSpeed = this.speed;
		_rigidbody = this.GetComponent<Rigidbody>();
		_transform = this.GetComponent<Transform>();
	}

	void Update()
	{
		FollowPathUpdate ();
	}

	private void FollowPathUpdate()
	{
		if (_pathToFollow != null && _pauseMovement == false) 
		{
			var __distanceToCurrentNode = Vector3.Distance(_transform.position, new Vector3(_pathToFollow[_currentNode].x, 0 , _pathToFollow[_currentNode].y));
			if(__distanceToCurrentNode < 0.1f)
			{
				if((_currentNode +1) >= _pathToFollow.Count)
				{
					_pauseMovement = true;
					_currentNode = 0;
					_pathToFollow = null;

					if(_isGoingToPortal)
						onReachPortal();
					else
						WalkToTheEndPortal();

					return;
				}

				_currentNode ++;
			}

			Vector3 __pointoToMoveTo = new Vector3(_pathToFollow[_currentNode].x, 0 , _pathToFollow[_currentNode].y);
			Vector3 __positionToBe = _transform.position + Vector3.Normalize( __pointoToMoveTo - _transform.position);
			_rigidbody.MovePosition(Vector3.Lerp(_transform.position, __positionToBe, this.speed * Time.deltaTime));
			//_transform.LookAt(__positionToBe);
		}
	}

	public void InformEndPortalPosition(Vector2 p_position)
	{
		_endPortalPosition = p_position;
	}

	public void InformMapNodes(List<AStar.Node> p_nodes)
	{
		_pathableNodes = p_nodes;
	}

	private void GoToPosition(Vector2 p_position, string p_pathTagName)
	{
		_isGoingToPortal = false;
		_pauseMovement = false;

		if (_pathToFollow != null)
			_pathToFollow = null;

		if (_currentProcessingPath != null)
			StopCoroutine (_currentProcessingPath);

		AStar __newPath = new AStar (this.Get2DPosition(), p_position, _pathableNodes);
		__newPath.onPathProcessed += (p_pathToFollow) => 
		{
			_currentNode = 0;
			_pathToFollow = p_pathToFollow;
			_currentProcessingPath = null;
		};

		_currentProcessingPath = StartCoroutine (__newPath.ProcessPath ());
	}

	public void WalkToTheEndPortal()
	{
		if(_originalSpeed != 0)
			this.speed = _originalSpeed;

		GoToPosition (_endPortalPosition, PathTagNames.WALKING_TO_PORTAL.ToString());
		_isGoingToPortal = true;
	}

	public void WalkToPosition(Vector2 p_position)
	{
		if (_originalSpeed != 0)
			this.speed = _originalSpeed;

		GoToPosition (p_position, PathTagNames.WALKING_TO_POINT.ToString());
	}

	public void RunToPosition(Vector2 p_position)
	{
		if(_originalSpeed != 0)
			this.speed = _originalSpeed * 3.543f;

		GoToPosition (p_position, PathTagNames.RUNNING_TO_POINT.ToString());
	}
}
