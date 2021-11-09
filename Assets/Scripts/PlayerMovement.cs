using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(AudioSource))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private AudioSource _audioSource;
    private RigidbodyConstraints _defaultRigidBodyConstraints;

    [SerializeField] private float _upwardThrustPower = 1000;
    [SerializeField] private float _rotationThrustPower = 50;

    private bool _thrusterIsActive;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _defaultRigidBodyConstraints = _rigidbody.constraints;
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        _thrusterIsActive = false;
        ProcessInput();
        HandleAudio();
    }

	private void HandleAudio()
	{
        if (_thrusterIsActive && !_audioSource.isPlaying) {
            _audioSource.Play();
		} 
        else if (!_thrusterIsActive && _audioSource.isPlaying) {
            _audioSource.Stop();
		}
	}

	private void ProcessInput() {
        ProcessUpwardsForce();
        ProcessRotationForce();
	}

	private void ProcessUpwardsForce()
	{
        Vector3 upwardsForce = Vector3.up * _upwardThrustPower * Time.deltaTime;
        if (Input.GetKey(KeyCode.UpArrow))
        {
            _thrusterIsActive = true;
            _rigidbody.AddRelativeForce(upwardsForce);
        }
    }

    private void ProcessRotationForce()
    {
        Vector3 leftRotationForce = Vector3.forward * _rotationThrustPower;
        if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
        {
            _thrusterIsActive = true;
            ApplyRotation(leftRotationForce);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            _thrusterIsActive = true;
            ApplyRotation(-leftRotationForce);
        }
    }

    private void ApplyRotation(Vector3 rotationForce) {
        _rigidbody.freezeRotation = true;
        Quaternion deltaRotation = Quaternion.Euler(rotationForce * Time.deltaTime);
        _rigidbody.MoveRotation(_rigidbody.rotation * deltaRotation);
        _rigidbody.constraints = _defaultRigidBodyConstraints;
    }
}
