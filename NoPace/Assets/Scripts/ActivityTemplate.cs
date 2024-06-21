using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActivityTemplate : MonoBehaviour
{
    public float remainingTimer;
    [SerializeField] private bool canStart;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private Button plusButton;
    [SerializeField] private Button minusButton;
    [SerializeField] private Button closeButton;

    private void Awake() 
    {
        plusButton.onClick.AddListener(() => {
            remainingTimer += MainController.Instance.defaultTime;
            UpdateVisual();
        });    
        minusButton.onClick.AddListener(() => {
            remainingTimer -= MainController.Instance.defaultTime;
            UpdateVisual();
        });    
        closeButton.onClick.AddListener(() => {
            DestroySelf();
            
        });    
    }

    private void Update()
    {
        if(!MainController.Instance.isPaused && canStart)
        {
            DecreasingTime();
        }
    }

    private void DecreasingTime()
    {
        if(remainingTimer > 0)
        {
            remainingTimer -= Time.deltaTime;
            if(remainingTimer < 0.5f)
            {
                Handheld.Vibrate();
            }
        }
        else if(remainingTimer < 0)
        {
            DestroySelf();
            MainController.Instance.StartNearestActivity();
        }

        UpdateVisual();
    }

    private void UpdateVisual()
    {
        int minutes = Mathf.FloorToInt(remainingTimer / 60);
        int seconds = Mathf.FloorToInt(remainingTimer % 60);
    
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void StartTimer(bool canStart)
    {
        this.canStart = canStart;
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
        MainController.Instance.activityGameObjectList.Remove(gameObject);
    }
}
