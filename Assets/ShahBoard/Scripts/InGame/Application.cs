namespace ShahBoard.InGame
{
    #region Const

    public sealed class BoardConfig
    {
        public const int VERTICAL = 7;
        public const int HORIZONTAL = 7;
    }

    #endregion

    #region Enum

    public enum GameState
    {
        None,
        Match,
        Edit,
        Input,
        Move,
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
        Input,
    }

    public enum PieceType
    {
        None,
        Emperor,
        Fool,
    }

    public enum PieceStatus
    {
        None,
        InDeck,
    }

    #endregion
}