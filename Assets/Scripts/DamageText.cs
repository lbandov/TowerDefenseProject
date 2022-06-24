using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    public TextMeshProUGUI DmgText => GetComponentInChildren<TextMeshProUGUI>();

    public void ReturnTextToPool()
    {
        transform.SetParent(null);
        ObjectPooler.ReturnToPool(gameObject);
    }
}
