using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Card : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Sprite _backImage;
    [SerializeField] private float _spinDuration;
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
        var sequence = DOTween.Sequence();
        sequence.Append(transform.DOLocalRotate(new Vector3(0f, 90f, 0f), _spinDuration, RotateMode.FastBeyond360))
            .SetEase(Ease.Linear).AppendCallback(() => _currentImage.sprite = _frontImage)
            .Join(transform.DOLocalRotate(new Vector3(0f, 180f, 0f), _spinDuration, RotateMode.FastBeyond360).SetEase(Ease.Linear));
    }

    public void Hide()
    {
        var sequence = DOTween.Sequence();
        sequence.Append(transform.DOLocalRotate(new Vector3(0f, 90f, 0f), _spinDuration, RotateMode.FastBeyond360))
            .SetEase(Ease.Linear).AppendCallback(() => _currentImage.sprite = _backImage)
            .Join(transform.DOLocalRotate(new Vector3(0f, 0f, 0f), _spinDuration, RotateMode.FastBeyond360).SetEase(Ease.Linear));
    }

    public void SetActiveState(bool isActive)
    {
        _button.enabled = isActive;
    }
}
