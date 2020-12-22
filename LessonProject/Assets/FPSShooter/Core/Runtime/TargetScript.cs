using UnityEngine;
using System.Collections;

public class TargetScript : MonoBehaviour {

	private float randomTime;
	private bool coroutineStarted = false;
	
	public bool isHit = false;
	[Header("Respawn time")]
	[SerializeField] private float minTime;
	[SerializeField] private float maxTime;

	[Header("Audio")]
	[SerializeField] private AudioClip upSound;
	[SerializeField] private AudioClip downSound;
	[SerializeField] private AudioSource audioSource;

	[SerializeField] private Animation _animation;
	
	private void Update () {
		randomTime = Random.Range (minTime, maxTime);
		if (isHit == true) 
		{
			if (coroutineStarted == false) 
			{
				_animation.Play("target_down");
				audioSource.clip = downSound;
				audioSource.Play();
				
				StartCoroutine(DelayTimer());
				coroutineStarted = true;
			} 
		}
	}
	
	private IEnumerator DelayTimer () {
		yield return new WaitForSeconds(randomTime);
		_animation.Play ("target_up");
		audioSource.clip = upSound;
		audioSource.Play();
		
		isHit = false;
		coroutineStarted = false;
	}
}