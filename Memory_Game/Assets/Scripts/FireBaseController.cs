using Firebase;
using Firebase.Auth;
using Firebase.Firestore;
using UnityEngine;
using System.Threading.Tasks;

public class FirebaseInit : MonoBehaviour
{
    void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                Debug.Log("Firebase dependencies OK");
                // Initialize Firebase Auth and Firestore here or keep references
                FirebaseAuth auth = FirebaseAuth.DefaultInstance;
                FirebaseFirestore firestore = FirebaseFirestore.DefaultInstance;
            }
            else
            {
                Debug.LogError($"Could not resolve all Firebase dependencies: {dependencyStatus}");
            }

           
        });
    }
    
  
}