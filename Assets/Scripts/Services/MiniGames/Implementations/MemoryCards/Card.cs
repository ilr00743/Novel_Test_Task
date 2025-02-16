using System;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Sprite _backImage;
    private Sprite _frontImage;
    
    private Image _currentImage;

    private bool _isFlipped;
    private bool _isMatched;
    
    public event Action<Card> CardSelected;
    public Sprite FrontImage => _frontImage;

    public void Initialize(Sprite frontImage)
    {
        _frontImage = frontImage;
        
        _currentImage = GetComponent<Image>();
        _currentImage.sprite = _backImage;
        
        _button.onClick.AddListener(OnCardClicked);
    }

    private void OnCardClicked()
    {
        if (_currentImage.sprite == _backImage)
        {
            Reveal();
            CardSelected?.Invoke(this);
        }
    }

    public void Reveal()
    {
        _currentImage.sprite = _frontImage;
    }

    public void Hide()
    {
        _currentImage.sprite = _backImage;
    }

    public void SetActiveState(bool isActive)
    {
        _button.enabled = isActive;
    }
}
