using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class menu : MonoBehaviour
{
    public static menu instance;
    
    public int n;
    public Dropdown dropdown;
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void start()
    {

        n = int.Parse(dropdown.options[dropdown.value].text);
        SceneManager.LoadScene("main");
    }
    
}
