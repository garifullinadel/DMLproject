using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactScript : MonoBehaviour {

    [Header("Impact Timer")]
    [SerializeField] private float destroyTimer = 10.0f;

    [Header("Audio")]
    [SerializeField] private AudioClip[] impactSounds;
    [SerializeField] private AudioSource audioSource;

    private void Start () {
        StartCoroutine (DestroyTimer ());
        
        audioSource.clip = impactSounds[Random.Range(0, impactSounds.Length-1)];
        audioSource.Play();
    }
	
    private IEnumerator DestroyTimer() {
        yield return new WaitForSeconds (destroyTimer);
        Destroy (gameObject);
    }
}
