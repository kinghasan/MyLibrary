using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class LevelManager : GameEntity<LevelManager>
{
    public int TestIndex;
    public int StartRandIndex;
    public bool AutoFindLevel = true;
    [HideIf("AutoFindLevel")] public List<Level> LevelList;
    public List<Level> RandList;

    public Level Current { get; set; }

    public void Init()
    {
        var levelIndex = Save.LevelIndex.Value;
        if (Current != null)
        {
            GamePool.DeSpawn(Current);
            Current = null;
        }

        if (TestIndex > 0)
        {
            levelIndex = TestIndex;
            Current = null;
        }

        LevelStart(levelIndex);
    }

    public void LevelStart(int levelIndex)
    {
        Level levelPrefab = null;
        if (levelIndex >= StartRandIndex && StartRandIndex > 0)
        {
            levelIndex = Random.Range(0, RandList.Count);
            levelPrefab = RandList[levelIndex];
        }

        if (levelPrefab == null)
        {
            if (AutoFindLevel)
                levelPrefab = Resources.Load<Level>("Level/Level_" + levelIndex.ToString("D2"));
            else
                levelPrefab = LevelList[levelIndex];
        }

        Current = GamePool.Spawn(levelPrefab);
        Current.transform.SetParent(null);
        Current.Init();

        Game.Enter(GameState.Ready);
    }
}
