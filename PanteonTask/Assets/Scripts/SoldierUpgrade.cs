using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SoldierUpgrade : MonoBehaviour
{
    private GameManager gameManager;
    private PowerPlantObject powerPlantObject;
    [SerializeField] private TextMeshProUGUI upgradeTo,cost,soldierLevel;
    private void Start()
    {
        gameManager = GameManager.instance;
        powerPlantObject = PowerPlantObject.instance;
        WriteTextAndUpdateCost();


    }
    public void WriteTextAndUpdateCost()
    {
        if (gameManager.soldierLevel != 3)
        {
            upgradeTo.text = "Upgrade To " + gameManager.soldierLevel + " -> " + (gameManager.soldierLevel + 1);
            soldierLevel.text = "SOLDIER LEVEL " + gameManager.soldierLevel;
            InvokeRepeating("CostRefresh", 3, 3);
            
        }
        else
        {
            CancelInvoke("CostRefresh");
            cost.gameObject.SetActive(false);
            soldierLevel.text = "SOLDIER LEVEL " + gameManager.soldierLevel;
            transform.GetChild(4).gameObject.SetActive(true);
            GetComponent<Button>().enabled = false;
        }
    }
    public void CostRefresh()
    {
        if (powerPlantObject.energy > gameManager.soldierLevel * 5)
        {
            cost.color = Color.green;
            cost.text = "COST : " + (gameManager.soldierLevel * 5);
        }
        else
        {
            cost.color = Color.red;
            cost.text = "COST : " + (gameManager.soldierLevel * 5);
        }
    }
    public void SoldierLevel()
    {

        if (powerPlantObject.energy> gameManager.soldierLevel*5)
        {
            powerPlantObject.energy -= gameManager.soldierLevel * 5;
            gameManager.soldierLevel++;
            UIManager.instance.energyValue.text = powerPlantObject.energy + "/" + 3000;
            WriteTextAndUpdateCost();
        }
        if (gameManager.soldierLevel == 2)
        {
            SoldierSpawnPoint.instance.InstantiateSpawnPointForLevel2();
        }
        if (gameManager.soldierLevel == 3)
        {
            SoldierSpawnPoint.instance.InstantiateSpawnPointForLevel3();
        }
    }
}
