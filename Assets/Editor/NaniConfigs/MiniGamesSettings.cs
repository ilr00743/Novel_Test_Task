using System;
using Naninovel;
using Services.MiniGames;
using UnityEditor;
using UnityEngine;

namespace Editor.NaniConfigs
{
    public class MiniGamesSettings : ResourcefulSettings<MiniGamesConfiguration>
    {
        protected override Type ResourcesTypeConstraint => typeof(GameObject);
        protected override string ResourcesCategoryId => Configuration.Loader.PathPrefix;
        
        [MenuItem("Naninovel/Resources/Mini Games")]
        private static void OpenResourcesWindow () => OpenResourcesWindowImpl();
    }
}