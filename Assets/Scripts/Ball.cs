using System;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody _rb;
    [SerializeField] private GameManager _gameManager;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bucket"))
        {
            TechnicalOperations();
            //particle effect
            //numbers update
            //slider
            _gameManager.BallIn();
        }
        else if (other.CompareTag("DestroyObject"))
        {
            TechnicalOperations();
            _gameManager.BallOut();
        }
    }

    void TechnicalOperations()
    {
        Renderer color = GetComponent<Renderer>();
        _gameManager.ParticleEffect(gameObject.transform.position, color.material.color);
        gameObject.transform.localPosition = Vector3.zero;
        gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero; 
        gameObject.SetActive(false);
    }
}
