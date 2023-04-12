using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    public static UIManager instance = null;
    public Information Information;
    public GameObject prefabSoldier1, prefabSoldier2, prefabSoldier3;
    private List<float> objectPos = new List<float>();
    public TextMeshProUGUI energyValue;
    public List<Buildings> buildings = new();
    public List<Soldiers> soldiers = new ();
    public List<GameObject> barrackList = new();
    public List<GameObject> powerPlant = new List<GameObject>();
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    public void InstantiateObject(GameObject obj)
    {
        var generateObject = GenerateBasedOnObjectSize(obj);
        Instantiate(obj, new Vector3(generateObject[0], generateObject[1], -.1f), Quaternion.identity);
    }

    public List<float> GenerateBasedOnObjectSize(GameObject GO)
    {
        objectPos.Clear();
        if (GO.transform.localScale.x % 2 == 0 && GO.transform.localScale.y % 2 == 0)
        {
            objectPos.Add(3.5f);
            objectPos.Add(4.5f);
            barrackList.Add(GO);
            return objectPos;
        }
        else if (GO.transform.localScale.x % 2 != 0 && GO.transform.localScale.y % 2 == 0 || GO.transform.localScale.x % 2 == 0 && GO.transform.localScale.y % 2 != 0)
        {
            objectPos.Add(3.5f);
            objectPos.Add(4f);

            return objectPos;
        }
        objectPos.Add(2f);
        objectPos.Add(2f);

        return objectPos;

    }
    public void UIObjectPanelClose()
    {
        for (int i = 0; i < buildings.Count; i++)
        {
            buildings[i].UI.SetActive(false);
        }
    }
    public void OpenSoldierBarrackPanel(GameObject GO)
    {
        var selectedBarrack = GO.GetComponent<SoldierBarrackObjectClass>();
        UIObjectPanelClose();
        Information.image.sprite = buildings[0].image;
        Information.name.text = buildings[0].name;
        buildings[0].UI.SetActive(true);
        AddListenerButton(selectedBarrack);
    }
    public void AddListenerButton(SoldierBarrackObjectClass selectedBarrack)
    {
        buildings[0].UI.gameObject.transform.GetChild(3).GetComponent<Button>().onClick.RemoveAllListeners();
        buildings[0].UI.gameObject.transform.GetChild(4).GetComponent<Button>().onClick.RemoveAllListeners();
        buildings[0].UI.gameObject.transform.GetChild(5).GetComponent<Button>().onClick.RemoveAllListeners();
        buildings[0].UI.gameObject.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(() => selectedBarrack.InstantiateSpawnPoint(prefabSoldier1));
        buildings[0].UI.gameObject.transform.GetChild(4).GetComponent<Button>().onClick.AddListener(() => selectedBarrack.InstantiateSpawnPoint(prefabSoldier2));
        buildings[0].UI.gameObject.transform.GetChild(5).GetComponent<Button>().onClick.AddListener(() => selectedBarrack.InstantiateSpawnPoint(prefabSoldier3));
    }
    public void OpenPowerPlantPanel()
    {
        UIObjectPanelClose();
        Information.image.sprite = buildings[1].image;
        Information.name.text = buildings[1].name;
        buildings[1].UI.SetActive(true);
    }
    public void OpenSoldiersPanel(GameObject GO)
    {
        UIObjectPanelClose();
        Information.image.sprite = buildings[2].image;
        Information.name.text = buildings[2].name;
        buildings[2].UI.SetActive(true);
        soldiers[GO.GetComponent<SoldierObjectClass>().soldierLevel - 1].soldiersLevel.text = "Soldiers Level: " + GO.GetComponent<SoldierObjectClass>().soldierLevel;
        soldiers[GO.GetComponent<SoldierObjectClass>().soldierLevel - 1].soldiersCount.text = "Soldiers Count: " + GO.GetComponent<SoldierObjectClass>().soldierCount;
        soldiers[GO.GetComponent<SoldierObjectClass>().soldierLevel - 1].soldiersHealth.text = "Soldiers Health: " + GO.GetComponent<SoldierObjectClass>().soldierHealth;
        soldiers[GO.GetComponent<SoldierObjectClass>().soldierLevel - 1].soldiersAttack.text = "Soldiers Attack: " + GO.GetComponent<SoldierObjectClass>().soldierAttack;

    }
    public void OpenEnemyPanel(GameObject GO)
    {
        UIObjectPanelClose();
        Information.image.sprite = buildings[3].image;
        Information.name.text = buildings[3].name;
        buildings[3].UI.SetActive(true);
        buildings[3].level1Count.text = "Health : " + GO.GetComponent<EnemyObjectClass>()._enemyHealth;
        buildings[3].level2Count.text = "Power : " + GO.GetComponent<EnemyObjectClass>()._enemyAttack;
    }
}

[System.Serializable]
public class Information
{
    public TextMeshProUGUI name;
    public Image image;
}
[System.Serializable]
public class Buildings
{
    public string name;
    public GameObject UI;
    public Sprite image;
    public TextMeshProUGUI level1Count;
    public TextMeshProUGUI level2Count;
    public TextMeshProUGUI level3Count;

}
[System.Serializable]
public class Soldiers
{
    public TextMeshProUGUI soldiersLevel;
    public TextMeshProUGUI soldiersCount;
    public TextMeshProUGUI soldiersHealth;
    public TextMeshProUGUI soldiersAttack;
}