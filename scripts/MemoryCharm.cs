using Godot;

public partial class MemoryCharm : Area3D
{
	private WorldController worldController;
    public override void _Ready()
    {
        worldController = GetNode<WorldController>("/root/Main/WorldController");
    }

    public void OnBodyEntered(Node3D node)
	{
		if (node.IsInGroup("Player"))
		{
			worldController.AddLastWorld();
			worldController.CharmValue++;
		}
	}
}
