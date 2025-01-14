﻿using UnityEngine;
using System.Collections;

public class Rocket : MonoBehaviour
{
	GameObject sounder;
	ExtAudio extaudio;

		
	void Start ()
	{
		// Destroy the rocket after 2 seconds if it doesn't get destroyed before then.
		Destroy(gameObject, 2);
	}


	void OnExplode()
	{
		// Create a quaternion with a random rotation in the z-axis.
		Quaternion randomRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		// If it hits an enemy...
		if(col.tag == "Enemy")
		{
			// ... find the Enemy script and call the Hurt function.
			col.gameObject.GetComponent<Enemy>().Hurt();

			// Call the explosion instantiation.
			OnExplode();

			Destroy (gameObject);
		}
		// Otherwise if it hits a bomb crate...
		else if(col.tag == "ground" || col.tag == "Obstacle")
		{
			OnExplode();
			ExtAudio.sounding.PlayOneShot(ExtAudio.bulletImpact);
			Destroy (gameObject);
		}
		else if(col.tag == "BombPickup")
		{
			// ... find the Bomb script and call the Explode function.
			Bomb.Explode();

			// Destroy the bomb crate.
			Destroy (col.transform.root.gameObject);

			// Destroy the rocket.
			Destroy (gameObject);
		}
		// Otherwise if the player manages to shoot himself...
		else if(col.gameObject.tag != "Player")
		{
			// Instantiate the explosion and destroy the rocket.
			OnExplode();
			Destroy (gameObject);
		}
	}
}
