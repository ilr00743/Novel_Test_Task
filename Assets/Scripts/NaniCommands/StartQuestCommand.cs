using Naninovel;
using Services.QuestLog;

namespace NaniCommands
{
    [CommandAlias("startQuest")]
    public class StartQuestCommand : Command
    {
        [ParameterAlias("id"), RequiredParameter]
        public StringParameter QuestId;
        
        [ParameterAlias("description"), RequiredParameter]
        public StringParameter QuestDescription;

        private QuestLogService _questLogService;
        
        public override UniTask ExecuteAsync(AsyncToken asyncToken = default)
        {
            _questLogService = Engine.GetService<QuestLogService>();
            
            _questLogService.AddQuest(QuestId, QuestDescription);
            return UniTask.CompletedTask;
        }
    }
}