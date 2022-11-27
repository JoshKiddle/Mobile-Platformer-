using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;


public class ShareButton : MonoBehaviour
{
	private string shareMessage;
	private int currentLevel;

	void Start()
    {
		currentLevel = SceneManager.GetActiveScene().buildIndex;
		Debug.Log(currentLevel);

	}

	public void ClickedShare()
    {
		shareMessage = "I just completed level " + currentLevel + " in Sector 29!";
		StartCoroutine(TakeScreenshotAndShare());
    }

	private IEnumerator TakeScreenshotAndShare()
	{
		yield return new WaitForEndOfFrame();

		Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
		ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
		ss.Apply();

		string filePath = Path.Combine(Application.temporaryCachePath, "shared img.png");
		File.WriteAllBytes(filePath, ss.EncodeToPNG());

		// To avoid memory leaks
		Destroy(ss);

		new NativeShare().AddFile(filePath)
			.SetSubject("Sector 29").SetText(shareMessage).SetUrl(" " + "https://joshkiddle.github.io/")
			.SetCallback((result, shareTarget) => Debug.Log("Share result: " + result + ", selected app: " + shareTarget))
			.Share();

		
	}
}
