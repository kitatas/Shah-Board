using System;
using System.Collections.Generic;

namespace ShahBoard.Common.Data.Entity
{
    public sealed class LanguageEntity
    {
        public LanguageType type;
        public LanguageData data;
    }

    [Serializable]
    public sealed class LanguageData
    {
        public List<PieceData> pieceData;
    }

    [Serializable]
    public sealed class PieceData
    {
        public PieceType type;
        public string name;
    }
}