using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] TileListClass tileListClass;
    public GameObject clickLastGO;
    private UIManager uIManager;
    public bool isMoveSoldier;
    public Button soldierInstLevel1, soldierInstLevel2, soldierInstLevel3;
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
    /// <summary>
	/// Shows which object we have selected when we click.
	/// </summary>
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {

            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -5)), Vector2.zero);

            if (hit.collider != null && !isMoveSoldier)
            {
                if (clickLastGO != null)
                {
                    clickLastGO.GetComponent<BoxCollider2D>().enabled = true;
                    clickLastGO.transform.GetChild(0).GetChild(0).GetComponent<Image>().color = new Color(0 / 255f, 0 / 255f, 0 / 255f, 87f / 255);
                    clickLastGO = null;
                    TileColliderOn();
                }
                if (hit.transform.tag == "Soldier")
                {
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
                    uIManager.OpenSoldierBarrackPanel(hit.transform.gameObject);
                }
                if (hit.transform.tag == "Enemy")
                {
                    uIManager.OpenEnemyPanel(hit.transform.gameObject);

                }
            }
        }

    }

    /// <summary>
	/// Disables the colliders on the tiles.
	/// </summary>
    public void TileColliderClose()
    {
        for (int i = 0; i < tileListClass.tiles.Count; i++)
        {
            tileListClass.tiles[i].GetComponent<BoxCollider2D>().enabled = false;
        }

    }
    /// <summary>
	/// Enabled the colliders on the tiles.
	/// </summary>
    public void TileColliderOn()
    {
        for (int i = 0; i < tileListClass.tiles.Count; i++)
        {
            tileListClass.tiles[i].GetComponent<BoxCollider2D>().enabled = true;
        }

    }
}
