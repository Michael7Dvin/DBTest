using System;
using System.Collections.Generic;
using CodeBase.Gameplay.Characters;

namespace CodeBase.Gameplay.Services.TurnQueue
{
    public interface ITurnQueue
    {
        IEnumerable<ICharacter> Characters { get; }
        ICharacter ActiveCharacter { get; }

        event Action<ICharacter> NewTurnStarted; 

        void Initialize();
        void CleanUp();

        void SetNextTurn();
        void SetFirstTurn();
    }
}