using UnityEngine;
using System.Collections;
using System;

public class TurnTimerController : MonoBehaviour
{
    [SerializeField]
    TurnOrderController TurnOrderController;

    public event Action OnTick;

    public static TurnTimerController instance;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        TurnOrderController.OnTurnEnded += TurnOrderController_OnTurnEnded;
    }

    public TurnTimer GetTimer(int count)
    {
        return new TurnTimer(count, this);
    }

    private void TurnOrderController_OnTurnEnded(object sender, EventArgs e)
    {
        OnTick?.Invoke();
    }

    public class TurnTimer
    {
        int maxCount;
        int curentCount;

        public event Action OnStart;
        public event Action OnEnd;

        public TurnTimer(int count, TurnTimerController turnTimer)
        {
            this.maxCount = count;
            curentCount = 0;
            turnTimer.OnTick += TurnTimer_OnTick;
        }

        public void Reset()
        {
            curentCount = 0;
        }

        private void TurnTimer_OnTick()
        {
            if(curentCount == maxCount)
            {
                curentCount++;
                OnEnd?.Invoke();
            }
            else
            {
                if(curentCount == 0)
                {
                    OnStart?.Invoke();
                }

                curentCount++;
            }
        }
    }
}
