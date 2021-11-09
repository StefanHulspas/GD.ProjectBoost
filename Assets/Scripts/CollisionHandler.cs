using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
	[SerializeField] private SceneTransitionParams _winParams;
	[SerializeField] private SceneTransitionParams _crashParams;

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
		LoadSceneIndex(currentSceneIndex, _crashParams);
	}

	private void HandleFinishCollision()
	{
		int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
		int nextSceneIndex = (currentSceneIndex + 1) % SceneManager.sceneCountInBuildSettings;
		LoadSceneIndex(nextSceneIndex, _winParams);
	}

	private void LoadSceneIndex(int newSceneIndex, SceneTransitionParams sceneTransitionParams) {
		if (isLoading) return;
		isLoading = true;

		GetComponent<PlayerMovement>().enabled = false;
		PlaySFX(sceneTransitionParams);
		StartCoroutine(LoadSceneIndexCoroutine(newSceneIndex, sceneTransitionParams.SecondsToWait));
	}

	private bool isLoading = false;
	private IEnumerator LoadSceneIndexCoroutine(int newSceneIndex, float secondsToWait) {
		yield return new WaitForSeconds(secondsToWait);
		SceneManager.LoadScene(newSceneIndex);
	}

	private void PlaySFX(SceneTransitionParams sceneTransitionParams) {
		Debug.Log(sceneTransitionParams.Message);
		_audioSource.Stop();
		if (sceneTransitionParams.AudioClip)
		{
			_audioSource.PlayOneShot(sceneTransitionParams.AudioClip);
		}
		if (sceneTransitionParams.ParticleSystem && !sceneTransitionParams.ParticleSystem.isPlaying)
		{
			sceneTransitionParams.ParticleSystem.Play();
		}
	}
}
