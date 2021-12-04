using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterUI_SpawnMovingImage : MonoBehaviour
{
    [SerializeField]
    GameWorldUIController GameWorldUIController;

    public Image movingImagePrefab;

    public Sprite spriteToAdd;

    public string textToAdd;
    public void Spawn_MovingImage()
    {
       var image = Instantiate(movingImagePrefab, this.transform.position, Quaternion.Euler(0, 0, 0), GameWorldUIController.GetWorldCanvas().transform);
        image.sprite = spriteToAdd;

        TMPro.TMP_Text text = image.GetComponentInChildren<TMPro.TMP_Text>();

        if(text != null)
        {
            text.text = textToAdd;
        }
    }
}
