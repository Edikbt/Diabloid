using System.Collections.Generic;
using UnityEngine;

namespace Diabloid
{
    public interface IGameFactory
    {
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressWriters { get; }

        void Cleanup();
        GameObject CreateHero();
        GameObject CreateMonster();
    }
}