
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;



public class PlayerManager : MonoBehaviour
{
    //PlayerManager will do things such as keeping track of player score, update the player's HUD etc
    public PlayerStats playerStats;
    public static PlayerManager instance;
    public Transform playerArm;
    public PlayerArm playerArmCS;
    
    private Image healthBar;
    private TMP_Text healthText;
    private Image energyBar;
    private TMP_Text energyText;
    public float x, y, z;
    Scene loadedScene;
    GameObject HUD;
    private int maxHealthCache;
    private int maxEnergyCache;

    public TMP_Text ammoDisplay;
    public FireArmBaseClass weapon;

    #region Singleton
    private void Awake()
    {
        if (instance == null)
        {
            instance = GetComponent<PlayerManager>();
        }
        else if (instance != null && instance != this.GetComponent<PlayerManager>())
        {
            Destroy(this);
        }
    }
    #endregion

    #region Scene Change Detection
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)//called when a scene is changed.
    {
        loadedScene = SceneManager.GetActiveScene();//returns the active scene
        if (loadedScene.name == "Level01")//change the player's position when entering the game scene
        {
            transform.position = new Vector3(x,y,z);
        }
        else//change the player movement characteristics to match station movement
        {

        }
    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        HUD = GameObject.FindGameObjectWithTag("HUD");
        healthText = HUD.transform.Find("HealthText").GetComponent<TMP_Text>();
        healthBar = HUD.transform.Find("Hp Fill").GetComponent<Image>();
        energyBar = HUD.transform.Find("Energy Fill").GetComponent<Image>();
        energyText = HUD.transform.Find("Energy Text").GetComponent<TMP_Text>();

        maxHealthCache = playerStats.maxHealth.GetValue();
        maxEnergyCache = playerStats.maxEnergy.GetValue();

        playerArm = EquipmentManager.instance.playerArm;
        playerArmCS = EquipmentManager.instance.playerArmCS;
        ammoDisplay = HUD.transform.Find("AmmoDisplay").GetComponent<TMP_Text>();
    }

    private void Update()
    {
        UpdateHUD();
    }
    public void UpdateHUD()
    {
        healthText.text = playerStats.currentHealth.ToString();
        healthBar.fillAmount = (float)playerStats.currentHealth / maxHealthCache;
        energyText.text = playerStats.CurrentEnergy.ToString();
        energyBar.fillAmount = (float)playerStats.CurrentEnergy / maxEnergyCache;
        if (weapon != null)
        {
            ammoDisplay.text = weapon.bulletsLeft + "/" + weapon.magSize;
        }
    }
}
