using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using System.Threading.Tasks;
using System;

public class controller : MonoBehaviour
{
    public GameObject loginPanel, signupPanel, profilePanel, notificationPanel;
    public InputField loginEmail, loginPassword, signupEmail, signupPassword, signupCPassword, signupName;
    public Text notif_title_text, notif_message_text, profileUserName_text;
    public Toggle rememberMe;
    bool isSignIn = false;

    Firebase.Auth.FirebaseAuth auth;
    Firebase.Auth.FirebaseUser user;

    void Start()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
        var dependencyStatus = task.Result;
        if (dependencyStatus == Firebase.DependencyStatus.Available) {
            // Create and hold a reference to your FirebaseApp,
            // where app is a Firebase.FirebaseApp property of your application class.
            InitializeFirebase();

            // Set a flag here to indicate whether Firebase is ready to use by your app.
        } else {
            UnityEngine.Debug.LogError(System.String.Format(
            "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
            // Firebase Unity SDK is not safe to use here.
        }
        });
    }

    public void OpenLoginPanel()
    {
        loginPanel.SetActive(true);
        signupPanel.SetActive(false);
        profilePanel.SetActive(false);
    }

    public void OpenSignUpPanel()
    {
        loginPanel.SetActive(false);
        signupPanel.SetActive(true);
        profilePanel.SetActive(false);
    }

    public void OpenProfilePanel()
    {
        loginPanel.SetActive(false);
        signupPanel.SetActive(false);
        profilePanel.SetActive(true);
    }

    private void showNotificationMessage(string title, string message)
    {
        notif_title_text.text = "" + title;
        notif_message_text.text = "" + message;

        notificationPanel.SetActive(true);
    }
    
    public void CloseNotifPanel()
    {
        notif_title_text.text = "";
        notif_message_text.text = "";
        notificationPanel.SetActive(false);
    }

    public void logout()
    {
        auth.SignOut();
        profileUserName_text.text = "";
        OpenLoginPanel();
    }

    public void LoginUser()
    {
        if(string.IsNullOrEmpty(loginEmail.text) && string.IsNullOrEmpty(loginPassword.text))
        {
            showNotificationMessage("Error", "Input Field(s) Empty!");
            return;
        }
        //Do Login
        signinUser(loginEmail.text, loginPassword.text);
    }

    public void SignUpUser()
    {
        if(string.IsNullOrEmpty(signupEmail.text) && string.IsNullOrEmpty(signupPassword.text) && string.IsNullOrEmpty(signupCPassword.text))
        {
            showNotificationMessage("Error", "Input Field(s) Empty!");
            return;
        }
        //Do Signup
        createUser(signupEmail.text, signupPassword.text, signupName.text);
    }
      
    void createUser(string email, string password, string userName)
    {
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task => {
        if (task.IsCanceled) {
            Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
            return;
        }
        if (task.IsFaulted) {
            Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
            foreach(Exception exception in task.Exception.Flatten().InnerExceptions)
            {
                Firebase.FirebaseException firebaseEx = exception as Firebase.FirebaseException;
                if (firebaseEx != null)
                {
                    var errorCode = (AuthError)firebaseEx.ErrorCode;
                    showNotificationMessage("Error",GetErrorMessage(errorCode));
                }
            }
            return;
        }

        // Firebase user has been created.
        Firebase.Auth.FirebaseUser newUser = task.Result;
        Debug.LogFormat("Firebase user created successfully: {0} ({1})",
            newUser.DisplayName, newUser.UserId);

        UpdateUserProfile(userName);
        });
    }   

    public void signinUser(string email, string password)
    {
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task => {
        if (task.IsCanceled) {
            Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
            return;
        }
        if (task.IsFaulted) {
            Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
            foreach(Exception exception in task.Exception.Flatten().InnerExceptions)
            {
                Firebase.FirebaseException firebaseEx = exception as Firebase.FirebaseException;
                if (firebaseEx != null)
                {
                    var errorCode = (AuthError)firebaseEx.ErrorCode;
                    showNotificationMessage("Error",GetErrorMessage(errorCode));
                }
            }
                    
            return;
        }

        Firebase.Auth.FirebaseUser newUser = task.Result;
        Debug.LogFormat("User signed in successfully: {0} ({1})",
            newUser.DisplayName, newUser.UserId);
            
            profileUserName_text.text = "" + newUser.DisplayName;
            OpenProfilePanel();
        });
    }

    void InitializeFirebase() {
    auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
    auth.StateChanged += AuthStateChanged;
    AuthStateChanged(this, null);
    }

    void AuthStateChanged(object sender, System.EventArgs eventArgs) 
    {
        if (auth.CurrentUser != user) 
        {
            bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;
            if (!signedIn && user != null) {
            Debug.Log("Signed out " + user.UserId);
            }
            user = auth.CurrentUser;
            if (signedIn) {
            Debug.Log("Signed in " + user.UserId);
            isSignIn = true;
            }
        }
    }

    bool isSigned = false;
    void Update()
    {
        if(isSignIn)
        {
            if(!isSigned)
            {
                isSigned = true;
                profileUserName_text.text = "" + user.DisplayName;
                OpenProfilePanel();
            }
        }
    }

    void OnDestroy() 
    {
        auth.StateChanged -= AuthStateChanged;
        auth = null;
    } 

    void UpdateUserProfile(string username)
    {
        Firebase.Auth.FirebaseUser user = auth.CurrentUser;
        if (user != null) {
        Firebase.Auth.UserProfile profile = new Firebase.Auth.UserProfile {
            DisplayName = username,
            PhotoUrl = new System.Uri("https://via.placeholder.com/150"),
        };
        user.UpdateUserProfileAsync(profile).ContinueWithOnMainThread(task => {
            if (task.IsCanceled) {
            Debug.LogError("UpdateUserProfileAsync was canceled.");
            return;
            }
            if (task.IsFaulted) {
            Debug.LogError("UpdateUserProfileAsync encountered an error: " + task.Exception);
            return;
            }

            Debug.Log("User profile updated successfully.");
            showNotificationMessage("Alert", "Account Successfully Created!");
        });
        }
    }

    private static string GetErrorMessage(AuthError errorCode)
    {
        var message = "";
        switch (errorCode)
        {
            case AuthError.AccountExistsWithDifferentCredentials:
            message = "Account Doesn't Exist";
            break;
            case AuthError.MissingPassword:
                message = "Missing Password";
                break;
            case AuthError.WeakPassword:
                message = "Password is Weak";
                break;
            case AuthError.WrongPassword:
                message = "Password is Incorrect";
                break;
            case AuthError.EmailAlreadyInUse:
                message = "Email Already in Use";
                break;
            case AuthError.InvalidEmail:
                message = "Invalid Email";
                break;
            case AuthError.MissingEmail:
                message = "E-mail is missing";
                break;
            default:
                message = "Error Occured";
                break;
        }
        return message;
    }
}
