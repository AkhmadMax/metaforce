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
            builder.RegisterInstance(playerConfig);
            builder.RegisterInstance(enemiesConfig);
            builder.Register<ScoreService>(Lifetime.Singleton)
                .As<IScoreService>();

            
            builder.Register<InputActionsReference>(Lifetime.Singleton)
                .WithParameter(inputAction);

            builder.RegisterEntryPoint<InputProvider>()
                .As<IInputProvider>();

            builder.RegisterComponentInHierarchy<PlayerView>();
            builder.Register<PlayerModel>(Lifetime.Singleton);
            builder.RegisterEntryPoint<PlayerMovementPresenter>();
            
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
            
            builder.Register<EnemiesFinder>(Lifetime.Singleton);
            builder.RegisterEntryPoint<EnemySpawnService>()
                .As<IEnemyRegistry>()
                .AsSelf();
            
            builder.RegisterEntryPoint<PlayerAttackPresenter>();


        }
    }
}