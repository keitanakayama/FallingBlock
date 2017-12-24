﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;




/***
 * 
 * 他のソースから
 * Transitioner.Instance.TransitionToScene("Sceneの名前")で遷移できると思う;
 * 
 ***/
public class Transitioner : MonoBehaviour
{
    const float MinimumTransitionTime = 0.1f;
    const float MaximumTransitionTime = 5.0f;
    const float MinimumTransitionBlockWidth = 1;
    const float MaximumTransitionBlockWidth = 60;

    [Tooltip("The number of transition blocks that will appear horizontally on the screen. The number that appear vertically will automatically be calculated from your screen resolution")]
    [Range(MinimumTransitionBlockWidth, MaximumTransitionBlockWidth)]
    public int _widthOfTransitionInBlocks = 9;

    [Space]

    [Tooltip("The transition block that will be used in this transition")]
    public GameObject _transitionBlockPrefab;
    [Tooltip("The sprite to be used for each of the transition blocks")]
    public Sprite _transitionBlockSprite;
    [Tooltip("The color to be used for each of the transition blocks")]
    public Color _transitionBlockColor = Color.white;
    [Tooltip("The time it takes for a transition block to complete its animation")]
    [Range(MinimumTransitionTime, MaximumTransitionTime)]
    public float _transitionBlockAnimationTime = 1.0f;

    [Space]

    [Tooltip("The transition order to be used when transitioning")]
    public GameObject _transitionOrderPrefab;
    [Tooltip("The time it takes to place all of the transition blocks down in this transition.")]
    [Range(MinimumTransitionTime, MaximumTransitionTime)]
    public float _transitionTime = 1.0f;

    [Space]

    [Tooltip("If true this transitioner will automatically transition into the scene when the scene is loaded. If it's false the transition will stay in front of the screen until you call Transitioner.Instance.FinishTransition(); and then the transition will finish. This will let you load assets in the background and transition in when you're ready.")]
    public bool _automaticallyTransitionIn = true;

    #region singleton

    [Tooltip("If true this transitioner will be a singleton that follows you into every scene. You will only need to place one down in your first scene if this is true. Otherwise, you'll need a transitioner in each scene you make. Either way you will call it the same way using Transitioner.Instance.TransitionToScene(LevelName); or Transitioner.Instance.TransitionToScene(levelNumber);.")]
    public bool dontDestroyOnLoad = true;

    private static Transitioner instance;

    public static Transitioner Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<Transitioner>();
                if (instance == null)
                {
                    Debug.LogError("No Transitioner was found in this scene. Make sure you place one in the scene.");
                    return instance;
                }
            }
            return instance;
        }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = GameObject.FindObjectOfType<Transitioner>();
            if (instance == null)
            {
                Debug.LogError("No Transitioner was found in this scene. Make sure you place one in the scene.");
                return;
            }
            if (dontDestroyOnLoad)
            {
                DontDestroyOnLoad(instance.gameObject);
            }
        }
        else
        {
            DestroyNonInstanceTransitioners();
        }
    }

    public void OnApplicationQuit()
    {
        instance = null;
    }

    private bool DestroyNonInstanceTransitioners()
    {
        if (this != instance)
        {
            Debug.Log("Found another Transitioner script, destroying it because this should be the only instance");
            Destroy(this.gameObject);
            return true;
        }
        return false;
    }

    #endregion

    [Space]
    [Range(1, 15)]
    [Tooltip("Only change this if you're having performance issues. This will skip every X transition block updates. (If this is at 2 every other update will be skipped, at 3 every third update is skipped, etc...) Leave this at one to do nothing")]
    public int _skipEveryXBlockUpdates = 1;

    private bool _canTransition = true;
    private bool _transitionInTriggered = false;

    private GameObject _transitionOrdererObject;

    public void TransitionToScene(string sceneName)
    {
        if (_canTransition)
        {
            StartCoroutine(TransitionOutThenLoadScene(sceneName));
        }
    }

    private IEnumerator TransitionOutThenLoadScene(string sceneName)
    {
        yield return StartCoroutine(Transition(TransitionType.Out));
        SceneManager.LoadScene(sceneName);
    }

    public void TransitionToScene(int sceneNumber)
    {
        if (_canTransition)
        {
            StartCoroutine(TransitionOutThenLoadScene(sceneNumber));
        }
    }

    private IEnumerator TransitionOutThenLoadScene(int sceneNumber)
    {
        yield return StartCoroutine(Transition(TransitionType.Out));
        SceneManager.LoadScene(sceneNumber);
    }

    public void TransitionOutWithoutChangingScene()
    {
        if (_canTransition)
        {
            StartCoroutine(Transition(TransitionType.Out));
        }
    }

    public void TransitionInWithoutChangingScene()
    {
        Destroy(_transitionOrdererObject);
        StartCoroutine(ActualTransitionIn());
    }

    protected void OnLevelWasLoaded(int levelNumber)
    {
        if (!DestroyNonInstanceTransitioners())
        {
            _transitionInTriggered = false;
            StartCoroutine(ActualTransitionIn());
        }
    }

    public void FinishTransition()
    {
        _transitionInTriggered = true;
    }

    private IEnumerator ActualTransitionIn()
    {
        yield return StartCoroutine(Transition(TransitionType.In));
        Destroy(_transitionOrdererObject);
        _canTransition = true;
    }

    private IEnumerator Transition(TransitionType transitionType)
    {
        _canTransition = false;

        TransitionOrderBase transitionOrderer = MakeTransitionOrderer(transitionType);
        if(transitionOrderer == null)
        {
            Debug.LogError("Unable to set up transition properly please fix any warnings");
            yield break;
        }

        yield return new WaitForFixedUpdate();
        transitionOrderer.SetPercent(0.0f);

        if (transitionType == TransitionType.In && !_automaticallyTransitionIn)
        {
            yield return new WaitUntil(() => _transitionInTriggered);
        }

        float timer = 0.0f;
        while (timer < _transitionTime)
        {
            transitionOrderer.SetPercent(timer / _transitionTime);
            timer += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        transitionOrderer.SetPercent(1.0f);

        yield return new WaitForSeconds(_transitionBlockAnimationTime);
    }

    private TransitionOrderBase MakeTransitionOrderer(TransitionType transitionType)
    {
        if (_transitionBlockPrefab == null)
        {
            Debug.LogWarning("A transition block prefab hasn't been added to the transitioner. Please add one in the inspector.");
            return null;
        }
        if (_transitionOrderPrefab == null)
        {
            Debug.LogWarning("A transition block order hasn't been added to the transitioner. Please add one in the inspector.");
            return null;
        }

        TransitionSetup transitionSetup = GetComponent<TransitionSetup>();
        if (transitionSetup == null)
        {
            transitionSetup = gameObject.AddComponent<TransitionSetup>();
        }

        _transitionOrdererObject = transitionSetup.SetUpTransition(transitionType, _widthOfTransitionInBlocks, _transitionBlockAnimationTime, _transitionBlockSprite, _transitionBlockColor, _transitionBlockPrefab, _transitionOrderPrefab, _skipEveryXBlockUpdates);
        if (_transitionOrdererObject == null)
        {
            return null;
        }

        return _transitionOrdererObject.GetComponent<TransitionOrderBase>();
    }
}
