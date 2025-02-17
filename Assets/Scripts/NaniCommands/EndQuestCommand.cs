using Naninovel;
using Services.QuestLog;

namespace NaniCommands
{
    [CommandAlias("endQuest")]
    public class EndQuestCommand : Command
    {
        [ParameterAlias("id"), RequiredParameter]
        public StringParameter QuestId;

        private QuestLogService _questLogService;
        
        public override UniTask ExecuteAsync(AsyncToken asyncToken = default)
        {
            _questLogService = Engine.GetService<QuestLogService>();
            
            _questLogService.RemoveQuest(QuestId);
            return UniTask.CompletedTask;
        }
    }
}