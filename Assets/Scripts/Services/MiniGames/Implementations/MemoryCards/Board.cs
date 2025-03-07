﻿using System;
using System.Collections;
using System.Collections.Generic;
using Naninovel;
using Services.MiniGames.Configs;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Services.MiniGames.Implementations.MemoryCards
{
    [RequireComponent(typeof(GridLayoutGroup))]
    public class Board : MonoBehaviour
    {
        [SerializeField] private MemoryConfig _memoryConfig;
        [SerializeField] private Card _cardPrefab;
        [SerializeField] private Sprite[] _frontImages;
        [SerializeField] private int _pairs;
        
        private GridLayoutGroup _grid;
        private Transform _cardsContainer;

        private Card _firstCard, _secondCard;
        private List<Card> _cards = new();
        private int _pairsFoundCount;

        public event Action AllPairsFound;
        
        [ContextMenu("Init board")]
        public void InitializeBoard()
        {
            _grid = GetComponent<GridLayoutGroup>();
            _cardsContainer = transform;
            
            ResetBoard();

            AdjustGridLayout();
            GenerateCards();
            SubscribeOnCardsEvents();
        }

        private void ResetBoard()
        {
            foreach (Transform card in _cardsContainer)
            {
                Destroy(card.gameObject);
            }
            _cards.Clear();
        }

        private void SubscribeOnCardsEvents()
        {
            foreach (var card in _cards)
            {
                card.CardSelected += OnCardSelected;
            }
        }
        
        private void UnsubscribeOnCardsEvents()
        {
            foreach (var card in _cards)
            {
                card.CardSelected -= OnCardSelected;
            }
        }
        
        private void GenerateCards()
        {
            List<Sprite> selectedImages = new();
        
            for (int i = 0; i < _pairs; i++)
            {
                selectedImages.Add(_frontImages[i]);
                selectedImages.Add(_frontImages[i]);
            }

            ShuffleList(selectedImages);

            foreach (Sprite image in selectedImages)
            {
                var newCard = Instantiate(_cardPrefab, _cardsContainer);

                newCard.Initialize(image);
                _cards.Add(newCard);
            }
        }

        private void ShuffleList(List<Sprite> selectedImages)
        {
            for (int i = selectedImages.Count - 1; i > 0; i--)
            {
                int randomIndex = Random.Range(0, i + 1);
                (selectedImages[i], selectedImages[randomIndex]) = (selectedImages[randomIndex], selectedImages[i]);
            }
        }

        public void SetCardsActiveState(bool isActive)
        {
            foreach (var card in _cards)
            {
                card.SetActiveState(isActive);
            }
        }

        private void AdjustGridLayout()
        {
            int totalCards = _pairs * 2;
            int columns = Mathf.CeilToInt(Mathf.Sqrt(totalCards));
            int rows = Mathf.CeilToInt((float)totalCards / columns);

            float parentWidth = ((RectTransform)_cardsContainer).rect.width;
            float parentHeight = ((RectTransform)_cardsContainer).rect.height;

            float availableWidth = parentWidth - _memoryConfig.SpaceBetweenCards * (columns - 1);
            float availableHeight = parentHeight - _memoryConfig.SpaceBetweenCards * (rows - 1);

            float cellSize = Mathf.Min(availableWidth / columns, availableHeight / rows);

            _grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            _grid.constraintCount = columns;
            _grid.cellSize = new Vector2(cellSize * _memoryConfig.CardScale, cellSize * _memoryConfig.CardScale);
            _grid.spacing = new Vector2(_memoryConfig.SpaceBetweenCards, _memoryConfig.SpaceBetweenCards);
        }
        
        private void OnCardSelected(Card card)
        {
            if (_firstCard == null)
            {
                _firstCard = card;
                _firstCard.SetActiveState(false);
            }
            else if (_secondCard == null && card != _firstCard)
            {
                _secondCard = card;
                _secondCard.SetActiveState(false);
                CheckMatch();
            }
        }
        
        private async UniTaskVoid CheckMatch()
        {
            var delay = 1f;
            SetCardsActiveState(false);

            await UniTask.Delay(TimeSpan.FromSeconds(0.3f));
        
            if (IsMatch(_firstCard, _secondCard))
            {
                _pairsFoundCount++;
                _cards.Remove(_firstCard);
                _cards.Remove(_secondCard);

                if (_pairsFoundCount == _pairs)
                {
                    await UniTask.Delay(TimeSpan.FromSeconds(delay));
                    AllPairsFound?.Invoke();
                    return;
                }
            }
            else
            {
                await UniTask.Delay(TimeSpan.FromSeconds(delay));
                
                _firstCard.Hide();
                _secondCard.Hide();
            }
            
            SetCardsActiveState(true);
        
            _firstCard = null;
            _secondCard = null;
        }

        private bool IsMatch(Card card1, Card card2)
        {
            return card1.FrontImage == card2.FrontImage;
        }

        public void OnDestroy()
        {
            UnsubscribeOnCardsEvents();
        }
    }
}