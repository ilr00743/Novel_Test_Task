using Naninovel;

namespace Services.QuestLog
{
    public interface IQuestLogService : IStatefulService<GameStateMap>
    {
        QuestLogState State { get; }
        void SetTitle(string title);
        void AddQuest(string id, string description);
        void RemoveQuest(string id);
    }
}