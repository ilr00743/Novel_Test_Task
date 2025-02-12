using System;
using UnityEngine;

namespace Services.MiniGames.Implementations
{
    public class MemoryCards : MiniGame
    {
        public override string Name { get; } = "Memory Cards";

        private void OnEnable()
        {
            InitializeGame();
        }

        protected override void InitializeGame()
        {
            Debug.Log("MemoryCards initialized");
        }

        protected override void EndGame()
        {
            throw new System.NotImplementedException();
        }
    }
}