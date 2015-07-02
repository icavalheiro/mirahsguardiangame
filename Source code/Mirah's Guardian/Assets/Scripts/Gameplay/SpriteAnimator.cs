using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class SpriteAnimator
{
	[System.Serializable]
	public class Animation
	{
		public int animationID;
		public float frameRate;
		public List<Sprite> sprites;
		public int currentSprite = 0;
		public bool isCurrentAnimation = false;
	}

	private SpriteRenderer _renderer;
	public List<Animation> animations = new List<Animation>();
	private float _counter = 0;

	public void SetRenderer(SpriteRenderer p_renderer)
	{
		_renderer = p_renderer;
	}

	public void PlayAnimation(int p_animationID)
	{
		foreach (var animation in animations) 
		{
			animation.isCurrentAnimation = animation.animationID == p_animationID;
		}
	}

	public void Update()
	{
		var __currentAnimation = animations.Find (x => x.isCurrentAnimation);
		if (__currentAnimation == null)
			return;

		_counter += Time.deltaTime;
		if(_counter >= __currentAnimation.frameRate)
		{
			_counter = 0;
			__currentAnimation.currentSprite ++;
			if(__currentAnimation.currentSprite >= __currentAnimation.sprites.Count)
				__currentAnimation.currentSprite = 0;
		}

		_renderer.sprite = __currentAnimation.sprites [__currentAnimation.currentSprite];
	}
}
