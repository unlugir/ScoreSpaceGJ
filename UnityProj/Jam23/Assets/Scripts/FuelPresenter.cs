using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FuelPresenter : MonoBehaviour
{
    [SerializeField] private DebugController _debugController;
    [SerializeField] private TextMeshProUGUI _textDisplayer;
    [SerializeField] private Image _fuelDisplayer;
    
    void Start()
    {
        if (_textDisplayer == null 
            || _debugController == null
            || _fuelDisplayer == null)
        {
            gameObject.SetActive(false);
            this.enabled = false;
            
            Debug.LogError("FuelPresenter is not set to data.");
            return;
        }
        
        _fuelDisplayer.fillAmount = _debugController.fuel / _debugController.startFuel;
        _textDisplayer.text = $"{_debugController.fuel}/{_debugController.startFuel}";
    }

    private void LateUpdate()
    {
        _fuelDisplayer.fillAmount = _debugController.fuel / _debugController.startFuel;
        _textDisplayer.text = $"{(int)_debugController.fuel}/{(int)_debugController.startFuel}";
    }
}
