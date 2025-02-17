using DG.Tweening;
using Naninovel;
using UnityEngine;

namespace Services.MiniGames.Implementations.MemoryCards
{
    public class MemoryCards : MiniGame
    {
        [SerializeField] private Board _board;
        private Canvas _canvas;
        private CanvasGroup _canvasGroup;
        
        public override string Name { get; } = "Memory Cards";
        
        private void OnEnable()
        {
            InitializeGame();
        }
        
        protected override void InitializeGame()
        {
            _canvas = GetComponent<Canvas>();
            _canvasGroup = GetComponent<CanvasGroup>();
            
            _canvasGroup.DOFade(1, 0.3f);
            _canvas.worldCamera = Engine.GetService<ICameraManager>().UICamera;
            
            _board.InitializeBoard();
            _board.AllPairsFound += EndGame;
            
            Debug.Log("MemoryCards initialized");
        }

        protected override void EndGame()
        {
            _board.AllPairsFound -= EndGame;
            Debug.Log("MemoryCards completed");
            _canvasGroup.DOFade(0, 0.3f).OnComplete(() => base.EndGame());
        }
    }
}