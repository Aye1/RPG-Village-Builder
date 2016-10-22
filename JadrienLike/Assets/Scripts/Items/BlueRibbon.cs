﻿using System;
using UnityEngine;

public class BlueRibbon : Equipment {

    // Use this for initialization
    public override void Init()
    {
        _tag = "hat";
    }

    public override void UpdateStats()
    {
        _player.Speed *= 1.3f;
    }
}
