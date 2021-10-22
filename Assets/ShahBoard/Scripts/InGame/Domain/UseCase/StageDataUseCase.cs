using ShahBoard.InGame.Domain.Factory;
using ShahBoard.InGame.Domain.Repository;
using UnityEngine;

namespace ShahBoard.InGame.Domain.UseCase
{
    public sealed class StageDataUseCase
    {
        public StageDataUseCase(BoardFactory boardFactory, BoardRepository boardRepository,
            PieceFactory pieceFactory, PieceRepository pieceRepository)
        {
            for (int i = 0; i < BoardConfig.VERTICAL; i++)
            {
                for (int j = 0; j < BoardConfig.HORIZONTAL; j++)
                {
                    var position = new Vector3(j - 3.0f, 0.0f, i - 3.0f);

                    // 皇帝の配置
                    if (j == 3)
                    {
                        var pieceView = i switch
                        {
                            0 => pieceFactory.Generate(pieceRepository.FindPiece(PieceType.Emperor), position, PlayerType.Master),
                            BoardConfig.VERTICAL - 1 => pieceFactory.Generate(pieceRepository.FindPiece(PieceType.Emperor), position, PlayerType.Client),
                            _ => null
                        };

                        boardFactory.GeneratePlacementObject(boardRepository.GetPlacement(), position, PlayerType.None, pieceView);
                        continue;
                    }

                    // Masterの初期配置可能マスの設定
                    if (i == 0)
                    {
                        boardFactory.GeneratePlacementObject(boardRepository.GetPlacement(), position, PlayerType.Master);
                        continue;
                    }

                    // Clientの初期配置可能マスの設定
                    if (i == BoardConfig.VERTICAL - 1)
                    {
                        boardFactory.GeneratePlacementObject(boardRepository.GetPlacement(), position, PlayerType.Client);
                        continue;
                    }

                    boardFactory.GeneratePlacementObject(boardRepository.GetPlacement(), position, PlayerType.None);
                }
            }

            // Masterの駒
            var index = PieceType.Fool;
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    var position = new Vector3(j - 2.0f, 0.0f, -(i + 4.5f));
                    pieceFactory.Generate(pieceRepository.FindPiece(index++), position, PlayerType.Master);
                }
            }

            // Clientの駒
            index = PieceType.Fool;
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    var position = new Vector3(j - 2.0f, 0.0f, i + 4.5f);
                    pieceFactory.Generate(pieceRepository.FindPiece(index++), position, PlayerType.Client);
                }
            }
        }
    }
}