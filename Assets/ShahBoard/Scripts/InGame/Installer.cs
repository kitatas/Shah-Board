using ShahBoard.InGame.Data.Container;
using ShahBoard.InGame.Data.DataStore;
using ShahBoard.InGame.Data.Entity;
using ShahBoard.InGame.Domain.Factory;
using ShahBoard.InGame.Domain.Repository;
using ShahBoard.InGame.Domain.UseCase;
using ShahBoard.InGame.Presentation.Controller;
using ShahBoard.InGame.Presentation.Presenter;
using ShahBoard.InGame.Presentation.View;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ShahBoard.InGame
{
    public sealed class Installer : LifetimeScope
    {
        [SerializeField] private Camera mainCamera = default;
        [SerializeField] private Transform board = default;

        [SerializeField] private BoardData boardData = default;
        [SerializeField] private PieceTable pieceTable = default;

        [SerializeField] private EditView editView = default;
        [SerializeField] private MaskView maskView = default;
        [SerializeField] private MatchingStateView matchingStateView = default;
        [SerializeField] private NextButtonView nextButtonView = default;
        [SerializeField] private TurnView turnView = default;
        [SerializeField] private WinnerView winnerView = default;

        protected override void Configure(IContainerBuilder builder)
        {
            // Container
            builder.Register<BoardPlacementContainer>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<PieceContainer>(Lifetime.Scoped).AsImplementedInterfaces();

            // DataStore
            builder.RegisterInstance<BoardData>(boardData);
            builder.RegisterInstance<PieceTable>(pieceTable);

            // Entity
            builder.Register<GameStateEntity>(Lifetime.Scoped);
            builder.Register<MatchingStateEntity>(Lifetime.Scoped);
            builder.Register<PlayerStatusEntity>(Lifetime.Scoped);
            builder.Register<TurnEntity>(Lifetime.Scoped);

            // Factory
            builder.Register<BoardFactory>(Lifetime.Scoped);
            builder.Register<PieceFactory>(Lifetime.Scoped);

            // Repository
            builder.Register<BoardRepository>(Lifetime.Scoped);
            builder.Register<PieceRepository>(Lifetime.Scoped);

            // UseCase
            builder.Register<ContainerUseCase>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<EditUseCase>(Lifetime.Scoped);
            builder.Register<GameStateUseCase>(Lifetime.Scoped);
            builder.Register<InputUseCase>(Lifetime.Scoped);
            builder.Register<MatchingStateUseCase>(Lifetime.Scoped);
            builder.Register<MovementUseCase>(Lifetime.Scoped);
            builder.Register<PieceDataUseCase>(Lifetime.Scoped);
            builder.Register<PlayerStatusUseCase>(Lifetime.Scoped);
            builder.Register<SelectUseCase>(Lifetime.Scoped);
            builder.Register<TurnUseCase>(Lifetime.Scoped);
            builder.RegisterEntryPoint<StageDataUseCase>(Lifetime.Scoped);

            // Controller
            builder.Register<BattleState>(Lifetime.Scoped);
            builder.Register<EditState>(Lifetime.Scoped);
            builder.Register<JudgeState>(Lifetime.Scoped);
            // TODO: ローカルとオンラインで分ける
            {
                builder.Register<LocalMatchState>(Lifetime.Scoped).As<MatchState>();
            }
            builder.Register<ResultState>(Lifetime.Scoped);
            builder.Register<SelectState>(Lifetime.Scoped);
            builder.Register<StateController>(Lifetime.Scoped);

            // Presenter
            builder.RegisterEntryPoint<MatchingPresenter>(Lifetime.Scoped);
            builder.RegisterEntryPoint<StatePresenter>(Lifetime.Scoped);

            // View
            builder.RegisterInstance<EditView>(editView);
            builder.RegisterInstance<MaskView>(maskView);
            builder.RegisterInstance<MatchingStateView>(matchingStateView);
            builder.RegisterInstance<NextButtonView>(nextButtonView);
            builder.RegisterInstance<TurnView>(turnView);
            builder.RegisterInstance<WinnerView>(winnerView);

            // Other
            builder.RegisterInstance<Camera>(mainCamera);
            builder.RegisterInstance<Transform>(board);
        }
    }
}