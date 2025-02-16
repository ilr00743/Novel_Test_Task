using Naninovel;
using UnityEngine;

namespace Services.MiniGames.Implementations.MemoryCards
{
    public class MemoryCards : MiniGame
    {
        [SerializeField] private Board _board;
        private Canvas _canvas;
        
        public override string Name { get; } = "Memory Cards";
        
        private void OnEnable()
        {
            InitializeGame();
        }
        
        protected override void InitializeGame()
        {
            _canvas = GetComponent<Canvas>();
            _canvas.worldCamera = Engine.GetService<ICameraManager>().UICamera;
            
            _board.InitializeBoard();
            _board.AllPairsFound += EndGame;
            
            Debug.Log("MemoryCards initialized");
        }

        protected override void EndGame()
        {
            _board.AllPairsFound -= EndGame;
            
            Debug.Log("MemoryCards completed");
            base.EndGame();
        }
    }
}