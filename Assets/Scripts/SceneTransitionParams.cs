using UnityEngine;

public struct SceneTransitionParams
{
	public string Message;
	public float SecondsToWait;
	public AudioClip Sound;

	public SceneTransitionParams(string message, float secondsToWait, AudioClip sound) {
		Message = message;
		SecondsToWait = secondsToWait;
		Sound = sound;
	}
}
