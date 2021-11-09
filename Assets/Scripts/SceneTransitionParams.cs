public struct SceneTransitionParams
{
	public string Message;
	public float SecondsToWait;

	public SceneTransitionParams(string message, float secondsToWait) {
		Message = message;
		SecondsToWait = secondsToWait;
	}
}
