using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class SceneController : Singleton<SceneController>
{
    private Transform canvasTransform;
    private GameObject overlayObject;
    private Image overlayImage;

    public event Action OnStartTransition;
    public event Action AfterTransition;
    public event Action OnExitTransition;

    /// <summary>
    /// Simple instant loading of nextscene without effects
    /// </summary>
    /// <param name="name"></param>
    public IEnumerator LoadSceneAdditive(string name)
    {
        AsyncOperation asyncLoadLevel = SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);

        // Wait until the level finish loading
        while (!asyncLoadLevel.isDone)
            yield return null;
        // Wait a frame so every Awake and Start method is called
        yield return new WaitForEndOfFrame();
    }
    public void LoadScene(string name)
    {
        SceneManager.LoadSceneAsync(name);
    }

    public void LoadScene(int sceneBuild)
    {
        SceneManager.LoadSceneAsync(sceneBuild);
    }

    public void ReloadLoadedScene(int loadedSceneNumber, LoadSceneMode mode)
    {
        Scene currentGame = LoadedScenes()[loadedSceneNumber];
        SceneManager.UnloadSceneAsync(currentGame.name);
        SceneManager.LoadScene(currentGame.name, mode);
    }

    /// <summary>
    /// Unload the loadedSceneNumer and reload it with a new scene
    /// </summary>
    /// <param name="loadedSceneNumber">Actively loaded scene Id</param>
    /// <param name="nextScene">Scene to Load</param>
    public void UnloadLoadSceneAsync(int loadedSceneNumber, string nextScene)
    {
        if(loadedSceneNumber < LoadedScenes().Length)
        {
            Scene currentGame = LoadedScenes()[loadedSceneNumber];
            SceneManager.UnloadSceneAsync(currentGame.name);
        }
        SceneManager.LoadSceneAsync(nextScene, LoadSceneMode.Additive);
    }


    private void OnEnable()
    {
        DontDestroyOnLoad(this.gameObject);
        CreateFadeImage(Color.black);
    }

    /// <summary>
    /// Transitions to a scene.
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="outDuration"></param>
    /// <param name="transitionOutType"></param>
    /// <param name="inDuration"></param>
    /// <param name="transitionInType"></param>
    public void TransitionToScene(string scene, 
                                  float outDuration =1.0f,
                                  SceneTransition transitionOutType = SceneTransition.FadeOut,
                                  float inDuration = 1.0f,
                                  SceneTransition transitionInType = SceneTransition.FadeIn)
    {
        OnStartTransition?.Invoke();
        Sequence transitionSequence = DOTween.Sequence();
        switch (transitionOutType)
        {
            case SceneTransition.FadeOut:
                transitionSequence.Append(overlayImage.DOFade(1, outDuration).SetEase(Ease.Linear));
                break;

            case SceneTransition.SlideRight:
                break;
            case SceneTransition.SlideUp:
                break;
            default:
                break;
        }

        transitionSequence.AppendCallback(() => SceneManager.LoadScene(scene));

        switch (transitionInType)
        {
            case SceneTransition.FadeIn:
                transitionSequence.Append(overlayImage.DOFade(0, outDuration).SetEase(Ease.Linear));
                break;
            case SceneTransition.FadeOut:
                break;
            case SceneTransition.SlideRight:
                break;
            case SceneTransition.SlideUp:
                break;
            default:
                break;
        }

        transitionSequence.AppendCallback(() => OnExitTransition?.Invoke());

    }

    /// <summary>
    /// Creates the gameobject, canvas and image for fadeing
    /// </summary>
    /// <param name="color">FadeImage Color</param>
    private void CreateFadeImage(Color color)
    {
        overlayObject = new GameObject("SceneSwitchOverlay");
        overlayImage = overlayObject.AddComponent<Image>();

        //put the image on a seperate canvas for sorting 
        Canvas c = gameObject.AddComponent<Canvas>();
        c.renderMode = RenderMode.ScreenSpaceOverlay;
        c.sortingOrder = 999;
        canvasTransform = c.transform;

        //set color
        overlayImage.DOColor(color, 0.0f);
        overlayImage.DOFade(0, 0.0f);

        //set size
        overlayObject.GetComponent<RectTransform>().SetParent(this.gameObject.transform);
        overlayObject.GetComponent<RectTransform>().position = canvasTransform.position;
        overlayObject.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
        overlayObject.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
        overlayObject.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);

        //should be below setparent for somereason
        c.overrideSorting = true;
    }

    /// <summary>
    /// Gets the actively loaded scenes
    /// </summary>
    /// <returns>List of actively loaded scenes</returns>
    private Scene[] LoadedScenes()
    {
        int countLoaded = SceneManager.sceneCount;
        Scene[] loadedScenes = new Scene[countLoaded];

        for (int i = 0; i < countLoaded; i++)
        {
            loadedScenes[i] = SceneManager.GetSceneAt(i);
        }

        return loadedScenes;
    }

    // private Transform GetCanvasTransform() => GameObject.Find("Canvas")?.transform == null ? ((Canvas)FindObjectOfType(typeof(Canvas), true)).transform : GameObject.Find("Canvas").transform;
    private Transform GetCanvasTransform() => GameObject.Find("Canvas").transform;
    public int GetActiveScene() => SceneManager.GetSceneAt(0).buildIndex;
}

public enum SceneTransition
{
    FadeIn,
    FadeOut,
    SlideRight,
    SlideUp,
}
