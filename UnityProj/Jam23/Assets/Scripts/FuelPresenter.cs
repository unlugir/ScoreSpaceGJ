using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FuelPresenter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textDisplayer;
    [SerializeField] private Image _fuelDisplayer;
    
    void Start()
    {
        if (_textDisplayer == null 
            || _fuelDisplayer == null)
        {
            gameObject.SetActive(false);
            this.enabled = false;
            
            Debug.LogError("FuelPresenter is not set to data.");
            return;
        }
        
        _fuelDisplayer.fillAmount = GameManager.Instance.localAirplane.fuel / GameManager.Instance.localAirplane.startFuel;
        _textDisplayer.text = $"{GameManager.Instance.localAirplane.fuel}/{GameManager.Instance.localAirplane.startFuel}";
    }

    private void LateUpdate()
    {
        _fuelDisplayer.fillAmount = GameManager.Instance.localAirplane.fuel / GameManager.Instance.localAirplane.startFuel;
        _textDisplayer.text = $"{(int)GameManager.Instance.localAirplane.fuel}/{(int)GameManager.Instance.localAirplane.startFuel}";
    }
}
