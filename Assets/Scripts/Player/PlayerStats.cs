using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public AnimationCurve ratioOfAddedCd;
    public float truckCooldownReduction;

    public void AddCdReduction(float amount)
    {
        truckCooldownReduction += ratioOfAddedCd.Evaluate(truckCooldownReduction) * amount;
    }
}
