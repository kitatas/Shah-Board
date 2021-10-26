using ShahBoard.InGame.Domain.Factory;
using ShahBoard.InGame.Domain.Repository;
using UnityEngine;
using VContainer.Unity;

namespace ShahBoard.InGame.Domain.UseCase
{
    public sealed class StageDataUseCase : IPostInitializable
    {
        private readonly BoardFactory _boardFactory;
        private readonly BoardRepository _boardRepository;
        private readonly PieceFactory _pieceFactory;
        private readonly PieceRepository _pieceRepository;

        public StageDataUseCase(BoardFactory boardFactory, BoardRepository boardRepository,
            PieceFactory pieceFactory, PieceRepository pieceRepository)
        {
            _boardFactory = boardFactory;
            _boardRepository = boardRepository;
            _pieceFactory = pieceFactory;
            _pieceRepository = pieceRepository;
        }

        public void PostInitialize()
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
                            0 => _pieceFactory.Generate(_pieceRepository.FindPiece(PieceType.Emperor), position, PlayerType.Master),
                            BoardConfig.VERTICAL - 1 => _pieceFactory.Generate(_pieceRepository.FindPiece(PieceType.Emperor), position, PlayerType.Client),
                            _ => null
                        };

                        _boardFactory.GeneratePlacementObject(_boardRepository.GetPlacement(), position, PlayerType.None, pieceView);
                        continue;
                    }

                    // Masterの初期配置可能マスの設定
                    if (i == 0)
                    {
                        _boardFactory.GeneratePlacementObject(_boardRepository.GetPlacement(), position, PlayerType.Master);
                        continue;
                    }

                    // Clientの初期配置可能マスの設定
                    if (i == BoardConfig.VERTICAL - 1)
                    {
                        _boardFactory.GeneratePlacementObject(_boardRepository.GetPlacement(), position, PlayerType.Client);
                        continue;
                    }

                    _boardFactory.GeneratePlacementObject(_boardRepository.GetPlacement(), position, PlayerType.None);
                }
            }

            CreatePiece(PlayerType.Master);
            CreatePiece(PlayerType.Client);
        }

        private void CreatePiece(PlayerType playerType)
        {
            var sign = playerType switch
            {
                PlayerType.Master => -1,
                PlayerType.Client => 1,
                _ => 0
            };

            var index = PieceType.Fool;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    var position = new Vector3(j - 2.0f, 0.0f, (i + 4.5f) * sign);
                    _pieceFactory.Generate(_pieceRepository.FindPiece(index++), position, playerType);
                }
            }
        }
    }
}