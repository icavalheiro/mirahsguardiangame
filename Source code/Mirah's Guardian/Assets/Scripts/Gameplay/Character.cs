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

	public Action onDead;

	public Type type;
	public float healthPoints = 3;
	public float speed = 1;

	void Awake()
	{
		GameplayManager.RegisterCharacter(this);
	}

	void LateUpdate()
	{
		if(Input.GetKeyDown("z"))
			healthPoints -= 1;
		if (healthPoints <= 0)
		if (onDead != null)
			onDead ();
	}

	public Vector2 Get2DPosition()
	{
		return new Vector2(transform.position.x, transform.position.z);
	}
}
