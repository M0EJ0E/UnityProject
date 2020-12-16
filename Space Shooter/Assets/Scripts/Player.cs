using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //Variables
    [SerializeField] private float _speed = 5f;
    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private GameObject _tripleShotPrefab;
    [SerializeField] private float _fireRate = 0.5f;
    [SerializeField] private int _lives = 3;
    [SerializeField] private float _speedBoost = 2;
    [SerializeField] private bool _tripleShotActive = false;
    [SerializeField] private bool _speedPowerup = false;
    [SerializeField] private bool _shieldsPowerup = false;
    [SerializeField] private GameObject _shieldsVisualizer;
    [SerializeField] private int _score = 0;
    [SerializeField] private GameObject _rightEngine;
    [SerializeField] private GameObject _leftEngine;
    
    private UIManager _uiManager;
    private float _canFire = -1f;
    private SpawnManager _spawnManager;
  

    // Start is called before the first frame update
    void Start()
    {
         transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();


        if (_spawnManager == null)    
        {
            Debug.LogError("The spawn manager is NULL");
        }
        if (_uiManager == null)
        {
            Debug.LogError("The UI Manger is NULL");
        }
    }

    void Update()
    {
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }
    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * _speed * Time.deltaTime);
      
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -4.5f, 0), 0);

        if (transform.position.x >= 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if (transform.position.x <= -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
    }

    void FireLaser()
    {
         _canFire = Time.time + _fireRate;
        if (_tripleShotActive)
        {
            Instantiate(_tripleShotPrefab, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
        }
    }
    public void Damage()
    {
        if (_shieldsPowerup == true)
        {
            _shieldsPowerup = false;
            _shieldsVisualizer.SetActive(false);
            return;
        }
        else
        {
            _lives--;

            if (_lives == 2)
            {
                _rightEngine.SetActive(true);
            }
            else if (_lives == 1)
            {
                _leftEngine.SetActive(true);
            }

            _uiManager.UpdateLives(_lives);

            if (_lives < 1)
            {
                Destroy(this.gameObject);
                _spawnManager.OnPlayerDeath();
                _uiManager.GameOver();
            }
        }
    }

    public void TripleShotActive()
    {
        _tripleShotActive = true;
        StartCoroutine(TripleShotCoolDownRoutine());
    }

    IEnumerator TripleShotCoolDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _tripleShotActive = false;
    }

    public void SpeedBoostActive()
    {
        _speedPowerup = true;
        _speed *= _speedBoost;
        StartCoroutine(SpeedBoostCoolDownRoutine());
    }

    IEnumerator SpeedBoostCoolDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _speedPowerup = false;
        _speed = 5;
    }

    public void ShieldsActive()
    {
        _shieldsPowerup = true;
        _shieldsVisualizer.SetActive(true);      
    }
    public void AddScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }
}
