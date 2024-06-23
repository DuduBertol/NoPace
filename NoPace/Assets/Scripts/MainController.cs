using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainController : MonoBehaviour
{
    public static MainController Instance {get; private set;}


    public float defaultTime;
    public bool isPaused;
    public List<GameObject> activityGameObjectList;
    public Color backgroundColor;

    [SerializeField] private float activityRunningTime;
    [SerializeField] private TextMeshProUGUI activityRunningTimeText;
    [SerializeField] private Transform activityRow;
    [SerializeField] private Transform activityTemplate;
    [SerializeField] private Transform lockerUI;
    [SerializeField] private Button addActivityButton;
    [SerializeField] private Button resetButton;
    [SerializeField] private Button startActivityButton;
    [SerializeField] private Button lockButton;
    [SerializeField] private Button unlockButton;
    [SerializeField] private Image startActivityImage;
    [SerializeField] private Sprite pauseSprite;
    [SerializeField] private Sprite resumeSprite;
    
    private int maxActivityAmount = 20;


    private void Awake() 
    {
        Instance = this;  

        addActivityButton.onClick.AddListener(() => {
            AddNewActivity();
        });
        startActivityButton.onClick.AddListener(() => {
            TogglePauseTimer();
            StartNearestActivity();
        });   
        resetButton.onClick.AddListener(() => {
            ResetActivity();
        });   
        lockButton.onClick.AddListener(() => {
            LeanTween.scale(lockerUI.GetComponent<RectTransform>(), Vector3.one, 0.25f).setEaseOutExpo();
        });   
        unlockButton.onClick.AddListener(() => {
            LeanTween.scale(lockerUI.GetComponent<RectTransform>(), Vector3.zero, 0.25f).setEaseOutExpo();
        });   
    }

    private void Update() 
    {
        if(!isPaused && activityGameObjectList.Count != 0)
        {
            activityRunningTime += Time.deltaTime;

            UpdateVisual();
        }
    }

    private void AddNewActivity()
    {
        if(activityGameObjectList.Count <= maxActivityAmount)
        {
            Transform activityTemplateInstantiated = Instantiate(activityTemplate, activityRow);
            activityGameObjectList.Add(activityTemplateInstantiated.gameObject);
        }
    }

    private void TogglePauseTimer()
    {
        isPaused = !isPaused;

        if(isPaused)
        {
            startActivityImage.sprite = resumeSprite;
        }
        else
        {
            startActivityImage.sprite = pauseSprite;
        }

        if(activityGameObjectList.Count != 0)
        {
            activityGameObjectList[0].GetComponent<ActivityTemplate>().backgroundImage.color = backgroundColor;
        }
    }

    public void StartNearestActivity()
    {
        if(activityGameObjectList.Count != 0)
        {
            activityGameObjectList[0].GetComponent<ActivityTemplate>().StartTimer(true);
        }
    }

    private void UpdateVisual()
    {            
        int minutes = Mathf.FloorToInt(activityRunningTime / 60);
        int seconds = Mathf.FloorToInt(activityRunningTime % 60);
    
        activityRunningTimeText.text = string.Format("{0:00}:{1:00}", minutes, seconds); 
    }

    private void ResetActivity()
    {
        isPaused = true;
        activityRunningTime = 0f;
        
        startActivityImage.sprite = resumeSprite;   
        
        UpdateVisual();

        for (int i = 0; i < activityGameObjectList.Count; i++)
        {
            Destroy(activityGameObjectList[i]);
            activityGameObjectList.RemoveAt(i);
        }
    }

}
