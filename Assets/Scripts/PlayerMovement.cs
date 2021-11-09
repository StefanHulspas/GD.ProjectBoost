using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(AudioSource))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private AudioSource _audioSource;

    [SerializeField] private float _upwardThrustPower = 1000;
    [SerializeField] private float _rotationThrustPower = 50;

    private bool _thrusterIsActive;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
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
        Vector3 leftRotationForce = Vector3.back * _rotationThrustPower;
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
        transform.Rotate(rotationForce * Time.deltaTime);
        _rigidbody.freezeRotation = false;
    }
}
