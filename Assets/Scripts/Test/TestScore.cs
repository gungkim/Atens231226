using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScore : TestBase
{
    Player player;
    void Start()
    {
        player = FindAnyObjectByType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
