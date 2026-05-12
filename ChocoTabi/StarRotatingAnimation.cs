using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StarRotatingAnimation : MonoBehaviour
{

    private Vector3 _startLocalPosition;
    private bool _isPlaying;
    private float _playingTime;
    private float _elapsedTime;
    private bool _isLoop; // Stopが呼ばれるまでループするか否か
    
    [Header("回転する星のパラメーター")]
    [SerializeField] private float _rotationSpeed = 180f;
    [SerializeField] private float _floatAmplitude = 0.2f;
    [SerializeField] private float _heightYOffset = 0.7f;
    [SerializeField] private float _scale = 0.7f;

    void Awake()
    {
        transform.localPosition = new Vector3(0, _heightYOffset, 0);
        transform.localScale = Vector3.one * _scale;
        _startLocalPosition = transform.localPosition;
        _isPlaying = false;
        gameObject.SetActive(false);
        
    }
    
    //ヘロヘロな敵の頭上でぐるぐる回る星のアニメーションをループ再生する
    public void PlayLoop()
    {
        _isLoop = true;
        _elapsedTime = 0;
        transform.localPosition = _startLocalPosition;
        transform.localScale = Vector3.one * _scale;
        gameObject.SetActive(true);
        _isPlaying = true;
    }
    
    //ヘロヘロな敵の頭上でぐるぐる回る星のアニメーションを再生する
    public void PlayStarRotatingAnimation(float stunDuration)
    {
        _isLoop = false;
        _playingTime = stunDuration;
        _elapsedTime = 0;
        transform.localPosition = _startLocalPosition;
        transform.localScale = Vector3.one * _scale;
        gameObject.SetActive(true);
        _isPlaying = true;
    }

    //ヘロヘロな敵の頭上でぐるぐる回る星のアニメーションを停止する
    public void StopStarRotatingAnimation()
    {
        transform.localPosition = _startLocalPosition;
        gameObject.SetActive(false);
        _isPlaying = false;
    }

    void Update()
    {
        if (!_isPlaying) return;
        if (!_isLoop)
        {
            _playingTime -= Time.deltaTime;
            if (_playingTime <= 0)
            {
                StopStarRotatingAnimation();
                return;
            }
        }
        _elapsedTime += Time.deltaTime;

        transform.Rotate(0, _rotationSpeed * Time.deltaTime, 0);
        float yOffset = Mathf.Sin(2 * Mathf.PI * _elapsedTime)*_floatAmplitude;
        transform.localPosition = _startLocalPosition + new Vector3(0f, yOffset, 0f);

    }

}
