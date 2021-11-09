using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
	private void OnCollisionEnter(Collision collision)
	{
		switch (collision.gameObject.tag) {
			case "Friendly": HandleFriendlyCollision(); break;
			case "Finish": HandleFinishCollision(); break;
			default: HandleDefaultCollision(); break;
		}
	}

	private void HandleFriendlyCollision()
	{
	}

	private void HandleDefaultCollision()
	{
		int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
		SceneTransitionParams SceneTransition = new SceneTransitionParams("You Crashed!!", 1f);
		StartCoroutine(LoadSceneIndex(currentSceneIndex, SceneTransition));
	}

	private void HandleFinishCollision()
	{
		int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
		int nextSceneIndex = (currentSceneIndex + 1) % SceneManager.sceneCountInBuildSettings;
		SceneTransitionParams SceneTransition = new SceneTransitionParams("You Win!!", 1f);
		StartCoroutine(LoadSceneIndex(nextSceneIndex, SceneTransition));
	}

	private IEnumerator LoadSceneIndex(int newSceneIndex, SceneTransitionParams sceneTransitionParams) {
		GetComponent<PlayerMovement>().enabled = false;
		Debug.Log(sceneTransitionParams.Message);
		yield return new WaitForSeconds(sceneTransitionParams.SecondsToWait);
		SceneManager.LoadScene(newSceneIndex);
	}
}
