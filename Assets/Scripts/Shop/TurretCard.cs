using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Shop
{
    public class TurretCard : MonoBehaviour
    {

        public static Action<TurretSettings> OnPlaceTurret;


        [SerializeField] private Image turretImage;
        [SerializeField] private TextMeshProUGUI turretCost;
        
        public TurretSettings TurretLoaded { get; set; }

        public void SetupTurretButton(TurretSettings turretSettings)
        {
            TurretLoaded = turretSettings;
            turretImage.sprite = turretSettings.TurretShopSprite;
            turretCost.text = turretSettings.TurretShopSprite.ToString();
        }

        public void PlaceTurret()
        {
            if (CurrencySystem.Instance.TotalCoins >= TurretLoaded.TurretShopCost)
            {
                CurrencySystem.Instance.RemoveCoins(TurretLoaded.TurretShopCost);
                UIManager.Instance.CloseTurretShopPanel();
                OnPlaceTurret?.Invoke(TurretLoaded);
            }
        }
    }
}