using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SocialMediasUI : MonoBehaviour
{
    [SerializeField] Button githubButton;
    [SerializeField] Button itchIoButton;
    [SerializeField] Button linkedinButton;
    [SerializeField] Button twitterButton;
    [SerializeField] Button instagramButton;
    
    private void Awake() 
    {
        githubButton.onClick.AddListener(() => {
            Application.OpenURL("https://github.com/DuduBertol");
        });    
        itchIoButton.onClick.AddListener(() => {
            Application.OpenURL("https://dudubertoldev.itch.io");
        });    
        linkedinButton.onClick.AddListener(() => {
            Application.OpenURL("https://www.linkedin.com/in/eduardo-bertol/");
        });    
        twitterButton.onClick.AddListener(() => {
            Application.OpenURL("https://twitter.com/dudubertoldev");
        });   
        instagramButton.onClick.AddListener(() => {
            Application.OpenURL("https://www.instagram.com/dudubertoldev/");
        });    
    }
}