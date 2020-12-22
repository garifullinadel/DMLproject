using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
	[SerializeField] private float maxHealth = 100.0f;
	[SerializeField] private bool canDie = true;
	
	private float currentHealth;
	private bool dead = false;

	// Start is called before the first frame update
	void Start()
	{
		currentHealth = maxHealth;

	}

	public void ChangeHealth(float amount)
	{
		currentHealth += amount;
		if (currentHealth <= 0 && !dead && canDie)
		{
			dead = true;
			transform.Rotate(45, 0, 0);
			Destroy(gameObject, 2);
		}

		else if (currentHealth > maxHealth)
			currentHealth = maxHealth;
	}
}
	
