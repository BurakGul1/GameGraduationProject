using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Ball Settings")]
    [SerializeField] private GameObject[] _balls;
    [SerializeField] private GameObject _firePoint;
    [SerializeField] private float _ballPower;
    private int _activeBallIndex;

    [Header("Level Settings")] 
    [SerializeField] private int _targetBallCount;
    [SerializeField] private int _totalBallCount;
    [SerializeField] private int _inBucketBallCount;
    [SerializeField] private Slider _levelSlider;
    [SerializeField] private TextMeshProUGUI _remainingBallCountTXT; 
    void Start()
    {
        _levelSlider.maxValue = _targetBallCount;
        _remainingBallCountTXT.text = _totalBallCount.ToString();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _totalBallCount--;
            _remainingBallCountTXT.text = _totalBallCount.ToString();
            _balls[_activeBallIndex].transform
                .SetPositionAndRotation(_firePoint.transform.position, _firePoint.transform.rotation);
            _balls[_activeBallIndex].SetActive(true);
            _balls[_activeBallIndex].GetComponent<Rigidbody>().AddForce(
                _balls[_activeBallIndex].transform.TransformDirection(90, 90, 0) * _ballPower, ForceMode.Force);
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

    internal void BallIn()
    {
        _inBucketBallCount++;
        _levelSlider.value = _inBucketBallCount;

        if (_inBucketBallCount==_targetBallCount)
        {
            //win panel
            //ball throwing locked.
            Debug.Log("Win");
        }

        if (_totalBallCount == 0 && _inBucketBallCount!=_targetBallCount)
        {
            //game over panel
            //ball throwing locked.
            Debug.Log("Game Over");
        }
        if (_totalBallCount + _inBucketBallCount < _targetBallCount)
        {
            Debug.Log("Game Over");
        }
    }

    internal void BallOut()
    {
        if (_totalBallCount == 0)
        {
            Debug.Log("Game Over");
        }

        if ((_totalBallCount + _inBucketBallCount) < _targetBallCount)
        {
            Debug.Log("Game Over");
        }
    }
}
