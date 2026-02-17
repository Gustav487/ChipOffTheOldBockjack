using Godot;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace ChipMasters.Menu
{
    public partial class NCreditsMenu : AnimationPlayer
    {
        [Export] private string? _CreditsPath;
        [Export] private Label? _GroupLabel;
        [Export] private Label? _Name;
        [Export] private Label? _RolesLabel;
        [Export] private Label? _TaskLabel;
        [Export] private Label? _Roles;
        [Export] private Label? _Tasks;
        [Export] private Label? _Thanks;

        private string devJSONData;
        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            _RolesLabel.Visible = false;
            _TaskLabel.Visible = false;
            devJSONData = File.ReadAllText(_CreditsPath);
            AnimateCredits();
        }
        private async Task AnimateCredits()
        {
            Play("fade_in");
            await ToSignal(GetTree().CreateTimer(3), "timeout");
            Play("fade_out");
            await ToSignal(GetTree().CreateTimer(3), "timeout");
            AnimateDevs();
        }

        private async Task AnimateDevs()
        {
            var _devs = JsonSerializer.Deserialize<List<Developer>>(devJSONData);

            foreach (var Developer in _devs)
            {
                _Name.Text = Developer.name.ToString();

                string roleString = "";
                foreach (var item in Developer.Roles)
                {
                    roleString += (item + "\n");
                }
                _Roles.Text = roleString;

                string TaskString = "";
                foreach (var item in Developer.Tasks)
                {
                    TaskString += ("•" + item + "\n");
                }
                _Tasks.Text = TaskString;
                _RolesLabel.Visible = true;
                _TaskLabel.Visible = true;
                Play("fade_in_dev");
                await ToSignal(GetTree().CreateTimer(5), "timeout");
                Play("fade_out_dev");
                await ToSignal(GetTree().CreateTimer(2), "timeout");
            }
            AnimateThanks();
        }
        private void AnimateThanks()
        {
            Play("Thanks");
        }
    }
    public class Developer
    {
        public string name { get; set; }
        public string[] Roles { get; set; }
        public string[] Tasks { get; set; }
    }
}
