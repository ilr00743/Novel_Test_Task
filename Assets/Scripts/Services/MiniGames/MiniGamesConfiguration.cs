using Naninovel;

namespace Services.MiniGames
{
    [EditInProjectSettings]
    public class MiniGamesConfiguration : Configuration
    {
        public const string DefaultPathPrefix = "MiniGames";

        public readonly string PathPrefix = "Naninovel/MiniGames";

        public ResourceLoaderConfiguration Loader = new ResourceLoaderConfiguration { PathPrefix = DefaultPathPrefix };
    }
}