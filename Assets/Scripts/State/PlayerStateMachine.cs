using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum PlayerStateType{ IDLE, WALK, JUMP, FALL }

[RequireComponent( typeof( PlayerController ) )]
public class PlayerStateMachine : StateMachine {

	private PlayerCharacter character;
	private PlayerController controller;

	void Start()
	{
		character = (GameObject.FindObjectOfType( typeof( PlayerCharacter ) ) as PlayerCharacter);
		controller = (GameObject.FindObjectOfType( typeof( PlayerController ) ) as PlayerController);

		controller.OnControllerEvent += OnControllerEvent;

		addState( PlayerStateType.IDLE, new PlayerIdleState( this, character, controller ) );
		addState( PlayerStateType.WALK, new PlayerWalkState( this, character, controller ) );
		addState( PlayerStateType.FALL, new PlayerFallState( this, character, controller ) );

		CurrentState = PlayerStateType.IDLE;
	}

	void OnGUI()
	{
		if( GUI.Button(new Rect(10, 10, 70, 30), "Fall") )
		{
			CurrentState = PlayerStateType.FALL;
		}
	}

	protected override void PreStateUpdate()
	{

	}

	protected override void PostStateUpdate()
	{
		controller.ApplyMoveVector();
	}

	private void OnControllerEvent( string eventID )
	{
		(state as PlayerState).HandleControllerEvent( eventID );
	}

}

public class PlayerState : State {

	protected PlayerStateMachine stateMachine;
	protected PlayerCharacter character;
	protected PlayerController controller;

	public CharacterActions Actions{ get; private set; }

	public PlayerState( PlayerStateMachine stateMachine, PlayerCharacter character, PlayerController controller )
	{
		this.stateMachine = stateMachine;
		this.controller = controller;
		this.character = character;
		Actions = character.Input.Actions;
	}

	public virtual void HandleControllerEvent( string eventID ){}

}
