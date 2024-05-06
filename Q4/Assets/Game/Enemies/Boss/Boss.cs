using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public PlayerMovement player;
    public GameObject bossUi;

    void Start()
    {
        
    }

    void Update()
    {
        if(player.isOnBoss)
        {
            bossUi.SetActive(true);
        }
    }
}
