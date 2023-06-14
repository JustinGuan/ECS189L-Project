using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject fire;
    [SerializeField] private float timer = 90.0f;
    private ScenesManager sm;
    private FireplaceMechanic fm;
    private PlayerHealthBar _playerHealthBar;
    private float timeSinceCheck;
    private float playerHP;
    private float fireHP;

    private void Awake()
    {
        // Set it as 1, so we don't trigger the loss condition upon restarting.
        playerHP = 1.0f;
        fireHP = 1.0f;
    }

    void Start()
    {
        sm = GetComponent<ScenesManager>();
        _playerHealthBar = player.GetComponent<PlayerHealthBar>();
        timeSinceCheck = 0.0f;
    }

    void Update()
    {
        timeSinceCheck += Time.deltaTime;
        // If the either the fire or player is null, dont update.
        if(fire == null || player == null) return;
        // Lose Conditions.
        playerHP = _playerHealthBar.GetPlayerHealth();
        fireHP = fm.GetFireHealth();
        if(playerHP <= 0.0f || fireHP <= 0.0f)
        {
            if(playerHP <= 0.0f)
            {
                Debug.Log("Player is dead");
            }
            if(fireHP <= 0.0f)
            {
                Debug.Log("Fire is dead");
            }
            sm.LoadGameOver();
        }
        // Victory Conditions.
        if(timeSinceCheck >= timer)
        {
            sm.LoadVictoryScreen();
        }
    }

    public void SetFire(GameObject go)
    {
        this.fire = go;
        this.fm = go.GetComponent<FireplaceMechanic>();
    }
}
