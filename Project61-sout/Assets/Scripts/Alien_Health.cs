﻿using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using UnityEngine.Networking;

public class Alien_Health : NetworkBehaviour {

	private int health = 50;
	private Animator anim;
	private float hasFinishedDyingTime= 4f;
	private bool hasPlayedDyingAnimation = false;

	void Start()
	{
		anim = GetComponent<Animator>();
	}

	private void Update()
	{
		if (hasPlayedDyingAnimation && Time.time > hasFinishedDyingTime)
		{
			Destroy(gameObject);
		}
	}

	public void DeductHealth (int dmg)
	{
		health -= dmg;
		CheckHealth();
		if (health < 0)
		{
			Debug.Log("zombie " + health + " collider " + GetComponent<CapsuleCollider>().enabled) ;

		}
	}

	void CheckHealth()
	{
		if(health <= 0 && !hasPlayedDyingAnimation)
		{
			GetComponent<CapsuleCollider>().enabled = false;
			anim.SetBool("isDead", true);
			hasPlayedDyingAnimation = true;
			GetComponent<ChaseAlien>().enabled = false;
			GetComponent<NavMeshAgent>().enabled = false;
			hasFinishedDyingTime = Time.time + hasFinishedDyingTime;
			
		}
	}
}
