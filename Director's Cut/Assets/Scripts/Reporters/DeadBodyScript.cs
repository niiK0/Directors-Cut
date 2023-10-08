using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadBodyScript : BaseReporter
{
    protected override void ReporterFunction()
    {
        Destroy(gameObject);
    }
}
