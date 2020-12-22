using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {
	
	[SerializeField] private float destroyAfter;
	[SerializeField] private bool destroyOnImpact = false;
	[SerializeField] private float minDestroyTime;
	[SerializeField] private float maxDestroyTime;
	
	[SerializeField] private float damage=10;

	[Header("Impact Effect Prefabs")]
	public Transform [] metalImpactPrefabs;

	private void Start () 
	{
		StartCoroutine (DestroyAfter ());
	}
	
	private void OnCollisionEnter (Collision collision) 
	{
		var collisionPoint = collision.gameObject;
		if (!destroyOnImpact) 
		{
			StartCoroutine (DestroyTimer ());
		}
		else 
		{
			Destroy (gameObject);
		}
		
		if (collision.transform.CompareTag("Metal")) 
		{
			Instantiate (metalImpactPrefabs [Random.Range 
				(0, metalImpactPrefabs.Length)], transform.position, 
				Quaternion.LookRotation (collision.contacts [0].normal));
			Destroy(gameObject);
		}
		
		if (collision.transform.CompareTag("Target")) 
		{
			collision.transform.gameObject.GetComponent<TargetScript>().isHit = true;
			Destroy(gameObject);
		}
		if (collisionPoint.CompareTag("Player") || collisionPoint.CompareTag("Enemy") ||
		    collisionPoint.CompareTag("Destructible"))
		{
			collisionPoint.GetComponent<Health>().ChangeHealth(-damage);
		}
		
	}

	private IEnumerator DestroyTimer () 
	{
		yield return new WaitForSeconds
			(Random.Range(minDestroyTime, maxDestroyTime));
		Destroy(gameObject);
	}

	private IEnumerator DestroyAfter () 
	{
		yield return new WaitForSeconds (destroyAfter);
		Destroy (gameObject);
	}
}
