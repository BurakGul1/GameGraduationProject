using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Ball Settings")] [SerializeField]
    private GameObject[] _balls;

    [SerializeField] private GameObject _firePoint;
    [SerializeField] private float _ballPower;
    private int _activeBallIndex;
    public Animator _ballThrow;
    public ParticleSystem _ballThrowParticleSystem;
    public ParticleSystem[] _BallEffects;
    private int _activeBallEffectIndex;
    public AudioSource[] _ballSounds;
    private int _activeBallSoundIndex;

    [Header("Level Settings")] [SerializeField]
    private int _targetBallCount;

    [SerializeField] private int _totalBallCount;
    [SerializeField] private int _inBucketBallCount;
    [SerializeField] private Slider _levelSlider;
    [SerializeField] private TextMeshProUGUI _remainingBallCountTXT;
    public bool levelCompleted = false;

    private CanvasUI _canvasUI;

    [Header("Other Settings")] public Renderer _bucketTranparentMaterial;
    private float _bucketFirstValue;
    private float _bucketAvailableValue;
    [SerializeField] private AudioSource[] _otherSounds;

    private void Awake()
    {
        _canvasUI = FindObjectOfType<CanvasUI>().GetComponent<CanvasUI>();
    }

    void Start()
    {
        _activeBallEffectIndex = 0;
        _activeBallSoundIndex = 0;
        _bucketFirstValue = .5f;
        _bucketAvailableValue = .25f / _targetBallCount;
        _levelSlider.maxValue = _targetBallCount;
        _remainingBallCountTXT.text = _totalBallCount.ToString();
    }

    // void Update()
    // {
    //     if (levelCompleted) return;
    //     if (Time.timeScale != 0)
    //     {
    //         if (Input.GetKeyDown(KeyCode.Space))
    //         {
    //             if (_totalBallCount > 0) // Ensure total ball count does not go below zero
    //             {
    //                 _totalBallCount--;
    //                 _remainingBallCountTXT.text = _totalBallCount.ToString();
    //                 _ballThrow.Play("BallThrow");
    //                 _ballThrowParticleSystem.Play();
    //                 _otherSounds[2].Play();
    //                 _balls[_activeBallIndex].transform
    //                     .SetPositionAndRotation(_firePoint.transform.position, _firePoint.transform.rotation);
    //                 _balls[_activeBallIndex].SetActive(true);
    //                 _balls[_activeBallIndex].GetComponent<Rigidbody>().AddForce(
    //                     _balls[_activeBallIndex].transform.TransformDirection(90, 90, 0) * _ballPower, ForceMode.Force);
    //
    //                 if (_balls.Length - 1 == _activeBallIndex)
    //                 {
    //                     _activeBallIndex = 0;
    //                 }
    //                 else
    //                 {
    //                     _activeBallIndex++;
    //                 }
    //             }
    //
    //             int num = 0;
    //             foreach (var item in _balls)
    //             {
    //                 if (item.activeInHierarchy)
    //                 {
    //                     num++;
    //                 }
    //             }
    //
    //             if (num == 0)
    //             {
    //                 if (_totalBallCount == 0 && _inBucketBallCount != _targetBallCount)
    //                 {
    //                     GameOver();
    //                 }
    //             }
    //         }
    //     }
    // }

    internal void BallIn()
    {
        _inBucketBallCount++;
        _levelSlider.value = _inBucketBallCount;
        _bucketFirstValue -= _bucketAvailableValue;
        _bucketTranparentMaterial.material.SetTextureScale("_MainTex", new Vector2(1f, _bucketFirstValue));
        _ballSounds[_activeBallSoundIndex].Play();
        _activeBallSoundIndex++;
        if (_activeBallSoundIndex == _ballSounds.Length - 1)
        {
            _activeBallSoundIndex = 0;
        }

        if (_inBucketBallCount == _targetBallCount)
        {
            Win();
        }

        int num = 0;
        foreach (var item in _balls)
        {
            if (item.activeInHierarchy)
            {
                num++;
            }
        }

        if (num == 0)
        {
            if (_totalBallCount == 0 && _inBucketBallCount != _targetBallCount)
            {
                GameOver();
            }

            if (_totalBallCount + _inBucketBallCount < _targetBallCount)
            {
                GameOver();
            }
        }
    }

    internal void BallOut()
    {
        int num = 0;
        foreach (var item in _balls)
        {
            if (item.activeInHierarchy)
            {
                num++;
            }
        }

        if (num == 0)
        {
            if (_totalBallCount == 0 && _inBucketBallCount != _targetBallCount)
            {
                GameOver();
            }

            if (_totalBallCount + _inBucketBallCount < _targetBallCount)
            {
                GameOver();
            }
        }
    }

    private void Win()
    {
        _otherSounds[1].Play();
        levelCompleted = true;
        Debug.Log("Win");
        PlayerPrefs.SetInt("Level", SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 0f;
        _canvasUI._LevelCount.text = "Level : " + SceneManager.GetActiveScene().buildIndex.ToString();
        _canvasUI.winPanel.SetActive(true);
        // Win panelini açabilir veya diğer kazandı işlemlerini yapabilirsiniz.
    }

    private void GameOver()
    {
        _otherSounds[0].Play();
        levelCompleted = false;
        Debug.Log("Game Over");
        Time.timeScale = 0f;
        _canvasUI.gameOverPanel.SetActive(true);
        // Game over panelini açabilir veya diğer kaybetti işlemlerini yapabilirsiniz.
    }

    public void ParticleEffect(Vector3 pos, Color color)
    {
        _BallEffects[_activeBallEffectIndex].transform.position = pos;
        var main = _BallEffects[_activeBallEffectIndex].main;
        main.startColor = color;
        _BallEffects[_activeBallEffectIndex].gameObject.SetActive(true);
        _activeBallEffectIndex++;
        if (_activeBallEffectIndex == _BallEffects.Length - 1)
        {
            _activeBallEffectIndex = 0;
        }
    }

    public void BallThrow()
    {
        if (levelCompleted) return;
        if (Time.timeScale != 0)
        {
            if (_totalBallCount > 0) // Ensure total ball count does not go below zero
            {
                _totalBallCount--;
                _remainingBallCountTXT.text = _totalBallCount.ToString();
                _ballThrow.Play("BallThrow");
                _ballThrowParticleSystem.Play();
                _otherSounds[2].Play();
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

            int num = 0;
            foreach (var item in _balls)
            {
                if (item.activeInHierarchy)
                {
                    num++;
                }
            }

            if (num == 0)
            {
                if (_totalBallCount == 0 && _inBucketBallCount != _targetBallCount)
                {
                    GameOver();
                }
            }
        }
    }
}