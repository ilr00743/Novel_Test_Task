using System.Collections.Generic;
using System.Linq;
using Naninovel;
using UI.QuestLog;
using UnityEngine;

namespace Services.QuestLog
{
    [InitializeAtRuntime]
    public class QuestLogService : IStatefulService<GameStateMap>
    {
        private IUIManager _uiManager;
        private IStateManager _stateManager;
        public QuestLogState State { get; private set; } = new QuestLogState();

        public UniTask InitializeServiceAsync()
        {
            _uiManager = Engine.GetService<IUIManager>();
            _stateManager = Engine.GetService<IStateManager>();
            return UniTask.CompletedTask;
        }

        public void ResetService()
        {
            _uiManager.GetUI<QuestLogUI>().RemoveAllQuests();
            State.ClearQuests();
        }

        public void DestroyService() { }

        public void SaveServiceState(GameStateMap state)
        {
            state.SetState(State);
        }

        public UniTask LoadServiceStateAsync(GameStateMap state)
        {
            State = state.GetState<QuestLogState>();
            
            if (State.Quests.Count == 0) return UniTask.CompletedTask;

            if (_stateManager.RollbackInProgress) return UniTask.CompletedTask;

            var questLogUI = _uiManager.GetUI<QuestLogUI>();
            
            foreach (var quest in State.Quests)
            {
                questLogUI.AddQuest(quest.Id, quest.Description);
            }
            
            SetTitle(State.Title);
            
            return UniTask.CompletedTask;
        }

        public void SetTitle(string title)
        {
            _uiManager.GetUI<QuestLogUI>().SetTitle(title);
            State.Title = title;
        }
        
        public void AddQuest(string id, string description)
        {
            if (State.Quests.Any(quest => quest.Id == id))
            {
                Debug.LogWarning($"<color=red>[Quest Log Service]</color><color=cyan>[Add Quest]</color>Quest {id} already exists");
                return;
            }
            
            State.AddQuest(id, description);
            
            _uiManager.GetUI<QuestLogUI>().AddQuest(id, description);
        }

        public void RemoveQuest(string id)
        {
            if (State.Quests.All(quest => quest.Id != id))
            {
                Debug.LogError("<color=red>[Quest Log Service]</color><color=cyan>[Removing Quest]</color>Quest not found: " + id);
                return;
            }
            
            _uiManager.GetUI<QuestLogUI>().RemoveQuest(id);
            State.RemoveQuest(id);
        }
    }
}