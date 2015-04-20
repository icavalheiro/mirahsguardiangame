using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Mirah : Character 
{
	private AStar _pathToFolow;
	private Rigidbody _rigidbody;
	private Vector3 _pointToMoveTo;
	private Transform _transform;

	void Start()
	{
		_rigidbody = this.GetComponent<Rigidbody>();
		_transform = this.GetComponent<Transform>();
		_pointToMoveTo = _transform.position;
	}

	void Update()
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
		}
	}

	public void SetPathToFollow(AStar p_path)
	{
		_pathToFolow = p_path;
	}
}
