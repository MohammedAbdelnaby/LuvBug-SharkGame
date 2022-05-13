using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class FishManager : MonoBehaviour
{
    private float SpawnTimer;
    private float SpawnTimerMax;
    private GameObject Fish;

    private int score = 0;

    [SerializeField]
    private GameObject FishPreFab;

    [SerializeField]
    private Animator SharkAnimator;
    [SerializeField]
    private Text SharkText;

    private float delay = 3f;
    private int Level = 0;
    private Fish FishBrain;
    void Start()
    {
        SpawnTimerMax = 2f;
        SpawnTimer = SpawnTimerMax;
    }

    IEnumerator LoadLevelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(Level);
    }

    void Update()
    {
        SpawnTimer -= Time.deltaTime;
        //generate a new if when one is destoryed
        if (Fish == null && SpawnTimer <= 0.0f)
        {
            Fish = Instantiate(FishPreFab, new Vector3(8.0f, 0.0f, 0.0f), Quaternion.identity);
            FishBrain = Fish.GetComponent<Fish>();
            SpawnTimer = SpawnTimerMax;
        }
    }

    public void Eat()
    {
        if (Fish != null)
        {
            if (FishBrain.Harmful)
            {
                //ate the bad fish
                SpawnTimer = 2;
                SharkAnimator.SetTrigger("Eat");
                SharkText.text = "That's not nice NUM NUM NUM";
                Destroy(Fish, 1.4f);
                score += 10;
            }
            else
            {
                //ate the good fish
                SharkAnimator.SetTrigger("Eat");
                SpawnTimer = 50;
                SharkText.text = "That fish was nice why did I eat it";
                Destroy(Fish, 1.4f);
                Level = 2;
                StartCoroutine(LoadLevelAfterDelay(delay));
            }
        }
    }

    public void DontEat()
    {
        if (Fish != null)
        {
            if (FishBrain.Harmful)
            {
                //let the bad fish go
                FishBrain.animator.SetTrigger("SwimAway");
                Destroy(Fish, 1.4f);
                SpawnTimer = 50;
                SharkText.text = "That was a mean fish and now Im hungry";
                Level = 1;
                StartCoroutine(LoadLevelAfterDelay(delay));
            }
            else
            {
                //let the good fish go
                FishBrain.animator.SetTrigger("SwimAway");
                SharkText.text = "Your Nice, I'll let you go";
                Destroy(Fish, 1.4f);
                score += 10;
            }
        }
    }
}
