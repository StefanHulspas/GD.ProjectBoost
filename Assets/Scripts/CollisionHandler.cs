using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
	[SerializeField] public AudioClip _winSound;
	[SerializeField] public AudioClip _crashSound;

	private AudioSource _audioSource;

	private void Start()
	{
		_audioSource = GetComponent<AudioSource>();
	}

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
		SceneTransitionParams SceneTransition = new SceneTransitionParams("You Crashed!!", 1f, _crashSound);
		StartCoroutine(LoadSceneIndex(currentSceneIndex, SceneTransition));
	}

	private void HandleFinishCollision()
	{
		int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
		int nextSceneIndex = (currentSceneIndex + 1) % SceneManager.sceneCountInBuildSettings;
		SceneTransitionParams SceneTransition = new SceneTransitionParams("You Win!!", 1f, _winSound);
		StartCoroutine(LoadSceneIndex(nextSceneIndex, SceneTransition));
	}


	private bool isLoading = false;
	private IEnumerator LoadSceneIndex(int newSceneIndex, SceneTransitionParams sceneTransitionParams) {
		if (isLoading) yield break;
		isLoading = true;
		GetComponent<PlayerMovement>().enabled = false;
		Debug.Log(sceneTransitionParams.Message);
		_audioSource.PlayOneShot(sceneTransitionParams.Sound);
		yield return new WaitForSeconds(sceneTransitionParams.SecondsToWait);
		SceneManager.LoadScene(newSceneIndex);
	}
}
