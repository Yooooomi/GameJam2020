using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStats : MonoBehaviour
{
    public AnimationCurve ratioOfAddedCd;
    public float truckCooldownReduction { get; private set; }
    public int totalQte { get; private set; }

    public UnityEvent onChange = new UnityEvent();

    public void SetCDR(float cdr)
    {
        truckCooldownReduction = cdr;
        onChange.Invoke();
    }

    public void SetTotalQTE(int total)
    {
        totalQte += total;
        onChange.Invoke();
    }

    public void AddCdReduction(float amount)
    {
        truckCooldownReduction += ratioOfAddedCd.Evaluate(truckCooldownReduction) * amount;
        onChange.Invoke();
    }
}
