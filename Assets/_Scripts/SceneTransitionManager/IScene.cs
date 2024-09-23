using System.Collections.Generic;

public interface IScene
{
    public string Name { get; }
    public bool Active { get; }
    public bool Preloaded { get; }
    public bool Disabled { get; }

    public List<IScene> Neighbours { get; }

}
