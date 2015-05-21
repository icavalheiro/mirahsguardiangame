using UnityEngine;
using System.Collections;
using System;

public class Guardian : Character 
{
	public Action onMirahCalled;

	public KeyCode keyToCallMirah = KeyCode.Space;

	private Rigidbody _rigidbody;
	private Transform _transform;
	public CanvasGroup balaoInterface;
	public float timer;
	public GameObject tiro;

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

		if(Input.GetKeyDown("b"))
			BalaoPensamento(3);
		
		if(timer < 0)
			balaoInterface.alpha = 0;
		else
			timer -= 1* Time.deltaTime;//multiplicar pelo tempo to sem tempo sai correndo

		if (Input.GetButtonDown("Fire1"))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray))
				Instantiate(tiro, transform.position, transform.rotation);
		}
	}

	public void BalaoPensamento(float tempo/*imagem variavel */)
	{
		balaoInterface.alpha = 1;
		timer = tempo * Time.deltaTime;
	}
}
