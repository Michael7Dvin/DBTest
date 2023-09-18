﻿using CodeBase.Gameplay.Services.Randomise;
using CodeBase.Gameplay.Services.TurnQueue;
using CodeBase.Gameplay.UI.TurnQueue;
using CodeBase.Infrastructure.Addressable;
using CodeBase.Infrastructure.Configs.Character;
using CodeBase.Infrastructure.GameFSM.FSM;
using CodeBase.Infrastructure.GameFSM.States;
using CodeBase.Infrastructure.Services.Factories.Common;
using CodeBase.Infrastructure.Services.Factories.TurnQueue;
using CodeBase.Infrastructure.Services.Logging;
using CodeBase.Infrastructure.Services.Providers.UIProvider;
using CodeBase.Infrastructure.Services.SceneLoading;
using CodeBase.Infrastructure.Services.UnitsProvider;
using CodeBase.Infrastructure.StaticDataProviding;
using Infrastructure.Services.ResourcesLoading;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace CodeBase.Infrastructure.Installers
{
    public class ProjectInstaller : LifetimeScope
    {
        [SerializeField] private AllStaticData _allStaticData;
       
        
        protected override void Configure(IContainerBuilder builder)
        {
            RegisterStateMachine(builder);
            RegisterServices(builder);
            RegisterFactories(builder);
        }

        private void RegisterFactories(IContainerBuilder builder)
        {
            builder.Register<ICommonUIFactory, CommonUIFactory>(Lifetime.Singleton);
            builder.Register<ITurnQueueViewFactory, TurnQueueViewFactory>(Lifetime.Singleton);
        }

        private void RegisterStateMachine(IContainerBuilder builder)
        {
            builder.Register<Bootstrapper>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<IGameStateMachine, GameStateMachine>(Lifetime.Singleton);
            builder.Register<InitializationState>(Lifetime.Singleton);
            builder.Register<GameplayState>(Lifetime.Singleton);
        }

        private void RegisterServices(IContainerBuilder builder)
        {
            builder
                .Register<IStaticDataProvider, StaticDataProvider>(Lifetime.Singleton)
                .WithParameter(_allStaticData);
            
            builder.Register<ICustomLogger, CustomLogger>(Lifetime.Singleton);
            builder.Register<ISceneLoader, SceneLoader>(Lifetime.Singleton);
            builder.Register<IAddressablesLoader, AddressablesLoader>(Lifetime.Singleton);
            builder.Register<ILogWriter, LogWriter>(Lifetime.Singleton);
            builder.Register<ITurnQueue, TurnQueue>(Lifetime.Singleton);
            builder.Register<ITurnQueueView, TurnQueueView>(Lifetime.Singleton);
            builder.Register<IUIProvider, UIProvider>(Lifetime.Singleton);
            builder.Register<IRandomService, RandomService>(Lifetime.Singleton);
            builder.Register<ICharactersProvider, CharactersProvider>(Lifetime.Singleton);
        }
    }
}