﻿using System.Linq;
using CodeBase.Gameplay.Characters;
using CodeBase.Gameplay.Services.MapGenerator;
using CodeBase.Gameplay.Services.MapService;
using CodeBase.Gameplay.Services.TurnQueue;
using CodeBase.Infrastructure.Services.Factories.Buttons;
using CodeBase.Infrastructure.Services.Factories.Characters;
using CodeBase.Infrastructure.Services.Factories.TurnQueue;
using CodeBase.Infrastructure.Services.Factories.UI;
using CodeBase.Infrastructure.Services.Providers.CharactersProvider;
using CodeBase.Infrastructure.Services.Providers.LevelDataProvider;
using CodeBase.Infrastructure.Services.Providers.ServiceProvider;
using CodeBase.Infrastructure.Services.StaticDataProviding;
using Cysharp.Threading.Tasks;

namespace CodeBase.Infrastructure.Services.Providers.LevelData
{
    public class LevelDataProvider : ILevelDataProvider
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IMapService _mapService;
        private readonly IMapGenerator _mapGenerator;
        private readonly ICommonUIFactory _commonUIFactory;
        private readonly ICharacterFactory _characterFactory;
        private readonly ITurnQueueViewFactory _turnQueueViewFactory;
        private readonly ITurnQueue _turnQueue;
        private readonly ICharactersProvider _charactersProvider;
        private readonly IStaticDataProvider _staticDataProvider;
        private readonly IButtonsFactory _buttonsFactory;

        public LevelDataProvider(IServiceProvider serviceProvider,
            ICommonUIFactory commonUIFactory,
            ICharacterFactory characterFactory,
            ITurnQueueViewFactory turnQueueViewFactory,
            ITurnQueue turnQueue,
            IStaticDataProvider staticDataProvider,
            ICharactersProvider charactersProvider,
            IMapService mapService,
            IMapGenerator mapGenerator,
            IButtonsFactory buttonsFactory)
        {
            _serviceProvider = serviceProvider;
            _commonUIFactory = commonUIFactory;
            _characterFactory = characterFactory;
            _turnQueueViewFactory = turnQueueViewFactory;
            _turnQueue = turnQueue;
            _staticDataProvider = staticDataProvider;
            _charactersProvider = charactersProvider;
            _mapService = mapService;
            _mapGenerator = mapGenerator;
            _buttonsFactory = buttonsFactory;
        }

        public async UniTask WarmUp()
        {
            await _commonUIFactory.WarmUp();
            await _buttonsFactory.WarmUp();
        }

        public async UniTask CreateLevel()
        {
            await _commonUIFactory.Create();
            _turnQueue.Initialize();
            
            await _mapGenerator.GenerateMap();

            await _turnQueueViewFactory.CreateTurnQueueView();
            await _characterFactory.WarmUp(_staticDataProvider.AllCharactersConfigs.CharacterConfigs.Values.ToList());
            

            await _characterFactory.Create(_staticDataProvider.AllCharactersConfigs.CharacterConfigs[CharacterID.Hero]);

            await _characterFactory.Create(
                _staticDataProvider.AllCharactersConfigs.CharacterConfigs[CharacterID.Enemy]);

            await _characterFactory.Create(
                _staticDataProvider.AllCharactersConfigs.CharacterConfigs[CharacterID.Enemy]); 
            await _characterFactory.Create(
                _staticDataProvider.AllCharactersConfigs.CharacterConfigs[CharacterID.Enemy]);
            await _characterFactory.Create(
                _staticDataProvider.AllCharactersConfigs.CharacterConfigs[CharacterID.Enemy]);

            _turnQueue.SetFirstTurn();

            await _buttonsFactory.CreateSkipTurnButton();
        }

        public void Initialize() => _serviceProvider.SetLevelDataProvider(this);
    }
}