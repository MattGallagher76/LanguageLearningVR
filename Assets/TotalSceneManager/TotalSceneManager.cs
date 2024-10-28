using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotalSceneManager : MonoBehaviour
{
    public Animator acController;

    public string[] animationTriggers;

    enum userStates
    {
        sit, waiterGreet, greetRepond, waiterDrinkOrder, drinkOrderRespond,
        waiterLeaveMenu, menuReading, waiterFoodOrder, foodOrderRespond, 
        waiterBringFood, eat, waiterBringCheck, payCheck, directions, 
        useBathroom, leave
    };

    private int currentState = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
