using Metaforce.Core;
using Metaforce.Enemy;
using Metaforce.Player;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;
using VContainer.Unity;

namespace Metaforce
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private InputActionAsset inputAction;
        [SerializeField] private PlayerConfig playerConfig;
        [SerializeField] private EnemiesConfig enemiesConfig;

        protected override void Configure(IContainerBuilder builder)
        {
            // Configs
            builder.RegisterInstance(playerConfig);
            builder.RegisterInstance(enemiesConfig);

            // Core Services
            builder.Register<InputActionsReference>(Lifetime.Singleton)
                .WithParameter(inputAction);
            builder.RegisterEntryPoint<InputProvider>()
                .As<IInputProvider>();
            builder.Register<ScoreService>(Lifetime.Singleton)
                .As<IScoreService>();

            // Player
            builder.RegisterComponentInHierarchy<PlayerView>();
            builder.Register<PlayerModel>(Lifetime.Singleton);
            builder.RegisterEntryPoint<PlayerMovementPresenter>();
            builder.RegisterEntryPoint<PlayerAttackPresenter>();

            // Enemy
            builder.Register<EnemiesFinder>(Lifetime.Singleton);
            builder.RegisterEntryPoint<EnemySpawnService>()
                .As<IEnemyRegistry>()
                .AsSelf();
            builder.RegisterFactory<Vector3, EnemyPresenter>(container =>
            {
                var config = container.Resolve<EnemiesConfig>();

                return position =>
                {
                    var view = Instantiate(config.Prefab, position, Quaternion.identity);
                    container.InjectGameObject(view.gameObject);
                    var model = new EnemyModel(config.Health);
                    return new EnemyPresenter(model, view, config);
                };
            }, Lifetime.Singleton);
        }
    }
}