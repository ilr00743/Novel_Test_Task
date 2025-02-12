using System;
using Naninovel;
using Services.MiniGames;
using UnityEngine;

namespace Editor.NaniConfigs
{
    public class MiniGamesSettings : ResourcefulSettings<MiniGamesConfiguration>
    {
        protected override Type ResourcesTypeConstraint => typeof(MiniGame);
        protected override string ResourcesCategoryId => Configuration.Loader.PathPrefix;
    }
}