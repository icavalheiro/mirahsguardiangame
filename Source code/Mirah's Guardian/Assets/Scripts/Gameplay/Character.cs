using UnityEngine;
using System.Collections;
using System;

public class Character : MonoBehaviour 
{
	public enum Type
	{
		UNDEFINED,
		MIRAH,
		GUARDIAN,
		ENEMY
	}


	#region public events
	public Action onDead;
	#endregion

	#region public data
	public Type type;
	public float healthPoints 
	{
		get { return _healthPoints; }
		set 
		{
			_healthPoints = value;
			if (_healthPoints <= 0)
				if (onDead != null)
					onDead ();
		}
	}
	public float speed = 1;
	#endregion

	#region private data
	private float _healthPoints = 3;
	#endregion

	void Awake()
	{
		GameplayManager.RegisterCharacter(this);
	}

	void LateUpdate()
	{
		//debuging only (lembre-se que isso vai ser executado em todos os character da cena (guardian, mirah, inimigos)
		if(Input.GetKeyDown("z"))
			healthPoints -= 1;
	}

	public Vector2 Get2DPosition()
	{
		return new Vector2(transform.position.x, transform.position.z);
	}
}
