using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private Image totalhealthbar;
    [SerializeField] private Image currenthealthbar;

    void Start()
    {
        totalhealthbar.fillAmount = playerHealth.currentHealth / 10;
    }
    void Update()
    {
        currenthealthbar.fillAmount = playerHealth.currentHealth / 10;
    }
}
