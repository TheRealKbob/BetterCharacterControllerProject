using UnityEngine;
using System;
using System.Collections;

public class PlayerWalkState : PlayerState {

	public PlayerWalkState( PlayerStateMachine stateMachine, PlayerCharacter character, PlayerController controller )
	: base( stateMachine, character, controller )
	{

	}

	public override void OnEnterState()
	{
		Debug.Log("Player Walk On Enter");
	}

	public override void OnUpdate()
	{
		if( Actions.Move.Value != Vector2.zero )
		{
			//TODO Move
			Debug.Log("Moving");
		}
		else
		{
			stateMachine.CurrentState = PlayerStateType.IDLE;
		}
	}

	public override void OnExitState()
	{
		Debug.Log("Player Walk On Exit");
	}

}
