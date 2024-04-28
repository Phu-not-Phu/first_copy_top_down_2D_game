using Godot;
using Godot.Collections;
using System.Linq;

public partial class options_setting : VBoxContainer
{
    OptionButton resolution_option;
    CheckBox fullscreen;

    Dictionary Resolutions = new()
    {
        {"800x600", new Vector2(800, 600)},
        {"1024x768", new Vector2(1024, 768)},
        {"1152x648", new Vector2(1152, 648)},
        {"1280x720", new Vector2(1280, 720)},
        {"1920x1080", new Vector2(1920, 1080)}
    };

    public override void _Ready()
    {
        resolution_option = GetNode<OptionButton>("ResolutionOption");
        fullscreen = GetNode<CheckBox>("Fullscreen");
        Add_Resolutions();
        Check_Variables();
    }

    public void Check_Variables()
    {
        var _window = GetWindow();
        Window.ModeEnum mode = _window.Mode;

        if (mode == Window.ModeEnum.Fullscreen)
        {
            fullscreen.SetPressedNoSignal(true);
        }
    }

    public void Set_Resolution_Text()
    {
        var resolution_text = GetWindow().Size.X.ToString() + "x" + GetWindow().Size.Y.ToString();
        resolution_option.Text = resolution_text;
    }

    public void Add_Resolutions()
    {
        var current_resolution = GetWindow().Size;
        var ID = 0;

        foreach (var resolution in Resolutions.Keys.Select(v => (string)v))
        {
            resolution_option.AddItem(resolution);

            if ((Vector2I)Resolutions[resolution] == current_resolution)
            {
                resolution_option.Select(ID);
            }

            ID++;
        }
    }


    private void OnResolutionOptionItemSelected(int index)
    {
        var ID = resolution_option.GetItemText(index);
        GetWindow().Size = (Vector2I)Resolutions[ID];
        CentreWindow();
    }

    private void CentreWindow()
    {
        var centre_sceen = DisplayServer.ScreenGetPosition() + DisplayServer.ScreenGetSize() / 2;
        var window_size = GetWindow().GetSizeWithDecorations();

        GetWindow().Position = centre_sceen - window_size / 2;
    }

    private void OnFullscreenToggled(bool toggled_on)
    {
        if (toggled_on)
        {
            GetWindow().Mode = Window.ModeEnum.Fullscreen;
        }
        else
        {
            GetWindow().Mode = Window.ModeEnum.Windowed;
            CentreWindow();
        }
        
        var timer = GetTree().CreateTimer(0.5f);
        timer.Timeout += () => Set_Resolution_Text();
    }

}
