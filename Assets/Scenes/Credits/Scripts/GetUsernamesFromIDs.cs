using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Discord;
using TMPro;

public class GetUsernamesFromIDs : MonoBehaviour {

    private Discord.Discord discord;
    private bool loaded;
    [Header("Credit Prefabs")]
    [SerializeField] private GameObject leftHand;
    [SerializeField] private GameObject rightHand;
    [Header("Discord IDs")]
    [SerializeField] private long devID;
    [SerializeField] private long musicProducerID;
    [SerializeField] private List<long> userIDs = new List<long>();
    [Header("UI Stuff")]
    [SerializeField] private GameObject parent;
    [SerializeField] private TMP_Text devUserText;
    [SerializeField] private TMP_Text musicUserText;
    [SerializeField] private TMP_Text discordNotif;

    private void OnEnable() {
        InitialiseAfterLoadingScript.Loaded += LoadedEvent;
    }
    
    private void OnDisable() {
        InitialiseAfterLoadingScript.Loaded -= LoadedEvent;
    }

    private void LoadedEvent() {
        loaded = true;
    }

    private readonly List<string> usernameStrings = new List<string>();

    private LoadingBarUpdateTextScript sliderScript;

    private void Start() {
        discord = new Discord.Discord(837028487201554483, (UInt64) CreateFlags.NoRequireDiscord);
        sliderScript = GameObject.Find("LoadingPercentText").transform.parent.GetComponentInChildren<LoadingBarUpdateTextScript>();
        StartCoroutine(TimeDiscordNotif());
        sliderScript.UpdateLoadValue(0.05f);
        UserManager userManager = discord.GetUserManager();
        sliderScript.UpdateLoadValue(0.05f);
        StartUsernameFetch(userManager);
    }

    private void StartUsernameFetch(UserManager userManager) {
        StartCoroutine(FetchDevUsername(userManager, devID));
        StartCoroutine(FetchMusicUsername(userManager, musicProducerID));
        sliderScript.UpdateLoadValue(sliderScript.SliderValue() + 1f / (userIDs.Count * 4));
        foreach (long userID in userIDs) {
            StartCoroutine(FetchUsername(userManager, userID));
            sliderScript.UpdateLoadValue(sliderScript.SliderValue() + 1f / (userIDs.Count * 4));
        }
        StartCoroutine(AddUsernamesToCredits());
    }

    private void Update() {
        discord.RunCallbacks();
    }

    private IEnumerator TimeDiscordNotif() {
        yield return new WaitForSeconds(10f);
        if (loaded) { yield break; }
        discordNotif.gameObject.SetActive(true);
    }

    private IEnumerator FetchDevUsername(UserManager userManager, long userID) {
        userManager.GetUser(userID, (Result result, ref User user) => {
            if (result == Result.Ok) {
                devUserText.text = user.Username + "#" + user.Discriminator;
                sliderScript.UpdateLoadValue(sliderScript.SliderValue() + 1f / (userIDs.Count * 2));
                Debug.Log("Found user: " + user.Username);
            } else {
                Debug.Log(result);
            }
        });
        yield break;
    }
    
    private IEnumerator FetchMusicUsername(UserManager userManager, long userID) {
        userManager.GetUser(userID, (Result result, ref User user) => {
            if (result == Result.Ok) {
                musicUserText.text = user.Username + "#" + user.Discriminator;
                sliderScript.UpdateLoadValue(sliderScript.SliderValue() + 1f / (userIDs.Count * 2));
                Debug.Log("Found user: " + user.Username);
            } else {
                Debug.Log(result);
            }
        });
        yield break;
    }

    private IEnumerator FetchUsername(UserManager userManager, long userID) {
        userManager.GetUser(userID, (Result result, ref User user) => {
            if (result == Result.Ok) {
                usernameStrings.Add(user.Username + "#" + user.Discriminator);
                sliderScript.UpdateLoadValue(sliderScript.SliderValue() + 1f / (userIDs.Count * 2));
                Debug.Log("Found user: " + user.Username);
            } else {
                Debug.Log(result);
            }
        });
        yield break;
    }

    private IEnumerator AddUsernamesToCredits() {
        yield return new WaitUntil(() => usernameStrings.Count == userIDs.Count);
        for (int i = 0; i < usernameStrings.Count; i++) {
            if (i % 2 == 0) {
                GameObject leftHandText = Instantiate(leftHand, parent.transform);
                leftHandText.GetComponent<TMP_Text>().text = usernameStrings[i];
            } else {
                GameObject rightHandText = Instantiate(rightHand, parent.transform);
                rightHandText.GetComponent<TMP_Text>().text = usernameStrings[i];
            }
        }
        sliderScript.UpdateLoadValue(1f);
    }
}

