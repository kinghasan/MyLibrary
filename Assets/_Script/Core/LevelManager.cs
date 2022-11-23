using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : GameEntity<LevelManager>
{
    public List<Level> LevelList;

    public Level Current { get; set; }

    public void Init()
    {

    }
}
