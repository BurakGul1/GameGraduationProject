using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _ball;
    [SerializeField] private GameObject _firePoint;
    [SerializeField] private float _ballPower;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject ball = Instantiate(_ball, _firePoint.transform.position, _firePoint.transform.rotation);
            ball.GetComponent<Rigidbody>().AddForce(ball.transform.TransformDirection(90,90,0) * _ballPower, ForceMode.Force);   
        }
    }
}
