using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Effects : MonoBehaviour
{
    public string effectName;
    public string chipName;
    private Player _player;
    public Sprite playerSprite;
    public Sprite[] monsterSprites;
    public float timeToResetSprite;
    public GameObject[] items;
    private Random randomNumber;
    private string[] effectsName = {"GiveGold", "BecomeMonster", "CleanInventory", "RabbitBrought", "BackToTavern",
        "RandomizeEffects", "Sales", ""};

    public string EffectName => effectName;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public void EnableEffect(string name)
    {
        Debug.Log("applied effect " + name);
        Invoke(name,0f);
    }

    public void DisableEffect()
    {
        enabled = false;
    }

    void HealPlayer()
    {
        Invoke("HealPlayer",1f);
        if (_player.Health<_player.StartHealth) 
        _player.Health += 1;
    }
    
    void GiveGold() // player recieve gold
    {
        Debug.Log("received sea of gold");
    }

    void BecomeMonster() // monsters don't attack
    {
        _player.GetComponent<SpriteRenderer>().sprite = monsterSprites[randomNumber.Next(0, monsterSprites.Length - 1)];
        var enemies = GameObject.FindGameObjectsWithTag("enemy");
        for (var i = 0; i < enemies.Length; i++)
            enemies[i].GetComponent<Enemy>().DontDoAnything = true;
        Invoke("resetSprite",timeToResetSprite);
    }

    void CleanInventory() // remove part of player's items
    { 
        var itemCount = randomNumber.Next(1, 5);
        for (var i = 0; i < itemCount; i++)
        {
            //_player.removeItem(i);
        }
            
    }

    void RabbitBrought() // summon random item near player
    {
        Instantiate(items[randomNumber.Next(0, items.Length - 1)]);
    }

    void BackToTavern() // teleport back to the tavern
    {
        //TeleportPlayerToTheTavern();
    }

    void RandomizeEffects() // make all values of player current effects random
    {
        
    }

    void Sales() // summon random count of items 
    {
        for (var i=0; i<items.Length; i++)
        for (var j = 0; j < randomNumber.Next(0, 5); j++)
            Instantiate(items[j]);
    }

    void BloodMoney()
    {
        _player.useGoldInsteadOfHP = true;
    }
    
    void resetSprite() // for monster mode
    {
        _player.GetComponent<SpriteRenderer>().sprite = playerSprite;
        var enemies = GameObject.FindGameObjectsWithTag("enemy");
        for (var i = 0; i < enemies.Length; i++)
            enemies[i].GetComponent<Enemy>().DontDoAnything = false;
    }
}
