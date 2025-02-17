namespace Naninovel.UI
{
    public class PausePanelContinueButton : ScriptableButton
    {
        private IUIManager _uiManager;
        protected override void Awake()
        {
            base.Awake();
            _uiManager = Engine.GetService<IUIManager>();
        }

        protected override void OnButtonClick()
        {
            base.OnButtonClick();
            _uiManager.GetUI<IPauseUI>().Hide();
        }
    }
}