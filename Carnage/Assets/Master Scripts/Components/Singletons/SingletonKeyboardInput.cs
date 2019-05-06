using Unity.Entities;

/*TODO:
 * Read basic Hotkeys for RTS games: https://starcraft.fandom.com/wiki/Hotkey
 * 
 * Should split hotkeys across multiple Singletons
 * 
 */
public struct SingletonKeyboardInput : IComponentData
{
    public bool SpaceBar;
    public float HorizontalMovement;
    public float VerticalMovement;
}
