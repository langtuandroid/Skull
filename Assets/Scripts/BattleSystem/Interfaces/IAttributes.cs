﻿using System;

namespace AdventureGame.BattleSystem
{
    public interface IAttributes
    {
        event Action OnUpdateStatus;
        int CurrentHP { get; }
        int CurrentMP { get; } // resource
        int CurrentSP { get; }
        int MaxHP { get; }
        int MaxMP { get; }
        int MaxSP { get; }
    }
}