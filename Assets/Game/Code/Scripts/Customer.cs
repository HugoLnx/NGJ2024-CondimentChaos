using System;
using System.Collections;
using System.Collections.Generic;
using Jam;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Customer : MonoBehaviour
{
    const string SCOREPOPUPBASE = "+";
    [SerializeField] private bool _isWaiting = true;
    [SerializeField] private bool _isServed = false;
    [SerializeField] private float _timeToLeave = 5.0f;
    [SerializeField] private CustomerDifficulty _difficulty;
    [SerializeField] private CustomerType _type;
    public CustomerType Type => _type;
    public CustomerDifficulty Difficulty => _difficulty;
    private Animator _animator;
    private SpriteRenderer _foodPopupRenderer;
    [SerializeField] private Slider _slider;
    public int scoreValue = 100;
    public FlavorSO preferredFlavor;

    public float spawnRangeX = 8.0f;
    public float spawnRangeY = 2.76f;

    [SerializeField] private GameObject _foodPopup;
    [SerializeField] private TextMeshProUGUI _scorePopup;
    [SerializeField] private BoxCollider2D _boxCollider;
    [SerializeField] private AudioClip _spawnSfx;
    [SerializeField] private AudioClip[] _foodIsCorrectMatchSfx;
    [SerializeField] private AudioClip[] _foodIsWrongMatchSfx;
    [SerializeField] private AudioClip _foodIsUndelivered;
    [SerializeField] private AudioClip _customerDestroyed;

    public event Action OnFinish;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _foodPopupRenderer = _foodPopup.GetComponent<SpriteRenderer>();
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        SetSliderMaxValue(_timeToLeave);
        SetSliderCurrentValue(_timeToLeave);
        preferredFlavor = FlavorSORepository.Repo.GetRandom();
        SetFlavorPopup();
        AudioPlayer.Instance.PlaySFX(_spawnSfx);
    }

    // Update is called once per frame
    void Update()
    {
        if (_timeToLeave > 0.0f)
        {
            _timeToLeave -= Time.deltaTime;
            SetSliderCurrentValue(_timeToLeave);
            if (_timeToLeave <= 0.0f && !_isServed)
            {
                _isWaiting = false;
                _animator.SetBool("waiting", _isWaiting);
                _boxCollider.enabled = false;
                PlayTimeOutAnimation();
            }

            if (_timeToLeave > 0.0f && _isServed)
            {
                _isWaiting = false;
                _animator.SetBool("waiting", _isWaiting);
                PlaySatisfiedAnimation();
            }
        }
    }

    // sets sprites of food popup and flavor overlay
    private void SetFlavorPopup()
    {
        _foodPopupRenderer.sprite = preferredFlavor.Sprite;
    }
    // if a trigger enters the collider
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Food"))
        {
            FoodProjectile projectile = collision.gameObject.GetComponent<FoodProjectile>();
            if (projectile == null || !projectile.Launched) return;
            if (projectile.Flavor == preferredFlavor)
            {
                _boxCollider.enabled = false;
                Debug.Log("Points: " + scoreValue);
                UI.Instance.IncreaseScore(scoreValue);
                ShowScorePopup(scoreValue);
                _isServed = true;
                projectile.Served();
            }
        }
    }

    // plays timeout animation
    private void PlayTimeOutAnimation()
    {
        AudioPlayer.Instance.PlaySFX(_foodIsUndelivered);
        _animator.SetTrigger("timeout");
    }

    // plays satisfied animation
    private void PlaySatisfiedAnimation()
    {
        _animator.SetTrigger("satisfied");
        // AudioPlayer.Instance.PlaySFX(_foodIsCorrectMatchSfx);

    }

    // sets the patience meters max value
    private void SetSliderMaxValue(float maxValue)
    {
        _slider.maxValue = maxValue;
    }

    // sets the current value of the patience meter and colors it accordingly
    private void SetSliderCurrentValue(float currentValue)
    {
        _slider.value = currentValue;

        // if currentValue is between 33-65% of max value, color it yellow
        if (currentValue > _slider.maxValue * 0.33f && currentValue < _slider.maxValue * 0.65f)
        {
            _slider.image.color = Color.yellow;
            return;
        }

        // if currentvalue is under 32%, color it red
        if (currentValue < _slider.maxValue * 0.32f)
        {
            _slider.image.color = Color.red;
            return;
        }
    }

    // show score popup
    private void ShowScorePopup(int value)
    {
        _scorePopup.text = SCOREPOPUPBASE + value;
        // withalpha is Hugo's function
        Color c = _scorePopup.color;
        _scorePopup.color = new Color(c.r, c.b, c.g, 1);
    }

    // destroys this gameobject
    public void DestroyCustomer()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        OnFinish?.Invoke();
        AudioPlayer.Instance.PlaySFX(_customerDestroyed);
    }
}
