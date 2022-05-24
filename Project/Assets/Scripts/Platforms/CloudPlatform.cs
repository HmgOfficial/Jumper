using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 class CloudPlatform : Platform
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
    }

    /*public void override RemoveThisPlatform()
    {
        if (platformtType == PlatformType.Cloud)
        {
            StartCoroutine("CloudPlatformRemove");
            return false;
        }
    }*/

    //override protected bool Foo()
    //{
    //    StartCoroutine("CloudPlatformRemove");
    //    return false;
    //}
}
