using System;
using Services.MiniGames.Configs;
using UnityEngine;

namespace Services.MiniGames
{
    public abstract class MiniGame : MonoBehaviour
    {
        public event Action Completed;
        public abstract string Name { get; }

        protected virtual void InitializeGame() { }

        protected virtual void EndGame()
        {
            Completed?.Invoke();
        }
    }
}