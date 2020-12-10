using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

public class Login : MonoBehaviour
{
    private string userEmail;
    private string userPassword;

    private string username;

    public GameObject loginPanel;

    public void Start()
    {
        
        if (string.IsNullOrEmpty(PlayFabSettings.TitleId)){
            PlayFabSettings.TitleId = "1A0E8";
        }

        // to delete all the remembered password
        //PlayerPrefs.DeleteAll();

        //To check wheter player has login before or not
        if (PlayerPrefs.HasKey("EMAIL")){
            //to get the stored EMAIL and PASSWORD
            userEmail = PlayerPrefs.GetString("EMAIL");
            userPassword = PlayerPrefs.GetString("PASSWORD");

            var request = new LoginWithEmailAddressRequest { Email = userEmail, Password = userPassword};
            PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginFailure);
        }
    }
    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Congratulations, you made your first successful API call!");
        Debug.Log("Welcome " + userEmail);
        PlayerPrefs.SetString("EMAIL", userEmail);
        PlayerPrefs.SetString("PASSWORD", userPassword);
        loginPanel.SetActive(false);
    }

    private void OnRegisterSuccess(RegisterPlayFabUserResult result){
        Debug.Log("Congratulations, you made your first successful API call!");
        // to remember user email and password
        PlayerPrefs.SetString("EMAIL", userEmail);
        PlayerPrefs.SetString("PASSWORD", userPassword);
        loginPanel.SetActive(false);
    }

    private void OnLoginFailure(PlayFabError error)
    {
        var registerRequest = new RegisterPlayFabUserRequest {Email = userEmail, Password = userPassword, Username = username};
        PlayFabClientAPI.RegisterPlayFabUser(registerRequest, OnRegisterSuccess, OnRegisterFailure);
    }

    private void OnRegisterFailure(PlayFabError error){
        Debug.Log(error.GenerateErrorReport());
    }

    public void GetUserEmail(string emailIn){
        userEmail = emailIn;
    }
    public void GetUserPassword(string passwordIn){
        userPassword = passwordIn;
    }

    public void GetUsername(string usernameIn){
        username = usernameIn;
    }

    public void OnClickLogin(){
        var request = new LoginWithEmailAddressRequest { Email = userEmail, Password = userPassword};
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginFailure);
    }
}