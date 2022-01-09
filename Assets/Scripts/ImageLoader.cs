using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ImageLoader : MonoBehaviour
{

    public Image image;

    private string hostURL;
    private string targetImage;
    private string targetURL;
    
    private void Start()
    {
        ConfigureImageTarget();
        StartCoroutine(LoadImagesFromURL());
    }

    private void ConfigureImageTarget()
    {
        hostURL = "http://rayqube.com/projects/games/webar/";
        targetImage = gameObject.name;
        targetURL = hostURL + targetImage + ".jpg";
    }

    private IEnumerator LoadImagesFromURL()
    {
        WWW www = new WWW(targetURL);
        yield return www;

        image.sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0, 0));
        image.preserveAspect = true;
    }

}
