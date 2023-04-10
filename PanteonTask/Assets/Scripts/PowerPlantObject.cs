using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPlantObject : MonoBehaviour
{
    public static PowerPlantObject instance;
    public int energy = 0;

    private void Awake()
    {
        if (instance!= null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
    private void Start()
    {
        InvokeRepeating("EnergyProduction",3,3);
    }
    public void EnergyProduction()
    {
        energy += 2;
        UIManager.instance.energyValue.text = energy +"/" + 3000;
    }
}
