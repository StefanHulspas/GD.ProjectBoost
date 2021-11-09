using System;
using UnityEngine;

[Serializable]
public struct SceneTransitionParams
{
	public string Message;
	public float SecondsToWait;
	public AudioClip AudioClip;
	public ParticleSystem ParticleSystem;

	public SceneTransitionParams(string message, float secondsToWait, AudioClip audioClip, ParticleSystem particleSystem) {
		Message = message;
		SecondsToWait = secondsToWait;
		AudioClip = audioClip;
		ParticleSystem = particleSystem;
	}
}
