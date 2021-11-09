using System;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
	private void OnCollisionEnter(Collision collision)
	{
		switch (collision.gameObject.tag) {
			case "Finish": HandleFinishCollision(); break;
			case "Fuel": HandleFuelCollision(); break;
			default: HandleDefaultCollision(); break;
		}
	}

	private void HandleDefaultCollision()
	{
		throw new NotImplementedException();
	}

	private void HandleFuelCollision()
	{
		throw new NotImplementedException();
	}

	private void HandleFinishCollision()
	{
		throw new NotImplementedException();
	}
}
