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

	private AStar _pathToFolow;
	private Rigidbody _rigidbody;
	private Vector3 _pointToMoveTo;
	private Transform _transform;
	private Vector2 _endPortalPosition;
	private List<AStar.Node> _pathableNodes;
	private float _originalSpeed;
	private bool _pauseMovement = false;


	void Start()
	{
		_originalSpeed = this.speed;
		_rigidbody = this.GetComponent<Rigidbody>();
		_transform = this.GetComponent<Transform>();
		_pointToMoveTo = _transform.position;
	}

	void Update()
	{
		FollowPathUpdate ();
	}

	private void FollowPathUpdate()
	{
		if(_pathToFolow != null && _pauseMovement == false)
		{
			var __distanceToPointToMove = Vector3.Distance(_transform.position, _pointToMoveTo);

			if(__distanceToPointToMove < 0.1f)
			{
				if(_pathToFolow.isEndOfPath)
				{
					if(_pathToFolow.tagName == PathTagNames.WALKING_TO_PORTAL.ToString())
					{
						if(onReachPortal != null)
							onReachPortal();
						
						_pauseMovement = true;
					}
					else
						WalkToTheEndPortal();
					
					return;
				}
				else
				{
					Vector2 __newPoint = _pathToFolow.GetNextPoint();
					_pointToMoveTo = new Vector3(__newPoint.x, 0, __newPoint.y);
				}
			}
			
			//make her move!
			Vector3 __positionToBe = _transform.position + Vector3.Normalize( _pointToMoveTo - _transform.position);
			_rigidbody.MovePosition(Vector3.Lerp(_transform.position, __positionToBe, this.speed * Time.deltaTime));
			_transform.LookAt(__positionToBe);
			
		}
	}

	public void SetPathToFollow(AStar p_path)
	{
		_pathToFolow = p_path;
	}

	public void InformEndPortalPosition(Vector2 p_position)
	{
		_endPortalPosition = p_position;
	}

	public void InformMapNodes(List<AStar.Node> p_nodes)
	{
		_pathableNodes = p_nodes;
	}

	public void WalkToTheEndPortal()
	{
		if(_originalSpeed != 0)
			this.speed = _originalSpeed;

		GoToPosition (_endPortalPosition, PathTagNames.WALKING_TO_PORTAL.ToString());
	}

	private void GoToPosition(Vector2 p_position, string p_pathTagName)
	{
		_pauseMovement = false;
		AStar __newPath = new AStar (this.Get2DPosition(), p_position, _pathableNodes);
		__newPath.tagName = p_pathTagName;
		__newPath.onPathProcessed += () => 
		{
			_pathToFolow = __newPath;
		};
		StartCoroutine (__newPath.ProcessPath ());
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
