﻿using Naninovel;
using Services.MiniGames;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace NaniCommands
{
    [CommandAlias("miniGame")]
    public class MiniGameCommand : Command
    {
        [ParameterAlias(NamelessParameterAlias), ResourceContext(MiniGamesConfiguration.DefaultPathPrefix)]
        public StringParameter Name;

        private MiniGamesService _miniGamesService;
        
        public override async UniTask ExecuteAsync(AsyncToken asyncToken = default)
        {
            if (!Assigned(Name)) return;

            _miniGamesService = Engine.GetService<MiniGamesService>();
            
            var resource = await _miniGamesService.LoadAsync(Name);
            
            if (resource == null)
            {
                Debug.LogError($"<color=red>[Mini Games Service]</color> Can't load {Name}");
                return;
            }
            
            Debug.Log($"<color=red>[Mini Games Service]</color> {Name} is loaded");
            
            _miniGamesService.InstantiateMiniGame(resource);
            await Engine.GetService<IStateManager>().SaveGlobalAsync();
        }
    }
}