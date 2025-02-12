using UnityEngine;

namespace Services.MiniGames
{
    public abstract class MiniGame : MonoBehaviour
    {
        public abstract string Name { get; }
        protected abstract void InitializeGame();
        protected abstract void EndGame();
    }
}