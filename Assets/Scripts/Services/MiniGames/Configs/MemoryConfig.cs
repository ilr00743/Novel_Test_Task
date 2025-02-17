using UnityEngine;

namespace Services.MiniGames.Configs
{
    [CreateAssetMenu(fileName = "MemoryConfig", menuName = "Mini Games Configs/Memory Config", order = 0)]
    public class MemoryConfig : MiniGameConfig
    {
        [SerializeField, Range(0f, 40f)] private float _spaceBetweenCards;
        [SerializeField, Range(0.5f, 1.5f)] private float _cardScale;
        
        public float SpaceBetweenCards => _spaceBetweenCards;
        public float CardScale => _cardScale;
    }
}