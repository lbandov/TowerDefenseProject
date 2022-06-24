using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyFX : MonoBehaviour
{
    [SerializeField] private Transform textDamageSpawnPosition;

    private EnemyHealth _enemyHealth;

    private void Start()
    {
        _enemyHealth = GetComponent<EnemyHealth>();
    }

    public void EnemyHit(Enemy enemy, float damage)
    {
        if (_enemyHealth == enemy.EnemyHealth)
        {
            GameObject newInstance = DamageTextManager.Instance.Pooler.GetInstanceFromPool();
            TextMeshProUGUI damageText = newInstance.GetComponent<DamageText>().DmgText;
            damageText.text = damage.ToString();

            newInstance.transform.SetParent(textDamageSpawnPosition);
            newInstance.transform.position = textDamageSpawnPosition.position;
            newInstance.SetActive(true);
        }
    }

    private void OnEnable()
    {
        EnemyHealth.OnEnemyHit += EnemyHit;
    }

    private void OnDisable()
    {
        EnemyHealth.OnEnemyHit -= EnemyHit;
    }
}
