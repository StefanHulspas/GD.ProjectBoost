using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
	private void OnCollisionEnter(Collision collision)
	{
		switch (collision.gameObject.tag) {
			case "Friendly": HandleFriendlyCollision(); break;
			case "Finish": HandleFinishCollision(); break;
			case "Fuel": HandleFuelCollision(); break;
			default: HandleDefaultCollision(); break;
		}
	}

	private void HandleFriendlyCollision()
	{
	}

	private void HandleDefaultCollision()
	{
		int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
		LoadSceneIndex(currentSceneIndex);
	}

	private void HandleFuelCollision()
	{
	}

	private void HandleFinishCollision()
	{
		int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
		int nextSceneIndex = (currentSceneIndex + 1) % SceneManager.sceneCountInBuildSettings;
		LoadSceneIndex(nextSceneIndex);
	}

	private void LoadSceneIndex(int newSceneIndex) {
		SceneManager.LoadScene(newSceneIndex);
	}
}
