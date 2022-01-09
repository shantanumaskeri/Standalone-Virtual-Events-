using UnityEngine;
using Affdex;

public class WebcamView : MonoBehaviour 
{

    public Affdex.CameraInput cameraInput;
	[HideInInspector]
    public Affdex.VideoFileInput movieInput;

	public Texture white;

	public void ActivateView()
	{
		if (!AffdexUnityUtils.ValidPlatform())
			return;

		Texture texture = movieInput != null ? movieInput.Texture : cameraInput.Texture;

		if (texture == null)
			return;

		this.GetComponent<MeshRenderer>().material.mainTexture = texture;

		if (cameraInput != null)
		{
			float videoRotationAngle = -cameraInput.videoRotationAngle;
			transform.rotation = transform.rotation * Quaternion.AngleAxis(videoRotationAngle, Vector3.forward);
		}
	}

	public void DeactivateView()
	{
		this.GetComponent<MeshRenderer>().material.mainTexture = white;
	}

}