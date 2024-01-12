using UnityEngine.UI;
using UnityEngine;

public class HealthSlider : MonoBehaviour
{
    [SerializeField]
    private Slider m_FillerSlider;
    [SerializeField]
    private Slider m_HealthSlider;

    [SerializeField]
    private float m_LerpDurationValue = 0.2f;

    private float m_LerpDuration;

    private float m_CurrentHealth;

    private void Awake()
    {
        var Canvas = GetComponent<Canvas>();
        Canvas.worldCamera = Camera.main;
    }
    public void Init(int maxHealth)
    {
        m_FillerSlider.maxValue = maxHealth;
        m_FillerSlider.value = maxHealth;
        m_HealthSlider.maxValue = maxHealth;
        m_HealthSlider.value = maxHealth;
        m_CurrentHealth = maxHealth;
    }

    public void HealthUpdated(int value)
    {
        m_HealthSlider.value = value;
        m_CurrentHealth = value;
        m_LerpDuration = m_LerpDurationValue;
        Debug.Log("Update current health = " + value);
    }

    private void FixedUpdate()
    {
        if(m_LerpDuration > 0)
        {
            m_FillerSlider.value = Mathf.Lerp( m_CurrentHealth, m_FillerSlider.value , m_LerpDuration / m_LerpDurationValue);
            m_LerpDuration -= Time.deltaTime;
            Debug.Log("FillerSLider Value = " + m_FillerSlider.value);
        }
    }
}
