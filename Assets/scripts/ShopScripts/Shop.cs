﻿using UnityEngine;
using UnityEngine.UI;

//This class will be used by the UI bottom to buy a new tower
public class Shop : MonoBehaviour 
{
    [SerializeField] private Color normalTextColor = Color.blue;
    [SerializeField] private Color cantBuildTextColor = Color.blue;
    [SerializeField] private Color normalCanvasColor = Color.blue;
    [SerializeField] private Color cantBuildCanvasColor = Color.blue;
    [SerializeField] private GameObject buttonPrefab = null;

	private int numOfButtons;
	private bool[] canBuildTower;
	private Transform[] buttons;
	private SoulsCounter soulsCounter;
	private ScoreCounter scoreCounter;
	private BuildManager buildManager;
    private int indexOfThisTower;
    private GameObject gameMaster;
    private TowerManager towerManager;
    private ShopManager shopManager;

	public void PurcheseTower0()
    {
		indexOfThisTower = 0;
		if ( !soulsCounter.CanBuild (indexOfThisTower) )
			return;
		buildManager.SetTowerToBuildIndex (indexOfThisTower);
		buildManager.SetTowerToBuild (buildManager.tower[indexOfThisTower]);
		buildManager.SetSelectionTowerToBuild (buildManager.selectionTower [indexOfThisTower]);
        towerManager.TowerSelected();
    }

	public void PurcheseTower1()
    {
		indexOfThisTower = 1;
		if ( !soulsCounter.CanBuild (indexOfThisTower) )
			return;
		buildManager.SetTowerToBuildIndex (indexOfThisTower);
		buildManager.SetTowerToBuild (buildManager.tower[indexOfThisTower]);
		buildManager.SetSelectionTowerToBuild (buildManager.selectionTower [indexOfThisTower]);
        towerManager.TowerSelected();
    }

	public void PurcheseTower2()
    {
		indexOfThisTower = 2;
		if ( !soulsCounter.CanBuild (indexOfThisTower) )
			return;
		buildManager.SetTowerToBuildIndex (indexOfThisTower);
		buildManager.SetTowerToBuild (buildManager.tower[indexOfThisTower]);
		buildManager.SetSelectionTowerToBuild (buildManager.selectionTower [indexOfThisTower]);
        towerManager.TowerSelected();
    }

    public void PurcheseTower3()  // Researcher
    {
        indexOfThisTower = 3;

        if (gameMaster.GetComponent<InstancesManager>().GetResearchTowerOfTheTime() != null)
            return;
        gameMaster.GetComponent<InstancesManager>().SetResearchTowerOfTheTime(buildManager.tower[indexOfThisTower].GetComponent<SearchCenterPlace>());

        if (!soulsCounter.CanBuild(indexOfThisTower))
            return;
        buildManager.SetTowerToBuildIndex(indexOfThisTower);
        buildManager.SetTowerToBuild(buildManager.tower[indexOfThisTower]);
        buildManager.SetSelectionTowerToBuild(buildManager.selectionTower[indexOfThisTower]);
        towerManager.TowerSelected();
    }

    public void PurcheseTower4()
    {
        indexOfThisTower = 4;
        if (!soulsCounter.CanBuild(indexOfThisTower))
            return;
        buildManager.SetTowerToBuildIndex(indexOfThisTower);
        buildManager.SetTowerToBuild(buildManager.tower[indexOfThisTower]);
        buildManager.SetSelectionTowerToBuild(buildManager.selectionTower[indexOfThisTower]);
        towerManager.TowerSelected();
    }

    public int GetTowerToBuildIndex()
    {
        return indexOfThisTower;
    }

	/// <summary>
	/// Determines whether this instance can build tower the specified index.
	/// </summary>
	/// <returns><c>true</c> if this instance can build tower the specified index; otherwise, <c>false</c>.</returns>
	/// <param name="index">Index.</param>
	public bool CanBuildTower(int index)
    {
		return canBuildTower [index];
	}

	// follow the monkey 
//	public void PurcheseTower0(){
//		int indexOfThisTower = 0;
//		if ( !soulsCounter.CanBuild (indexOfThisTower) )
//			return;
//		buildManager.SetTowerToBuild (buildManager.Tower[indexOfThisTower]);
//	}

	//Will set the store up:
	private void Awake(){
		numOfButtons = transform.childCount;
		SetTheShopButtons ();
		SetCanBuildTower ();
	}

	//get the buttons gameobjects
	private void SetTheShopButtons()
    {
		buttons = new Transform[numOfButtons];
		for (int i = 0; i < numOfButtons; i++)
        {
			buttons [i] = transform.GetChild (i);
		}
	}

	/// <summary>
	/// Sets the can build tower.
	/// </summary>
	private void SetCanBuildTower()
    {
		canBuildTower = new bool[numOfButtons];
		for (int i = 0; i < numOfButtons; i++)
        {
			canBuildTower [i] = false;
		}
	}

	//Instantiate and initialize 
	private void Start()
    {
        gameMaster = GameObject.Find("GameMaster");
        buildManager = gameMaster.GetComponent<BuildManager> ();
		soulsCounter = gameMaster.GetComponent<SoulsCounter> ();
		scoreCounter = gameMaster.GetComponent<ScoreCounter> ();
        towerManager = gameMaster.GetComponent<TowerManager>();
        shopManager = gameMaster.GetComponent<ShopManager>();
	}

	/// <summary>
	/// Update this instance.
	/// every frame verify if the player have enought soul to 
	/// buy each tower
	/// </summary>
	private void Update()
    {
		UpdateCanBuildTower ();
	}

	/// <summary>
	/// Updates the can build tower.
	/// </summary>
	private void UpdateCanBuildTower()
    {
		for (int i = 0 ; i < buildManager.tower.Length; i ++)
        {
            if ( !soulsCounter.CanBuild( buttons[i].GetComponent<ShopButton>().GetIndexOfThisTower() ))
            {
				canBuildTower [i] = false;
                buttons[i].GetComponentInChildren<PriceFinder>().
                          GetComponent<Text>().color = cantBuildTextColor;
                buttons[i].GetComponentInChildren<DescriptionCanvasFinder>().
                          GetComponent<Image>().color = cantBuildCanvasColor;
			} 
            else 
            {
				canBuildTower [i] = true;
                buttons[i].GetComponentInChildren<PriceFinder>().
                          GetComponent<Text>().color = normalTextColor;
                buttons[i].GetComponentInChildren<DescriptionCanvasFinder>().
                          GetComponent<Image>().color = normalCanvasColor;
			}
		}
	}
}
