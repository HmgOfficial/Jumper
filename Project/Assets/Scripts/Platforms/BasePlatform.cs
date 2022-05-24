using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class BasePlatform : Platform
{



    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        //CheckPlayer();
        //CheckWalls();
    }

    //override protected bool Foo()
    //{
    //    return false;
    //}
}
