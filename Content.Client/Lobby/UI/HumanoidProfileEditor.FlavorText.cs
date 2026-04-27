using Robust.Client.UserInterface.Controls;
using Robust.Shared.Utility;

namespace Content.Client.Lobby.UI;

// !! CRIMSON COMMAND MODIFIED !! //
public sealed partial class HumanoidProfileEditor
{
    private bool _allowFlavorText;

    private _CrimsonCommand.FlavorText.NewFlavorText? _flavorText; // CC : FlavorText -> NewFlavorText
    private TextEdit? _flavorTextEdit;
    private TextEdit? _backstoryEdit; // CC : Added

    /// <summary>
    /// Refreshes the flavor text editor status.
    /// </summary>
    public void RefreshFlavorText()
    {
        if (_allowFlavorText)
        {
            if (_flavorText != null)
                return;

            _flavorText = new _CrimsonCommand.FlavorText.NewFlavorText(); // CC : FlavorText -> NewFlavorText
            TabContainer.AddChild(_flavorText);
            TabContainer.SetTabTitle(TabContainer.ChildCount - 1, Loc.GetString("humanoid-profile-editor-flavortext-tab"));
            _flavorTextEdit = _flavorText.CFlavorTextInput;
            _backstoryEdit = _flavorText.CBackstoryInput; // CC : Added

            _flavorText.OnFlavorTextChanged += OnFlavorTextChange;
            _flavorText.OnBackstoryChanged += OnBackstoryChange; // CC : Added
        }
        else
        {
            if (_flavorText == null)
                return;

            TabContainer.RemoveChild(_flavorText);
            _flavorText.OnFlavorTextChanged -= OnFlavorTextChange;
            _flavorText.OnBackstoryChanged -= OnBackstoryChange; // CC : Added
            _flavorText.Dispose();
            _flavorTextEdit?.Dispose();
            _backstoryEdit?.Dispose(); // CC : Added
            _flavorTextEdit = null;
            _backstoryEdit = null; // CC : Added
            _flavorText = null;
        }
    }

    private void OnFlavorTextChange(string content)
    {
        if (Profile is null)
            return;

        Profile = Profile.WithFlavorText(content);
        SetDirty();
    }

    // !! CRIMSON COMMAND SPECIFIC !! //
    private void OnBackstoryChange(string content)
    {
        if (Profile is null)
            return;

        Profile = Profile.WithBackstory(content);
        SetDirty();
    }

    private void UpdateFlavorTextEdit()
    {
        if (_flavorTextEdit != null)
            _flavorTextEdit.TextRope = new Rope.Leaf(Profile?.FlavorText ?? "");

        if (_backstoryEdit != null)
            _backstoryEdit.TextRope = new Rope.Leaf(Profile?.Backstory ?? "");
    }
}
