using Character;
using TMPro;
using UnityEngine;

namespace UI
{
    public sealed class HUDScreen : UIScreen
    {
        [SerializeField] private TMP_Text _hitPointsCount;

        private void OnEnable()
        {
            CharacterStatsObserver.OnCharacterHitPointsStatsChanged += this.UpdateHitPointsCount;
        }

        private void OnDisable()
        {
            CharacterStatsObserver.OnCharacterHitPointsStatsChanged -= this.UpdateHitPointsCount;
        }

        private void UpdateHitPointsCount(int value)
        { 
            int currentValue = value <= 0 ? 0 : value;
            
            this._hitPointsCount.text = currentValue.ToString();
        }

    }
}