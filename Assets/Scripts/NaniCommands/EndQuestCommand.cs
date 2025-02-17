using Naninovel;
using Services.QuestLog;

namespace NaniCommands
{
    [CommandAlias("endQuest")]
    public class EndQuestCommand : Command
    {
        [ParameterAlias("id"), RequiredParameter]
        public StringParameter QuestId;

        private IQuestLogService _questLogService;
        
        public override UniTask ExecuteAsync(AsyncToken asyncToken = default)
        {
            _questLogService = Engine.GetService<IQuestLogService>();
            
            _questLogService.RemoveQuest(QuestId);
            return UniTask.CompletedTask;
        }
    }
}