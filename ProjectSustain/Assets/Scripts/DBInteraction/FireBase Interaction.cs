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
        firestore.Document(playerpath).SetAsync(playerData);
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
