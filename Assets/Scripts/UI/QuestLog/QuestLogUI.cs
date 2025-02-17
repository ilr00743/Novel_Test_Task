using System.Collections.Generic;
using Naninovel.UI;
using Services.QuestLog;
using TMPro;
using UnityEngine;

namespace UI.QuestLog
{
    public class QuestLogUI : CustomUI
    {
        [SerializeField] private TextMeshProUGUI _title;
        [SerializeField] private Transform _questsContainer;
        [SerializeField] private QuestItem _questItemPrefab;
        private List<QuestItem> _quests = new List<QuestItem>();

        public void SetTitle(string title)
        {
            _title.text = title;
        }
        
        public void AddQuest(string id, string description)
        {
            var questItem = Instantiate(_questItemPrefab, _questsContainer);
            questItem.Initialize(id, $"- {description}");
            _quests.Add(questItem);
        }

        public void RemoveQuest(string id)
        {
            var quest = _quests.Find(quest => quest.Id == id);
            
            Destroy(quest.gameObject);
            _quests.Remove(quest);
        }

        public void RemoveAllQuests()
        {
            if (_quests.Count == 0) return;
                
            _quests.ForEach(quest => Destroy(quest.gameObject));
            _quests.Clear();
        }
        
        [ContextMenu("Add Test Quest")]
        public void AddTestQuest()
        {
            var questItem = Instantiate(_questItemPrefab, _questsContainer);
            questItem.Initialize("Episode_1_Quest_1", "- Go to somewhere and take smth");
        }
    }
}