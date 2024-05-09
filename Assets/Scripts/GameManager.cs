using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _balls;
    [SerializeField] private GameObject _firePoint;
    [SerializeField] private float _ballPower;
    private int _activeBallIndex;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _balls[_activeBallIndex].transform.SetPositionAndRotation(_firePoint.transform.position, _firePoint.transform.rotation);
            _balls[_activeBallIndex].SetActive(true);
            _balls[_activeBallIndex].GetComponent<Rigidbody>().AddForce(_balls[_activeBallIndex].transform.TransformDirection(90,90,0) * _ballPower, ForceMode.Force);
            if (_balls.Length - 1 == _activeBallIndex)
            {
                _activeBallIndex = 0;
            }
            else
            {
                _activeBallIndex++;
            }
        }
    }
}
