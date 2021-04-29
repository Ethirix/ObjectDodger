using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Discord;
using TMPro;

public class GetUsernamesFromIDs : MonoBehaviour {
    
    private Discord.Discord discord = new Discord.Discord(837028487201554483, (UInt64) CreateFlags.NoRequireDiscord);
    [SerializeField] private List<long> userIDs = new List<long>();
    [SerializeField] private GameObject leftHand;
    [SerializeField] private GameObject rightHand;
    [SerializeField] private GameObject parent;
    private List<string> usernameStrings = new List<string>();

    private void Start() {
        UserManager userManager = discord.GetUserManager();
        StartUsernameFetch(userManager);
    }

    private void StartUsernameFetch(UserManager userManager) {
        foreach (long userID in userIDs) {
            StartCoroutine(FetchUsername(userManager, userID));
        }
        StartCoroutine(AddUsernamesToCredits());
    }

    private IEnumerator FetchUsername(UserManager userManager, long userID) {
        userManager.GetUser(userID, (Result result, ref User user) => {
            if (result == Result.Ok) {
                usernameStrings.Add(user.Username + "#" + user.Discriminator);
                Debug.Log("Found user: " + user.Username);
            } else {
                Debug.Log(result);
            }
        });
        yield break;
    }
    
    private void Update()
    {
        discord.RunCallbacks();
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
    }
}

