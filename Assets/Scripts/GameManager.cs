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
    public bool levelCompleted = false;

    void Start()
    {
        _levelSlider.maxValue = _targetBallCount;
        _remainingBallCountTXT.text = _totalBallCount.ToString();
    }

    void Update()
    {
        if (levelCompleted) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _totalBallCount--;
            _remainingBallCountTXT.text = _totalBallCount.ToString();
            _balls[_activeBallIndex].transform.SetPositionAndRotation(_firePoint.transform.position, _firePoint.transform.rotation);
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

            if (_totalBallCount == 0 && _inBucketBallCount != _targetBallCount)
            {
                GameOver();
            }
        }
    }

    internal void BallIn()
    {
        _inBucketBallCount++;
        _levelSlider.value = _inBucketBallCount;

        if (_inBucketBallCount == _targetBallCount)
        {
            Win();
        }

        if (_totalBallCount + _inBucketBallCount < _targetBallCount)
        {
            GameOver();
        }
    }

    internal void BallOut()
    {
        if (_totalBallCount == 0)
        {
            GameOver();
        }

        if ((_totalBallCount + _inBucketBallCount) < _targetBallCount)
        {
            GameOver();
        }
    }

    private void Win()
    {
        levelCompleted = true;
        Debug.Log("Win");
        // Win panelini açabilir veya diğer kazandı işlemlerini yapabilirsiniz.
    }

    private void GameOver()
    {
        levelCompleted = false;
        Debug.Log("Game Over");
        // Game over panelini açabilir veya diğer kaybetti işlemlerini yapabilirsiniz.
    }
}