using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(AudioSource))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private AudioSource _audioSource;
    private RigidbodyConstraints _defaultRigidBodyConstraints;

    [SerializeField] private float _upwardThrustPower = 1000;
    [SerializeField] private float _rotationThrustPower = 50;
    [SerializeField] private AudioClip _thrusterSound;

    [SerializeField] private List<ParticleSystem> _upwardThrusters;
    [SerializeField] private List<ParticleSystem> _leftThrusters;
    [SerializeField] private List<ParticleSystem> _rightThrusters;

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
        HandleThrusterParticleSystems();
    }

	private void HandleThrusterParticleSystems()
	{
        HandleThrusterParticleSystems(KeyCode.UpArrow, _upwardThrusters);
        HandleThrusterParticleSystems(KeyCode.LeftArrow, _rightThrusters);
        HandleThrusterParticleSystems(KeyCode.RightArrow, _leftThrusters);
    }
    private void HandleThrusterParticleSystems(KeyCode key, List<ParticleSystem> particleSystems)
    {
        if (particleSystems.Count > 0) {
            if (Input.GetKey(key) && !particleSystems[0].isPlaying) {
                EnableParticleSystemList(particleSystems, true);
            } else if (!Input.GetKey(key) && particleSystems[0].isPlaying) {
                EnableParticleSystemList(particleSystems, false);
            }
		}
    }

    private void HandleAudio()
	{
        if (_thrusterIsActive && !_audioSource.isPlaying) {
            _audioSource.PlayOneShot(_thrusterSound);
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

    private void EnableParticleSystemList(List<ParticleSystem> particleSystems, bool isActive) {
        foreach (ParticleSystem ps in particleSystems) {
            if (isActive && !ps.isPlaying) {
                ps.Play();
            }
            if (!isActive && ps.isPlaying) {
                ps.Stop();
			}
		}
	}
}
