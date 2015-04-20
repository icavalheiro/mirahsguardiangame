using UnityEngine;
using System.Collections;
using System;

public class Guardian : Character 
{
	public Action onMirahCalled;

	public KeyCode keyToCallMirah = KeyCode.Space;

	private Rigidbody _rigidbody;
	private Transform _transform;

	void Start()
	{
		_transform = this.GetComponent<Transform> ();
		_rigidbody = this.GetComponent<Rigidbody> ();
	}


	void Update()
	{
		if(Input.GetKeyDown(keyToCallMirah))
		{
			if(onMirahCalled != null)
				onMirahCalled();
		}

		float __horizontalAxis = Input.GetAxis ("Horizontal");
		float __verticalAxis = Input.GetAxis ("Vertical");
		_rigidbody.MovePosition (_transform.position + ((new Vector3(__horizontalAxis, 0, __verticalAxis) * speed) * Time.deltaTime));

	}
}
