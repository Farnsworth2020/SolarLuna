using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipSystem : MonoBehaviour
{
    private Player _player;
    public GameObject chip;
    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        LoadChips();
    }

    private void LoadChips()
    {
        for (var i = 0; i < _player.Chips.Length; i++)
        {
            Instantiate(chip, transform);
        }
    }
}
