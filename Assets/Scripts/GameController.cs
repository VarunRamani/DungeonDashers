using Unity;
using UnityEngine;
using UnityEngine.Events;
public class GameController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int level;
    public UnityEvent newLevel;
    public UnityEvent loading;

    public int enemyBasicHealth;

    public GameObject enemyBasic;
    void Start()
    {
        enemyBasicHealth = 5;
        NewLevel(1);
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void NewLevel(int level) {

        loading.Invoke();
        GameObject newEnemy = Instantiate(enemyBasic, new Vector2(Random.Range(-10, 10), Random.Range(-10, 10)), Quaternion.identity);
        

        newLevel.Invoke();
        
        
        enemyBasicHealth++;


    }

        
        


    
}
