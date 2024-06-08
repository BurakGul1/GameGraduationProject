using System;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bucket"))
        {
            gameObject.transform.localPosition = Vector3.zero;
            gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
            _rb.velocity = Vector3.zero;
            _rb.angularVelocity = Vector3.zero;
            gameObject.SetActive(false);
            //particle effect
            //numbers update
            //slider
            
        }
        else if (other.CompareTag("DestroyObject"))
        {
            gameObject.transform.localPosition = Vector3.zero;
            gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
            _rb.velocity = Vector3.zero;
            _rb.angularVelocity = Vector3.zero; 
            gameObject.SetActive(false);
        }
    }
}
