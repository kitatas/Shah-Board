namespace ShahBoard.InGame
{
    #region Const



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
        None,
        Master,
        Client,
    }

    #endregion
}