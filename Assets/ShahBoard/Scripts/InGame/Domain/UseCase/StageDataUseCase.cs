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
                    boardFactory.GeneratePlacementObject(boardRepository.GetPlacement(), position);

                    // 王の配置
                    if (j == 3)
                    {
                        if (i == 0)
                        {
                            pieceFactory.Generate(pieceRepository.FindPiece(PieceType.King), position, PlayerType.Master);
                        }
                        else if (i == BoardConfig.VERTICAL - 1)
                        {
                            pieceFactory.Generate(pieceRepository.FindPiece(PieceType.King), position, PlayerType.Client);
                        }
                    }
                }
            }

            // 自分の駒
            var index = 1;
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    var position = new Vector3(j - 2.0f, 0.0f, -(i + 4.5f));
                    pieceFactory.Generate(pieceRepository.GetPiece(index++), position, PlayerType.Master);
                }
            }

            // 相手の駒
            index = 1;
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    var position = new Vector3(j - 2.0f, 0.0f, i + 4.5f);
                    pieceFactory.Generate(pieceRepository.GetPiece(index++), position, PlayerType.Client);
                }
            }
        }
    }
}