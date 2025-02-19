using Naninovel;
using UnityEngine;

public class PhoneExitButton : MonoBehaviour
{
    private IScriptPlayer _scriptPlayer;

    private void Start()
    {
        _scriptPlayer = Engine.GetService<IScriptPlayer>();
    }

    private void OnMouseUpAsButton()
    {
        _scriptPlayer.Play(_scriptPlayer.PlayedIndex + 1);

    }
}
