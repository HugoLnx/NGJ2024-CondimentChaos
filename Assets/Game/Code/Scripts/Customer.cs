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
    private BoxCollider2D _collider;
    private Animator _animator;
    [SerializeField] private Slider _slider;
    public int scoreValue = 100;
    public FoodSO preferredFood;
    public FlavorSO preferredFlavor;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _collider = GetComponent<BoxCollider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        SetSliderMaxValue(_timeToLeave);
        SetSliderCurrentValue(_timeToLeave);
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
