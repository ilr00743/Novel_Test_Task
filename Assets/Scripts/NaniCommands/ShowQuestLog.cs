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

        private IUIManager _uiManager;
        private QuestLogService _questLogService;

        public override UniTask ExecuteAsync(AsyncToken asyncToken = default)
        {
            _uiManager = Engine.GetService<IUIManager>();
            _questLogService = Engine.GetService<QuestLogService>();
            
            _questLogService.SetTitle(Title);
            
            return base.ExecuteAsync(asyncToken);
        }
    }
}