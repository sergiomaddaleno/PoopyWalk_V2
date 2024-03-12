using UnityEngine;
using UnityEngine.UI;

public class Changeimages : MonoBehaviour
{
     public Sprite[] sprites; // Array de sprites
    public float switchInterval = 1f; // Intervalo de cambio en segundos
    private int currentIndex = 0; // Índice del sprite actual
    private Image image; // Componente Image del objeto

    void Start()
    {
        image = GetComponent<Image>(); // Obtener el componente Image del objeto

        // Comenzar la rutina para cambiar los sprites
        InvokeRepeating("SwitchSprites", 0f, switchInterval);
    }

    void SwitchSprites()
    {
        // Cambiar al siguiente índice de sprite
        currentIndex = (currentIndex + 1) % sprites.Length;

        // Aplicar el nuevo sprite al Image
        image.sprite = sprites[currentIndex];
    }
}
