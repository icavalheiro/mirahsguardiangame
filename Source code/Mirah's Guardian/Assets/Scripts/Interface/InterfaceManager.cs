using UnityEngine;
using System.Collections;

public class InterfaceManager : MonoBehaviour 
{
	public bool paused = false;
	public CanvasGroup gameInterface;
	public CanvasGroup pauseInterface;

	void Update()
	{
		if(Input.GetKeyDown("escape"))
			OnEscapePressed();
	}

	public void OnEscapePressed()
	{
		paused = !paused;
		
		Time.timeScale = (paused) ? 0 : 1 ;
		EnableCanvas(gameInterface, !paused);
		EnableCanvas(pauseInterface, paused);
	}

	private void EnableCanvas(CanvasGroup p_canvas, bool p_enable)
	{
		p_canvas.alpha = (p_enable) ? 1 : 0;
		p_canvas.blocksRaycasts = p_enable;
		p_canvas.interactable = p_enable;
	}

}
