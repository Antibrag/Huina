using Godot;
using System;

public partial class Player : CharacterBody3D
{
	public float Speed { get; set; } = 5.0f;
	public float JumpVelocity { get; set; } = 4.5f;
	private float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseMotion ev)
			Rotation = new Vector3(Rotation.X, Rotation.Y - ev.Relative.X / 1152, Rotation.Z);
    }

    public override void _Ready()
    {
        Input.MouseMode = Input.MouseModeEnum.Captured;
    }

    public override void _PhysicsProcess(double delta)
	{
		Vector3 velocity = Velocity;

		if (!IsOnFloor())
			velocity.Y -= gravity * (float)delta;

		if (Input.IsActionJustPressed("Jump") && IsOnFloor())
			velocity.Y = JumpVelocity;

		Vector2 inputDir = Input.GetVector("Left", "Right", "Forward", "Backward");
		Vector3 direction = (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();

		if (direction != Vector3.Zero)
		{
			velocity.X = direction.X * Speed;
			velocity.Z = direction.Z * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			velocity.Z = Mathf.MoveToward(Velocity.Z, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
