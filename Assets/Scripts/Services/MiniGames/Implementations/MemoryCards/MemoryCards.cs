using UnityEngine;

namespace Services.MiniGames.Implementations.MemoryCards
{
    public class MemoryCards : MiniGame
    {
        [SerializeField] private Board _board;
        
        public override string Name { get; } = "Memory Cards";
        
        protected override void OnEnable()
        {
            base.OnEnable();
            InitializeGame();
        }
        
        protected override void InitializeGame()
        {
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