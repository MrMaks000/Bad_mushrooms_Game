using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    [SerializeField] private VolumeController volumeController;

    private Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();
        slider.value = volumeController.GetVolume();
    } 
}
