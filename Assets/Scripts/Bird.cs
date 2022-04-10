//Jose Velazquez
//Bird

using System.Net.Mime;
using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

using Vector3 = UnityEngine.Vector3;
using Vector2 = UnityEngine.Vector2;

public class Bird : MonoBehaviour
{
    [SerializeField]private float _upForce = 3f; //The upward force of the bird.
    [SerializeField]private GameObject _intruction; //UI -> Instruction Screen
    [SerializeField]private GameObject _gameScreen; //UI -> Game Screen
    [SerializeField]private GameObject _deadScreen; //UI -> GameOver Screen
    [SerializeField]private Text _highScore; //UI -> Text HighScore
    [SerializeField]private Text[] _textScore; //UI -> Score
    [SerializeField]private Generator _generator;
    [SerializeField]private Rigidbody2D _rb;
    [SerializeField]private Animator _animator;
    [SerializeField]private AudioSource _audioSource;
    [SerializeField]private AudioSource _audioPoints; //Audio -> Points
    [SerializeField]private AudioClip _audioHit; //Audio -> The bird hit something
    [SerializeField]private AudioClip _audioWing; //Audio -> The bird flying
    [HideInInspector]public bool begin = false;
    [HideInInspector]public bool _isDead = false;
    private int _score = 0;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = gameObject.GetComponent<Animator>();
        _generator = GameObject.Find("Generator").GetComponent<Generator>(); //Find Generator in the game and gets component
        _rb.gravityScale = 0; //Gravity 0 before begin play
        _animator.SetTrigger("Loop"); //Animation of the bird loop fly
        Time.timeScale = 1; //The game is not in Pause
    }

    void Update()
    {
        BeforeBegin();
        AfterBegin();
        BirdDirection();
    }

    private void BeforeBegin() //Function -> Before the gameplay begins
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began || Input.GetMouseButtonDown(0) && !begin || Input.GetKeyDown(KeyCode.Space) && !begin) 
        {
            _rb.gravityScale = 2; //Gravity 2 after begin play
            _animator.ResetTrigger("Loop");
            _animator.SetTrigger("Stay"); 
            begin = true;
            _generator.InvokeObstacle(); //Call for Functions Generator Scripts
            _intruction.SetActive(false); //Hide Intruction
        }

        if(!begin)
        {
            /// <summary>
            /// Make animation of the bird flying in the waiting for begin
            /// </summary>
            float pos1, pos2;
            pos1 = transform.position.x + 1;
            pos2 = Mathf.Sin(Time.time * 5);
            transform.position += new Vector3(0, pos2,0) * Time.deltaTime;
        }
    }

    private void AfterBegin() //Function -> After the gameplay begins
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began || Input.GetKeyDown(KeyCode.Space))
        {
            if(!_isDead)
            {
                _rb.velocity = new Vector2(0, _upForce);
                _animator.SetTrigger("Fly"); //The Bird Fly Animation
                _animator.ResetTrigger("Stay");
                ChangeAudioClip(_audioWing);
            }
        }
        else
        {
            if(!_isDead)
            {
                _animator.ResetTrigger("Fly");
                _animator.SetTrigger("Stay");
            }
        }

    }

    private void BirdDirection() //Function -> The bird effect changes the view direction up or down depending on the speed of the force
    {
        if(_rb.velocity.y > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 45); //Up
        }
        if (_rb.velocity.y < 0)
        {
            transform.eulerAngles = new Vector3(0, 0, -45); //Down
        }
        if (_rb.velocity.y == 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0); //Middle
        }
    }

    private void OnTriggerEnter2D(Collider2D other) //Function -> The bird of collition enter in a collider
    {
        if(other.gameObject.CompareTag("Limit"))
        {
            if(!_isDead)
            {
                ChangeAudioClip(_audioHit);
            }
            _generator.isDead = true;
           _animator.SetTrigger("Dead");
           _isDead = true;
           ChangeAudioClip(_audioHit);
        }
        else if(other.gameObject.CompareTag("Obstacle"))
        {
            if(!_isDead)
            {
                ChangeAudioClip(_audioHit);
            }
            _isDead = true;
            _generator.isDead = true;
            _animator.SetTrigger("Dead");
        }
        else if(other.gameObject.CompareTag("Floor"))
        {
            ChangeAudioClip(_audioHit);
            _animator.SetTrigger("Dead");
            _rb.simulated = false;
            _isDead = true;
            _generator.isDead = true;
            _deadScreen.SetActive(true);//Active
            _gameScreen.SetActive(false);//Hide
            _highScore.text = PlayerPrefs.GetInt("HighScore").ToString(); //Define HighScore for the UI Text
            StartCoroutine(Pause());     
        }
        else if(other.gameObject.CompareTag("Score"))
        {
            _audioPoints.Play();
            _score += 1; //Add 1 to the score
            for(int i = 0; i < _textScore.Length ; i++)
            {
                _textScore[i].text =  _score.ToString(); //Define score for the UI Text
            }
            if(_score > PlayerPrefs.GetInt("HighScore"))
            {
                PlayerPrefs.SetInt("HighScore" , _score); //Change HighScore if the score is more
            }
        }
    }

    IEnumerator Pause()//Function -> Pause in the gameover
    {
        yield return new WaitForSeconds(1);
        Time.timeScale = 0; //Pause
    }

    void ChangeAudioClip(AudioClip Audio) //Function -> Change AudioClip in AudioSource
    {
        _audioSource.clip = Audio;
        _audioSource.Play();
    }
}