using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FuelPresenter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textDisplayer;
    [SerializeField] private Image _fuelDisplayer;
    [SerializeField] private TextMeshProUGUI _consumText;
    
    public void DisplayFuelData()
    {
        if (_textDisplayer == null 
            || _fuelDisplayer == null
            || GameManager.Instance.localAirplane == null)
        {
            gameObject.SetActive(false);
            this.enabled = false;
            
            Debug.LogError("FuelPresenter is not set to data.");
            return;
        }
        
        _fuelDisplayer.fillAmount = GameManager.Instance.localAirplane.fuel / GameManager.Instance.localAirplane.startFuel;
        _textDisplayer.text = $"{GameManager.Instance.localAirplane.fuel}/{GameManager.Instance.localAirplane.startFuel}";
        _consumText.text = $"-{GameManager.Instance.localAirplane.fuelConsumption}";
    }

    private void LateUpdate()
    {
        if(GameManager.Instance.localAirplane == null) return;
        
        _fuelDisplayer.fillAmount = GameManager.Instance.localAirplane.fuel / GameManager.Instance.localAirplane.startFuel;
        _textDisplayer.text = $"{(int)GameManager.Instance.localAirplane.fuel}/{(int)GameManager.Instance.localAirplane.startFuel}";
        _consumText.text = $"-{System.Math.Round((double)GameManager.Instance.localAirplane.fuelConsumption, 1)}";
    }
}
