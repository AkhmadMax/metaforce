using Core.Interfaces;
using Core.Models;
using DefaultNamespace;
using Presenters;
using Settings;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;
using VContainer.Unity;

namespace Core
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
            
            builder.Register<TargetFinder>(Lifetime.Singleton);
            builder.RegisterEntryPoint<EnemySpawnService>().AsSelf();
            
            builder.RegisterEntryPoint<PlayerAttackPresenter>();

            
        }
    }
}