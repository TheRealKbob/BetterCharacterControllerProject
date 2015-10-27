using UnityEngine;
using System.Collections;

public enum CameraStateTypes{ FREE, TARGET }

public class CameraStateMachine : StateMachine {

	public PlayerController Controller{get; private set; }
	public PlayerCamera Camera{ get; private set; }

	// Use this for initialization
	void Start () {
		addState( CameraStateTypes.FREE, new CameraFreeState( this ) );
		addState( CameraStateTypes.TARGET, new CameraTargetState( this ) );

		Controller = GameObject.FindObjectOfType<PlayerController>() as PlayerController;
		Camera = gameObject.GetComponent<PlayerCamera>() as PlayerCamera;

		CurrentState = CameraStateTypes.FREE;
	}
	
}

public class CameraState : State {

	protected CameraStateMachine stateMachine;
	protected PlayerController controller;
	protected PlayerCamera camera;

	public CharacterActions Actions{ get; private set; }

	public Vector3 LookDirectionModifier{ get; private set; }

	public CameraState( CameraStateMachine stateMachine )
	{
		this.stateMachine = stateMachine;
		controller = (GameObject.FindObjectOfType( typeof( PlayerController ) ) as PlayerController);
		camera = (GameObject.FindObjectOfType( typeof( PlayerCamera ) ) as PlayerCamera);
		Actions = (GameObject.FindObjectOfType( typeof( PlayerCharacter ) ) as PlayerCharacter).Input.Actions;
	}

	public override void OnUpdate()
	{
		LookDirectionModifier = camera.transform.TransformDirection(Vector3.forward);
	}

}
