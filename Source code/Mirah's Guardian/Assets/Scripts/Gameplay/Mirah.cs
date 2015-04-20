using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Mirah : Character 
{
	public event Action onReachPortal;

	private AStar _pathToFolow;
	private Rigidbody _rigidbody;
	private Vector3 _pointToMoveTo;
	private Transform _transform;
	private Vector2 _endPortalPosition;
	private List<AStar.Node> _pathableNodes;
	private bool _isWalkingToPortal = false;

	void Start()
	{
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
		if(_pathToFolow != null)
		{
			if(Vector3.Distance(_transform.position, _pointToMoveTo) < 0.1f)
			{
				Vector2 __pointTo = _pathToFolow.GetNextPoint();
				_pointToMoveTo = new Vector3(__pointTo.x, 0, __pointTo.y);
			} 
			
			if(Vector3.Distance(_transform.position, _pointToMoveTo) > 0.1f)
			{
				Vector3 __positionToBe = _transform.position + Vector3.Normalize( _pointToMoveTo - _transform.position);
				_rigidbody.MovePosition(Vector3.Lerp(_transform.position, __positionToBe, this.speed * Time.deltaTime));
				_transform.LookAt(__positionToBe);
			}
			else
			{
				if(_isWalkingToPortal)
				{
					if(onReachPortal != null)
						onReachPortal();
				}
				else
					WalkToTheEndPortal();
			}
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
		WalkToPosition (_endPortalPosition);
		_isWalkingToPortal = true;
	}

	public void WalkToPosition(Vector2 p_position)
	{
		_isWalkingToPortal = false;
		AStar __newPath = new AStar (this.Get2DPosition(), p_position, _pathableNodes);
		__newPath.onPathProcessed += () => _pathToFolow = __newPath;
		StartCoroutine (__newPath.ProcessPath ());
	}
}
