using IceMilkTea.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineMonoBehaviour : MonoBehaviour
{
    protected class State<TContext, TEvent> : ImtStateMachine<TContext, TEvent>.State where TContext : StateMachineMonoBehaviour
    {
        protected internal override sealed void Enter()
        {
            Context.onTriggerEnter += OnTriggerEnter;
            Context.onTriggerStay += OnTriggerStay;
            Context.onTriggerExit += OnTriggerExit;
            OnEnter();
        }

        protected internal override sealed void Update()
        {
            OnUpdate();
        }

        protected internal override sealed void Exit()
        {
            Context.onTriggerEnter -= OnTriggerEnter;
            Context.onTriggerStay -= OnTriggerStay;
            Context.onTriggerExit -= OnTriggerExit;
            OnExit();
        }

        protected virtual void OnEnter()
        {
        }

        protected virtual void OnUpdate()
        {
        }

        protected virtual void OnExit()
        {
        }

        protected virtual void OnTriggerEnter(Collider2D collision)
        {
        }

        protected virtual void OnTriggerStay(Collider2D collision)
        {
        }

        protected virtual void OnTriggerExit(Collider2D collision)
        {
        }
    }

    private Action<Collider2D> onTriggerEnter = null;
    private Action<Collider2D> onTriggerStay = null;
    private Action<Collider2D> onTriggerExit = null;

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        this.onTriggerEnter?.Invoke(collision);
    }

    protected virtual void OnTriggerStay2D(Collider2D collision)
    {
        this.onTriggerStay?.Invoke(collision);
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        this.onTriggerExit?.Invoke(collision);
    }
}
