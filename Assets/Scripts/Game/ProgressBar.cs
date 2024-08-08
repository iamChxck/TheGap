using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField]
    public Slider slider;


    public float FillSpeed = 0.1f;
  
    // Start is called before the first frame update
    void Start()
    {
        slider.value = 0;
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void IncrementProgress()
    {
        slider.value += FillSpeed * Time.deltaTime;
    }

    public void Reset()
    {
        slider.value = 0;
    }
}
