****TBD****
<img width="1631" height="907" alt="image" src="https://github.com/user-attachments/assets/ec5c9367-e23e-4bb0-99d0-45acce038303" />


Unity Version: 2022.3.62f2

**Comments**

1. Currently we check distance for all the alive enemies, choose the closest and attack. Alternatively it can be implemented by using a Trigger volume around the Player, including using reactivity here.
2. Laser in the PlayerView is currently doesn't represent Player attack speed, Ememy tracking mechanics can be improved
3. Bootstraper scene can be introduced in future to instantiate prefabs instead of having them in the main scene

**Project structure**

**Packages:** 
UniRx,
VContainer, 
Input System, 
ProBuilder, 
TextMeshPro

```
● Assets/                                                                                                                                                                                                                                        
  ├── Configs/                                                                                                                                                                                                                                                                                                     
  │   ├── PlayerInputActions.cs                                                                                                                                                                                                                                                                                    
  │   └── PlayerInputActions.inputactions                                                                                                                                                                                                                                                                          
  ├── Prefabs/                                                                                                                                                                                                                                                                                                     
  │   ├── Enemy.prefab                                                                                                                                                                                                                                                                                             
  │   ├── GameLifetimeScope.prefab                                                                                                                                                                                                                                                                                 
  │   ├── Player.prefab                                                                                                                                                                                                                                                                                            
  │   └── UI.prefab
  ├── Scenes/
  │   └── Main.unity
  ├── Scripts/
  │   ├── GameLifetimeScope.cs
  │   ├── Core/
  │   │   ├── Core.asmdef
  │   │   ├── Input/
  │   │   │   ├── InputActionsReference.cs
  │   │   │   └── InputProvider.cs
  │   │   ├── Interfaces/
  │   │   │   ├── IDamageable.cs
  │   │   │   ├── IEnemy.cs
  │   │   │   ├── IEnemyRegistry.cs
  │   │   │   ├── IInputProvider.cs
  │   │   │   ├── IScoreService.cs
  │   │   │   └── ITargetable.cs
  │   │   └── Services/
  │   │       ├── EnemiesFinder.cs
  │   │       └── ScoreService.cs
  │   ├── Enemy/
  │   │   ├── Enemy.asmdef
  │   │   ├── EnemiesConfig.cs
  │   │   ├── EnemyModel.cs
  │   │   ├── EnemyPresenter.cs
  │   │   ├── EnemyRegistry.cs
  │   │   ├── EnemySpawnService.cs
  │   │   └── EnemyView.cs
  │   ├── Player/
  │   │   ├── Player.asmdef
  │   │   ├── PlayerAttackPresenter.cs
  │   │   ├── PlayerConfig.cs
  │   │   ├── PlayerModel.cs
  │   │   ├── PlayerMovementPresenter.cs
  │   │   └── PlayerView.cs
  │   └── UI/
  │       └── GameUI.cs
  ├── Settings/
  │   ├── EnemiesConfig.asset
  │   └── PlayerConfig.asset
  └── Thirdparty/
      ├── ProBuilder Data/
      └── TextMesh Pro/

```
