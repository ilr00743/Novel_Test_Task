using Naninovel;
using Naninovel.Commands;
using Services.QuestLog;

namespace NaniCommands
{
    [CommandAlias("showQuestLog")]
    public class ShowQuestLog : ShowUI
    {
        [RequiredParameter, ParameterAlias("title")]
        public StringParameter Title;

        private IQuestLogService _questLogService;

        public override UniTask ExecuteAsync(AsyncToken asyncToken = default)
        {
            _questLogService = Engine.GetService<IQuestLogService>();
            
            _questLogService.SetTitle(Title);
            
            return base.ExecuteAsync(asyncToken);
        }
    }
}