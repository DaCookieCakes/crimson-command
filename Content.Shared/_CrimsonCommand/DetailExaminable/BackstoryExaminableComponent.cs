using Robust.Shared.GameStates;

namespace Content.Shared._CrimsonCommand.DetailExaminable;

[RegisterComponent, NetworkedComponent, AutoGenerateComponentState]
public sealed partial class BackstoryExaminableComponent : Component
{
    [DataField(required: true), AutoNetworkedField]
    public string Content = string.Empty;
}
