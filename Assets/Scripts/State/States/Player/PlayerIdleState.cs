using UnityEngine;
using System;
using System.Collections;

public class PlayerIdleState : PlayerState {

	public PlayerIdleState( PlayerStateMachine stateMachine, PlayerCharacter character, PlayerController controller ) 
	: base( stateMachine, character, controller )
	{

	}

	public override void OnEnterState()
	{
		Debug.Log("Player Idle On Enter");
	}

	public override void OnUpdate()
	{
		if( Actions.Move.Value != Vector2.zero )
		{
			stateMachine.CurrentState = PlayerStateType.WALK;
		}
	}

	public override void OnExitState()
	{
		Debug.Log("Player Idle On Exit");
	}

}
