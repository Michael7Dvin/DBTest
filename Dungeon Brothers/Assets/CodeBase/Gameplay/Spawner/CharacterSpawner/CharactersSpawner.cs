﻿using System.Collections.Generic;
using CodeBase.Gameplay.Characters;
using CodeBase.Gameplay.Services.MapService;
using CodeBase.Gameplay.Tiles;
using CodeBase.Infrastructure.Services.Factories.Characters;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Gameplay.Spawner.CharacterSpawner
{
    public class CharactersSpawner : ICharactersSpawner
    {
        private readonly IMapService _mapService;
        private readonly ICharacterFactory _characterFactory;

        public CharactersSpawner(IMapService mapService,
            ICharacterFactory characterFactory)
        {
            _mapService = mapService;
            _characterFactory = characterFactory;
        }

        public async UniTask Spawn(Dictionary<Vector2Int, CharacterConfig> spawnCharacter)
        {
            foreach (var character in spawnCharacter)
            {
                if (_mapService.TryGetTile(character.Key, out Tile tile))
                {
                    Debug.Log("1");
                    
                    Character prefab = await _characterFactory.Create(character.Value);
                    Transform transform = tile.transform;
                    
                    prefab.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
                    
                    tile.Occupy(prefab);
                }
            }
        }
    }
}