using System;
using UnityEngine;
using Discord;
using TMPro;

public class LoadMyDiscordTagScript : MonoBehaviour
{
    [SerializeField] private TMP_Text text;

    private Discord.Discord discord;
        
    private void OnEnable()
    {
        discord = new Discord.Discord(837028487201554483, (UInt64) CreateFlags.NoRequireDiscord);
        UserManager userManager = discord.GetUserManager();
        userManager.GetUser(244468460430557184, (Result result, ref User user) => {
            if (result == Result.Ok) {
                text.text = "Created by: " + user.Username + "#" + user.Discriminator;
            } else {
                Debug.Log(result);
            }
        });
    }

    private void Update()
    {
        discord.RunCallbacks();
    }
}
