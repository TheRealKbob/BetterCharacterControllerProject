using UnityEngine;
using System;
using System.Collections;

public class PlayerFallState : PlayerState {

	public PlayerFallState( PlayerStateMachine stateMachine, PlayerCharacter character, PlayerController controller ) 
	: base( stateMachine, character, controller )
	{

	}

	public override void OnEnterState()
	{
		Debug.Log("Player Fall On Enter");
	}

	public override void OnUpdate()
	{
		// Check for ground
		controller.Fall();
	}

	public override void OnExitState()
	{
		Debug.Log("Player Fall On Exit");
	}

	public override void HandleControllerEvent( string eventID )
	{
		Debug.Log("EventID: " + eventID);
		if( eventID == PlayerControllerEvents.ENTER_GROUND )
			stateMachine.CurrentState = PlayerStateType.IDLE;
	}

}
