using UnityEngine;
using System.Collections;
using System;

public class Enemy : Character 
{
	#region events
	public event Action<Mirah> onTargetFound;
	public event Action onTargetLost;
	#endregion

	#region inspector
	public float detectionAreaRadius = 5;
	#endregion

	#region private data
	private Vector3 _position;
	private Mirah _currentTarget;
	private Quaternion _originalRotation;
	private Transform _transform;
	#endregion

	#region unity methods
	void Awake()
	{
		onTargetLost += () => transform.rotation = _originalRotation;
	}

	void Start()
	{
		_position = transform.position;
		_originalRotation = transform.rotation;
		_transform = this.transform;
	}

	void OnDrawGizmosSelected ()
	{
		Gizmos.DrawWireSphere (this.transform.position, detectionAreaRadius);
	}

	void Update()
	{
		ManageAimAndShoot ();
	}

	void LateUpdate()
	{
		ManageTarget ();
	}
	#endregion

	private void ManageAimAndShoot()
	{
		if (_currentTarget == null) 
			return;

		_transform.LookAt (_currentTarget.transform.position);
	}

	private void ManageTarget()
	{
		if (_currentTarget == null)
			SearchAndDetect ();
		else if (Vector3.Distance (_position, _currentTarget.transform.position) > detectionAreaRadius) 
		{
			_currentTarget = null;
			if(onTargetLost != null)
				onTargetLost();
		}
	}

	private void SearchAndDetect()
	{
		var __detectedObjects = Physics.OverlapSphere (_position, detectionAreaRadius);
		foreach(Collider objCollider in __detectedObjects)
		{
			if(objCollider.gameObject.layer != LayerMask.NameToLayer("EnemyDetectable"))
				continue;

			Transform __rootObj = objCollider.transform.root;
			Mirah __mirahScript = __rootObj.GetComponent<Mirah>();
			if(__mirahScript != null)
			{
				_currentTarget = __mirahScript;
				if(onTargetFound != null)
					onTargetFound(_currentTarget);
			}
		}
	}
}
