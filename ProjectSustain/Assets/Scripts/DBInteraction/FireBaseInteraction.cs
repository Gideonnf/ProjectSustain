using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Firestore;
using Firebase.Extensions;
using UnityEngine.Assertions;

public class FireBaseInteraction : MonoBehaviour
{
    public string progresspath = "progress";
    public string playerpath = "players";
    public string scorepath = "scores";
    public string key = "key";

    public void ReadData()
    {
        var firestore = FirebaseFirestore.DefaultInstance;

        firestore.Document(playerpath+key).GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            Assert.IsNull(task.Exception);

            var playerData = task.Result.ConvertTo<player_info>();

            
        });
    }

    public void WriteData()
    {
        var playerData = new player_info();
        playerData.p_id = 1;
        playerData.p_name = "name";
        playerData.p_username = "username";
        playerData.p_password = "password";

        var firestore = FirebaseFirestore.DefaultInstance;
        firestore.Document(scorepath).SetAsync(playerData);
    }

    public void WriteScores()
    {

        var playerData = new player_scores();
        playerData.p_id = 1;
        playerData.s_id = 1;
        playerData.s_level = 1;
        playerData.s_score = 1000;

        var firestore = FirebaseFirestore.DefaultInstance;
        firestore.Document(playerpath).SetAsync(playerData);
    }

    public int login(string key = null, string password = null)
    {
        if(key == null || password == null)
        {
            return -1;
        }
        var firestore = FirebaseFirestore.DefaultInstance;
        firestore.Document(playerpath + key).GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            Assert.IsNull(task.Exception);


            var playerData = task.Result.ConvertTo<player_info>();
            if (playerData.p_password == password)
            {
                return playerData.p_id;
            }
            else
            {
                return -1;
            }

        });
        return -1;

    }


    [FirestoreData]
    public struct player_progress
    {
        [FirestoreProperty]
        public int pr_id { get; set; }

        [FirestoreProperty]
        public int p_id { get; set; }

        [FirestoreProperty]
        public int pr_dish_progress { get; set; }

        [FirestoreProperty]
        public int pr_ingredient_progress { get; set; }
    }

    [FirestoreData]
    public struct player_info
    {
        [FirestoreProperty]
        public int p_id { get; set; }

        [FirestoreProperty]
        public string p_name { get; set; }

        [FirestoreProperty]
        public string p_username { get; set; }

        [FirestoreProperty]
        public string p_password { get; set; }
    }

    [FirestoreData]
    public struct player_scores
    {
        [FirestoreProperty]
        public int s_id { get; set; }

        [FirestoreProperty]
        public int p_id { get; set; }

        [FirestoreProperty]
        public int s_level { get; set; }

        [FirestoreProperty]
        public float s_score { get; set; }
    }

}
