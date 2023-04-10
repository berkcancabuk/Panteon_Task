using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private AgentMovement _agentMovement;
    private NavMeshAgent _navMeshAgent;
    [SerializeField] TileListClass tileListClass;
    public GameObject clickLastGO;
    private UIManager uIManager;
    public int soldierLevel = 1;
    public int ownedSoldiersLevel1 = 0;
    public int ownedSoldiersLevel2 = 0;
    public int ownedSoldiersLevel3 = 0;
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
    private void Start()
    {
        uIManager = UIManager.instance;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -5)), Vector2.zero);

            if (hit.transform.tag == "Soldier" && hit.transform.GetComponent<BoxCollider2D>().enabled)
            {
                //if (_clickLastGO != null)
                //{
                //    if (!_clickLastGO.transform.GetComponent<NavMeshAgent>().isActiveAndEnabled)
                //    {

                //        _clickLastGO.transform.GetChild(0).GetChild(0).GetComponent<Image>().color = new Color(0 / 255f, 0 / 255f, 0 / 255f, 87f / 255);
                //        _clickLastGO.transform.GetComponent<AgentMovement>().enabled = false;
                //        _clickLastGO.transform.GetComponent<NavMeshAgent>().enabled = false;
                //        _clickLastGO.transform.GetComponent<NavMeshObstacle>().enabled = true;
                //    }



                //}

                //hit.transform.GetComponent<NavMeshObstacle>().enabled = false;
                //_agentMovement = hit.transform.GetComponent<AgentMovement>();
                //_agentMovement.enabled = true;
                //_clickLastGO = hit.transform.gameObject;
                if (clickLastGO == null)
                {
                    hit.transform.GetChild(0).GetChild(0).GetComponent<Image>().color = new Color(16f / 255f, 255f / 255f, 72f / 255f, 87f / 255);
                    uIManager.OpenSoldiersPanel(hit.transform.gameObject);
                    hit.transform.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                    clickLastGO = hit.transform.gameObject;
                    TileColliderClose();
                }
               
            }
            if (hit.transform.tag == "PowerPlant")
            {
                uIManager.OpenPowerPlantPanel();
            }
            if (hit.transform.tag == "Cube")
            {
                uIManager.OpenSoldierBarrackPanel();
            }
            if (hit.transform.tag == "Enemy")
            {
                uIManager.OpenEnemyPanel(hit.transform.gameObject);
            }
        }
    }
    public void TileColliderClose()
    {
        for (int i = 0; i < tileListClass.tiles.Count; i++)
        {
            tileListClass.tiles[i].GetComponent<BoxCollider2D>().enabled = false;
        }
       
    }
    public void TileColliderOn()
    {
        for (int i = 0; i < tileListClass.tiles.Count; i++)
        {
            tileListClass.tiles[i].GetComponent<BoxCollider2D>().enabled = true;
        }
       
    }
}
