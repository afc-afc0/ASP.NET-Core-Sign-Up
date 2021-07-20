using UnityEngine;

public class User : MonoBehaviour
{
    [SerializeField]
    private UserData user;

    private void Start()
    {
        StartCoroutine(WebController.Instance.HttpRequestJson("http://localhost:5000/User/SignUp" , user));
    }
}


[System.Serializable]
public class UserData
{
    public string userName;
    public string password;
    public string confirmationPassword;
    public string email;
    public string confirmatationEmail;
}
