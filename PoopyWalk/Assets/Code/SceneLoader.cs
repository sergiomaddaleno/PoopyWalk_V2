using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string sceneName; // El nombre de la escena que deseas cargar

    // Esta función se llamará cuando se haga clic en el botón
    public void LoadSceneOnClick()
    {
        SceneManager.LoadScene("LevelSelectors");
    }
}