namespace ShahBoard.InGame
{
    #region Const

    public sealed class BoardConfig
    {
        public const int VERTICAL = 7;
        public const int HORIZONTAL = 7;
    }

    public sealed class DeckConfig
    {
        public const int INIT_PIECE_COUNT = 1;
        public const int MAX_PIECE_COUNT = 7;
    }

    public sealed class PieceConfig
    {
        public const float EDIT_HEIGHT = 5.0f;
    }

    public sealed class UiConfig
    {
        public const float TWEEN_TIME = 0.25f;
    }

    #endregion

    #region Enum

    public enum GameState
    {
        None,
        Match,
        Edit,
        Select,
        Battle,
        Judge,
        Result,
    }

    public enum MatchingState
    {
        None,
        Connect,
        Matching,
        Matched,
        Ready,
        Disconnect,
    }

    public enum PlayerType
    {
        None = 0,
        Master = 1,
        Client = 2,
    }

    public enum PlacementType
    {
        None,
        Valid,
        Invalid,
        Select,
    }

    public enum PieceStatus
    {
        None,
        InDeck,
    }

    #endregion
}