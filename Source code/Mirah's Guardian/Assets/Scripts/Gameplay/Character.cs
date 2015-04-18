using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour 
{
	public enum Type
	{
		UNDEFINED,
		MIRAH,
		GUARDIAN,
		ENEMY
	}

	public Type type;
	public float helthPoints = 3;
	public float speed = 1;

	void Awake()
	{
		GameplayManager.RegisterCharacter(this);
	}
}
