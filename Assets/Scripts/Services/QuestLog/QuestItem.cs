using TMPro;
using UnityEngine;

namespace Services.QuestLog
{
    public class QuestItem : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _description;

        public string Id {get; private set; }
        public string Description {get; private set; }

        public void Initialize(string id, string description)
        {
            Id = id;
            Description = description;
            _description.text = Description;
        }
    }
}