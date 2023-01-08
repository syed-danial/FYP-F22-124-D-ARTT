using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FirebaseController : MonoBehaviour
{
    public GameObject loginpPanel,registerpPanel, forgetPasswordPanel, notificationPanel;

    public TMP_InputField loginpEmail,loginpPassw, registerpEmail, registerpPassw, registerpUName, forgetPassEmail;

    public TMP_Text notif_Title_Text, notif_Message_Text, profName_Text,profEmail_Text;

    public Toggle rememberMe;

  
    // Start is called before the first frame update
    

    public void OpenLoginpane1()

    {
        loginpPanel.SetActive(true);
        registerpPanel.SetActive(false);
        forgetPasswordPanel.SetActive(false);
    }

    // Update is called once per frame
    public void OpenRegisterPanel()
    {

        loginpPanel.SetActive(false);
        registerpPanel.SetActive(true);
        forgetPasswordPanel.SetActive(false);
    }

    public void OpenProfilePanel()
    {

        loginpPanel.SetActive(false);
        registerpPanel.SetActive(false);
        forgetPasswordPanel.SetActive(false);   
    }

    public void OpenForgetPassPanel()
    {
        loginpPanel.SetActive(false);
        registerpPanel.SetActive(false);
        forgetPasswordPanel.SetActive(true);   
    }

    public void LoginpUser()
    {
        if( string.IsNullOrEmpty(loginpEmail.text) && string.IsNullOrEmpty(loginpPassw.text))
        {
            showNotificationMess("Error", "Fields Empty");
            return;
        }
        
    }

    public void RegisterpUser()
    {
        if( string.IsNullOrEmpty(registerpEmail.text) && string.IsNullOrEmpty(registerpPassw.text) && string.IsNullOrEmpty(registerpUName.text))
        {
            showNotificationMess("Error", "Fields Empty");
            return;
        }

        
    }

    public void forgetPass()
    {
        if( string.IsNullOrEmpty(forgetPassEmail.text))
        {
            showNotificationMess("Error", "Fields Empty");
            return;
        }
    }

    private void showNotificationMess(string title, string message)
    {
        notif_Title_Text.text = "" + title;
        notif_Message_Text.text = "" + message;

        notificationPanel.SetActive(true);
    }

    public void CloseNotif_Panel()
    {
        notif_Title_Text.text = "";
        notif_Message_Text.text = "";

        notificationPanel.SetActive(false);
    }

    public void logout()
    {
        profName_Text.text="";
        profEmail_Text.text="";
        
    }

    

   
    

}
 ;








    


