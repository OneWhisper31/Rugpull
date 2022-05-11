using FirebaseWebGL.Examples.Utils;
using FirebaseWebGL.Scripts.FirebaseBridge;
using FirebaseWebGL.Scripts.Objects;
using TMPro;
using UnityEngine;

public class AuthHandler : MonoBehaviour
{
    //used to log in event
    public AuthHandlerUI UI;

    [Header("Sing In")]
    public TMP_InputField emailInputFieldSingIn;
    public TMP_InputField passwordInputFieldSingIn;
    public TextMeshProUGUI outputTextSingIn;

    [Header("Sing Up")]
    public TMP_InputField emailInputFieldSingUp;
    public TMP_InputField passwordInputFieldSingUp;
    public TMP_InputField confirmPasswordInputFieldSingUp;
    public TextMeshProUGUI outputTextSingUp;

    bool _isLogingIn = true;

    FirebaseUser userData;
    bool iscalled;//used to call it once

    private void Start()
    {
        if (Application.platform != RuntimePlatform.WebGLPlayer)
        {
            DisplayError("The code is not running on a WebGL build; as such, the Javascript functions will not be recognized.");
            return;
        }
        // if(databaseHandler==null)//if in menu, and auth null, find it
        //     databaseHandler= GameObject.FindObjectOfType<DatabaseHandler>();
        // Debug.Log(databaseHandler);
        //FirebaseAuth.OnAuthStateChanged(gameObject.name, "DisplayUserInfo", "DisplayInfo");
    }

    private void Update()
    {
        if (userData != null&&iscalled==false)
        {
            iscalled=true;
            UI.OnUserLogIn();

            GameObject.FindObjectOfType<DatabaseHandler>().GetUserData(userData);
        }
    }

    public void SingInVerification()
    {//checks before send to server
        var emailText = emailInputFieldSingIn.text;
        var passwordtext = passwordInputFieldSingIn.text;

        //email comprobation
        if (EmailVerification(emailText))
        {

            //password comprobation
            if (passwordtext.Length >= 6 && passwordtext.IndexOf(" ") == -1)
            {
                SignInWithEmailAndPassword();
            }
            else outputTextSingIn.text = "6 characters minimum and no spaces in the password";
        }
        else outputTextSingIn.text = "Insert a valid email";

    }
    public void SingUpVerification()
    {//checks before send to server

        var emailText = emailInputFieldSingUp.text;
        var passwordtext = passwordInputFieldSingUp.text;
        var confirmPasswordtext = confirmPasswordInputFieldSingUp.text;
        if (EmailVerification(emailText))
        {

            //password comprobation
            if (passwordtext.Length >= 6 && passwordtext.IndexOf(" ") == -1)
            {
                if (confirmPasswordtext == passwordtext)
                    CreateUserWithEmailAndPassword();
                else outputTextSingUp.text = "Passwords do not match";
            }
            else outputTextSingUp.text = "6 characters minimum and no spaces in the password";
        }
        else outputTextSingUp.text = "Insert a valid email";
    }

    bool EmailVerification(string emailText)
    {
        if (emailText.Length != 0 && emailText.IndexOf("@") != -1 && emailText.IndexOf(".") != -1/*Exists in the mail*/
        && emailText.IndexOf("@") < emailText.IndexOf(".")/*@comes first than the .*/
        && emailText.IndexOf("@") != emailText.IndexOf(".") - 1/*They need to be spaced*/
        && emailText.IndexOf("@") != 0/*@ isnt first*/) return true;
        else return false;
    }

    public void IsLogingUp(bool isLogingIn)
    {
        _isLogingIn = isLogingIn;
        emailInputFieldSingIn.text = "";
        passwordInputFieldSingIn.text = "";
        outputTextSingIn.text = "";
        emailInputFieldSingUp.text = "";
        passwordInputFieldSingUp.text = "";
        confirmPasswordInputFieldSingUp.text = "";
        outputTextSingUp.text = "";
    }
    void SignInWithEmailAndPassword() =>
        FirebaseAuth.SignInWithEmailAndPassword(emailInputFieldSingIn.text, passwordInputFieldSingIn.text, gameObject.name, "DisplayInfo", "DisplayErrorObject");


    void CreateUserWithEmailAndPassword() =>
        FirebaseAuth.CreateUserWithEmailAndPassword(emailInputFieldSingUp.text, passwordInputFieldSingUp.text, gameObject.name, "DisplayInfo", "DisplayErrorObject");



    public void DisplayUserInfo(string user)//saves userData
    {
        userData = StringSerializationAPI.Deserialize(typeof(FirebaseUser), user) as FirebaseUser;
        //DisplayData(parsedUser);/*$"Email: {parsedUser.email}, UserId: {parsedUser.uid}, EmailVerified: {parsedUser.isEmailVerified}"*/
    }

    public void DisplayData(string data)
    {
        if (_isLogingIn)
        {
            //outputTextSingIn.color = outputTextSingIn.color == Color.green ? Color.blue : Color.green;
            outputTextSingIn.text = data;
        }
        else
        {
            //outputTextSingUp.color = outputTextSingUp.color == Color.green ? Color.blue : Color.green;
            outputTextSingUp.text = data;
        }
    }

    public void DisplayInfo(string info)
    {//shows Success
        if (_isLogingIn)
        {
            //outputTextSingIn.color = Color.white;
            outputTextSingIn.text = info;
        }
        else
        {
            //outputTextSingUp.color = Color.white;
            outputTextSingUp.text = info;
        }
        FirebaseAuth.OnAuthStateChanged(gameObject.name, "DisplayUserInfo", "DisplayInfo");
    }
    public void DisplayErrorObject(string error)//show error
    {
        var parsedError = StringSerializationAPI.Deserialize(typeof(FirebaseError), error) as FirebaseError;
        DisplayError(parsedError.message);
    }
    public void DisplayError(string error)
    {
        if (_isLogingIn)
        {
            //outputTextSingIn.color = Color.red;
            outputTextSingIn.text = error;
        }
        else
        {
            //outputTextSingUp.color = Color.red;
            outputTextSingUp.text = error;
        }
    }
    public void GetUserData(FirebaseUser _userData) => userData = _userData;
}
