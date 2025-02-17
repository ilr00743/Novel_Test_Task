using System;
using System.Collections.Generic;
using UnityEngine;

namespace Services.QuestLog
{
    [Serializable]
    public class QuestData
    {
        public string Id;
        public string Description;
    }
    
    [Serializable]
    public class QuestLogState
    {
        [SerializeField] private List<QuestData> _quests = new();
        public string Title;
        public IReadOnlyList<QuestData> Quests => _quests;

        public void AddQuest(string id, string description)
        {
            _quests.Add(new QuestData() { Id = id, Description = description });
        }

        public void ClearQuests()
        {
            _quests.Clear();
        }

        public void RemoveQuest(string id)
        {
            _quests.RemoveAll(quest => quest.Id == id);
        }
    }
}