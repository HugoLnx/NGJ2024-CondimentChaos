using System.Collections;
using System.Collections.Generic;
using Jam;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Customer : MonoBehaviour
{
    [SerializeField] private bool _isWaiting = true;
    [SerializeField] private bool _isServed = false;
    [SerializeField] private float _timeToLeave = 5.0f;
    private Animator _animator;
    private SpriteRenderer _foodPopupRenderer;
    private SpriteRenderer _flavorRenderer;
    [SerializeField] private Slider _slider;
    public int scoreValue = 100;
    public FoodSO preferredFood;
    public FlavorSO preferredFlavor;

    public float spawnRangeX = 8.0f;
    public float spawnRangeY = 2.76f;

    [SerializeField] private GameObject _foodPopup;
    [SerializeField] private GameObject _flavorOverlay;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _foodPopupRenderer = _foodPopup.GetComponent<SpriteRenderer>();
        _flavorRenderer = _flavorOverlay.GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        SetSliderMaxValue(_timeToLeave);
        SetSliderCurrentValue(_timeToLeave);
        preferredFood = FoodSORepository.Repo.GetRandom();
        preferredFlavor = FlavorSORepository.Repo.GetRandom();
        SetFoodPopup();
        transform.position = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), Random.Range(-spawnRangeY, spawnRangeY), 0);
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
    private void SetFoodPopup()
    {
        _foodPopupRenderer.sprite = preferredFood.Texture;
        _flavorRenderer.sprite = preferredFood.FlavorTexture;
        _flavorRenderer.color = preferredFlavor.Color;
    }
    // if a trigger enters the collider
    void OnTriggerEnter2D(Collider2D collision)
    {
        _isServed = true;
    }

    // plays timeout animation
    private void PlayTimeOutAnimation()
    {
        _animator.SetTrigger("timeout");
    }

    // plays satisfied animation
    private void PlaySatisfiedAnimation()
    {
        _animator.SetTrigger("satisfied");
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

    // destroys this gameobject
    public void DestroyCustomer()
    {
        Destroy(gameObject);
    }
}
