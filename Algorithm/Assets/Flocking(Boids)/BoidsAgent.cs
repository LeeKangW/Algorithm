using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidsAgent : MonoBehaviour
{
    [Header("Velocity")]
    [SerializeField]
    Vector3 _velocity;
    [SerializeField]
    float _maxVelocity = 2f;

    [Header("Detect Range")]
    [SerializeField, Range(0, 100f)]
    float _detectRange = 10f;

    public float GetDetectRange() => _detectRange;

    [SerializeField, Range(0, 100f)]
    float _sperationRange = 5f;

    public float GetSperationRange() => _sperationRange;

    private void Awake()
    {
        _velocity = new Vector3(
            Random.Range(-10f, 10f),
            Random.Range(-10f, 10f), 
            Random.Range(-10f, 10f));
    }

    // Update is called once per frame
    void Update()
    {
        if (_velocity.sqrMagnitude > (_maxVelocity * _maxVelocity))
            _velocity = _velocity.normalized * _maxVelocity;

        this.transform.position += _velocity * Time.deltaTime;
        this.transform.rotation = Quaternion.LookRotation(_velocity);
    }

    public void AddVeocity(in Vector3 rhs)
    {
        _velocity += rhs;
    }
}
