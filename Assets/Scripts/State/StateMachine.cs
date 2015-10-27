using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class StateMachine : MonoBehaviour {

	protected Dictionary<Enum, IState> stateHash = new Dictionary<Enum, IState>();

	protected IState state = new State();

	public Enum CurrentState
	{
		get
		{
			return state.ID;
		}
		set
		{
			if( state.ID == value )
				return;
			changingState();
			state.ID = value;
			configureCurrentState();
		}
	}

	public Enum LastState{ get; private set; }

	void Awake () {
		
	}

	private void changingState()
	{
		LastState = state.ID;
	}

	private void configureCurrentState()
	{
		state.OnExitState();
		state = getState( CurrentState );
		state.OnEnterState();
	}

	protected void addState( Enum stateID, IState state )
	{
		stateHash.Add( stateID, state );
	}

	protected void removeState( Enum stateID )
	{
		stateHash.Remove( stateID );
	}

	protected IState getState( Enum stateID )
	{
		if( stateHash.ContainsKey( stateID ) )
		{	
			IState s = stateHash[stateID];
			return s;
		}
		return null;
	}

	void DoUpdate()
	{	
		PreStateUpdate();
		state.OnUpdate();
		PostStateUpdate();
	}

	protected virtual void PreStateUpdate(){}
	protected virtual void PostStateUpdate(){}

	public static void DoNothing() { }

}

public class State : IState
{
	public Enum ID{ get; set; }
	public State(){}
	public virtual void OnEnterState(){}
	public virtual void OnUpdate(){}
	public virtual void OnExitState(){}
}

public interface IState
{
	void OnUpdate();
	void OnEnterState();
	void OnExitState();

	Enum ID{ get; set; }
}
