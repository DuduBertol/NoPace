using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActivityTemplate : MonoBehaviour
{
    public float remainingTimer;
    public Image backgroundImage;

    private bool isDead;

    [SerializeField] private bool canStart;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private Button plusButton;
    [SerializeField] private Button minusButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button chooseColorButton;
    [SerializeField] private AudioSource audioSource;


    private void Awake() 
    {
        plusButton.onClick.AddListener(() => {
            if(remainingTimer >= 0)
                remainingTimer += MainController.Instance.defaultTime;
            UpdateVisual();
        });    
        minusButton.onClick.AddListener(() => {
            if(remainingTimer > 0)
                remainingTimer -= MainController.Instance.defaultTime;
            UpdateVisual();
        });    
        closeButton.onClick.AddListener(() => {
            DestroySelf();
            MainController.Instance.StartNearestActivity();
        });    
    }

    private void Start() 
    {
        LeanTween.scale(gameObject.GetComponent<RectTransform>(), Vector3.one, 0.25f).setEaseOutExpo();    
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
        float timeToVibrate = 3f;

        if(remainingTimer > 0)
        {
            remainingTimer -= Time.deltaTime;
            if(remainingTimer < timeToVibrate)
            {
                Handheld.Vibrate();
            }
        }
        else if(remainingTimer < 0)
        {
            remainingTimer = 0f;
        }
        else if(remainingTimer == 0)
        {
            if (!isDead)
            {
                isDead = true;
                audioSource.PlayOneShot(audioSource.clip);
                DestroySelf();
                MainController.Instance.StartNearestActivity();
            }
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
        LeanTween.moveX(this.gameObject.GetComponent<RectTransform>(), -1500, 4f).setEaseInOutBack().setDestroyOnComplete(true);
        MainController.Instance.activityGameObjectList.Remove(gameObject);

        if(MainController.Instance.activityGameObjectList.Count != 0)
            MainController.Instance.activityGameObjectList[0].GetComponent<ActivityTemplate>().backgroundImage.color = MainController.Instance.backgroundColor;
    }
}
