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

    public enum PieceType
    {
        None,
        Emperor,    // 皇帝
        Fool,       // 愚者
        Magician,   // 魔術師
        Priestess,  // 女教皇
        Empress,    // 女帝
        Hierophant, // 教皇
        Lovers,     // 恋人
        Chariot,    // 戦車
        Justice,    // 正義
        Hermit,     // 隠者
        Wheel,      // 運命の輪
        Strength,   // 力
        HangedMan,  // 吊された男
        Death,      // 死神
        Temperance, // 節制
        Devil,      // 悪魔
        Tower,      // 塔
        Star,       // 星
        Moon,       // 月
        Sun,        // 太陽
        Judgement,  // 正義
        World,      // 世界
    }

    public enum PieceStatus
    {
        None,
        InDeck,
    }

    #endregion
}