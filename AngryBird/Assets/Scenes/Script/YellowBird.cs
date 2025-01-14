using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowBird : BIrd
{

    public override void ShowSkill()
    {
        base.ShowSkill();
        rg.velocity *= 2;
    }
}
