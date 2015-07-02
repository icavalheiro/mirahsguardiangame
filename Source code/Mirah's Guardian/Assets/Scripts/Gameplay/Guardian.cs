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
	public GameObject shield;
	public float __horizontalAxis = 0;
	public float __verticalAxis = 0;
	public int municao = 3;
	public AudioSource assovio;

	void Start()
	{
		_transform = this.GetComponent<Transform> ();
		_rigidbody = this.GetComponent<Rigidbody> ();

		switch(Application.loadedLevel)
		{
			case 0: //Primeira fase
				municao = 3;
				break;
			case 1:
				municao = 2;
				break;
		}
	}


	void Update()
	{
		if(Input.GetKeyDown(keyToCallMirah))
		{
			assovio.Play();
			if(onMirahCalled != null)
				onMirahCalled();
		}

		if(!Input.GetButton("Fire2"))
		{
			__horizontalAxis = Input.GetAxis ("Horizontal");
			__verticalAxis = Input.GetAxis ("Vertical");
			_rigidbody.MovePosition (_transform.position + ((new Vector3(__horizontalAxis, 0, __verticalAxis) * speed) * Time.deltaTime));
		}

		if(Input.GetKeyDown("b"))
			BalaoPensamento(6);
		
		if(timer < 0)
			balaoInterface.alpha = 0;
		else
			timer -= 1* Time.deltaTime;

		if (Input.GetButtonDown("Fire1"))
		{
			if(municao > 0)
			{
				municao--;
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				if (Physics.Raycast(ray))
					Instantiate(tiro, transform.position, transform.rotation);
			}
		}

		if(Input.GetButtonDown("Fire2"))
		{
			Instantiate(shield, transform.position+ new Vector3(__horizontalAxis,1,__verticalAxis), transform.rotation);
		}
	}

	public void BalaoPensamento(float tempo/*imagem variavel */)
	{
		balaoInterface.alpha = 1;
		timer = tempo * Time.deltaTime;
	}
}
