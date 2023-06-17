using System;
using System.Collections;
using System.Collections.Generic;
using LootLocker.Requests;
using UnityEngine;

public class CreatePlayer : MonoBehaviour
{
    public void Start()
    {
        sendFakeData();
        //loginWithFakeData();
    }

    public void loginWithFakeData()
    {
        // This code should be placed in a handler when user clicks the login button.
        string email = "user@lootlocker.io";
        string password = "password here";
        bool rememberMe = true;
        LootLockerSDKManager.WhiteLabelLoginAndStartSession(email, password, rememberMe, response =>
        {
            if (!response.success)
            {
                if (!response.LoginResponse.success) {
                    Debug.Log("error while logging in");
                } else if (!response.SessionResponse.success) {
                    Debug.Log("error while starting session");
                }
                return;
            }

            // Handle Returning Player
        });
    }

    public void sendFakeData()
    {
        string email = "1user@lootlocker.io";
        string password = "1password here";
        LootLockerSDKManager.WhiteLabelSignUp(email, password, (response) =>
        {
            if (!response.success)
            {
                Debug.Log("error while creating user");

                return;
            }

            Debug.Log("user created successfully");
        });
    }
}
