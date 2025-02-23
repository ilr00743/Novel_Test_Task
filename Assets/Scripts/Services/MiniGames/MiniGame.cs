using System;
using Naninovel.UI;

namespace Services.MiniGames
{
    public abstract class MiniGame : CustomUI
    {
        public event Action Completed;
        public abstract string Name { get; }

        protected virtual void InitializeGame() { }

        protected virtual void EndGame()
        {
            Completed?.Invoke();
            Hide();
        }
    }
}