﻿using UnityEngine;
using System.Collections;
using System;

public class RedRibbon : Equipment {

	// Use this for initialization
	public override void Init ()
    {
        _tag = "hat";
	}

    public override void UpdateStats()
    {
        _player.JumpPower *= 1.2f;
    }
}
