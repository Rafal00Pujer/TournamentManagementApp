
//------------------------------------------------------------------------------

//  <auto-generated>
//      This code was generated by:
//        TerminalGuiDesigner v1.0.24.0
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// -----------------------------------------------------------------------------
namespace TournamentManagementConsoleUi.View.Windows {
    using System;
    using Terminal.Gui;
    
    
    public partial class CreateTournamentWindow : Terminal.Gui.Window {
        
        private Terminal.Gui.Label label;
        
        private Terminal.Gui.TextField nameTextField;
        
        private Terminal.Gui.Label label2;
        
        private Terminal.Gui.TextField gameNameTextField;
        
        private Terminal.Gui.Label label3;
        
        private Terminal.Gui.TextField descriptionTextField;
        
        private Terminal.Gui.Label label4;
        
        private Terminal.Gui.ListView teamsListView;
        
        private Terminal.Gui.Button removeTeamBtn;
        
        private Terminal.Gui.Button addTeamBtn;
        
        private Terminal.Gui.Button randomizeTeamsBtn;
        
        private Terminal.Gui.Button createTournamentBtn;
        
        private void InitializeComponent() {
            this.createTournamentBtn = new Terminal.Gui.Button();
            this.randomizeTeamsBtn = new Terminal.Gui.Button();
            this.addTeamBtn = new Terminal.Gui.Button();
            this.removeTeamBtn = new Terminal.Gui.Button();
            this.teamsListView = new Terminal.Gui.ListView();
            this.label4 = new Terminal.Gui.Label();
            this.descriptionTextField = new Terminal.Gui.TextField();
            this.label3 = new Terminal.Gui.Label();
            this.gameNameTextField = new Terminal.Gui.TextField();
            this.label2 = new Terminal.Gui.Label();
            this.nameTextField = new Terminal.Gui.TextField();
            this.label = new Terminal.Gui.Label();
            this.Width = Dim.Fill(0);
            this.Height = Dim.Fill(0);
            this.X = 0;
            this.Y = 0;
            this.Modal = true;
            this.Border.BorderStyle = Terminal.Gui.BorderStyle.Single;
            this.Border.BorderBrush = Terminal.Gui.Color.White;
            this.Border.Effect3D = false;
            this.Border.Effect3DBrush = null;
            this.Border.DrawMarginFrame = true;
            this.TextAlignment = Terminal.Gui.TextAlignment.Left;
            this.Title = "Create Tournament (Press Ctrl+Q to cancel)";
            this.label.Width = 3;
            this.label.Height = 1;
            this.label.X = 0;
            this.label.Y = 0;
            this.label.Data = "label";
            this.label.Text = "Name";
            this.label.TextAlignment = Terminal.Gui.TextAlignment.Left;
            this.Add(this.label);
            this.nameTextField.Width = Dim.Fill(10);
            this.nameTextField.Height = 1;
            this.nameTextField.X = 12;
            this.nameTextField.Y = 0;
            this.nameTextField.Secret = false;
            this.nameTextField.Data = "nameTextField";
            this.nameTextField.Text = "";
            this.nameTextField.TextAlignment = Terminal.Gui.TextAlignment.Left;
            this.Add(this.nameTextField);
            this.label2.Width = 3;
            this.label2.Height = 1;
            this.label2.X = 0;
            this.label2.Y = 2;
            this.label2.Data = "label2";
            this.label2.Text = "Game Name";
            this.label2.TextAlignment = Terminal.Gui.TextAlignment.Left;
            this.Add(this.label2);
            this.gameNameTextField.Width = Dim.Fill(10);
            this.gameNameTextField.Height = 1;
            this.gameNameTextField.X = 12;
            this.gameNameTextField.Y = 2;
            this.gameNameTextField.Secret = false;
            this.gameNameTextField.Data = "gameNameTextField";
            this.gameNameTextField.Text = "";
            this.gameNameTextField.TextAlignment = Terminal.Gui.TextAlignment.Left;
            this.Add(this.gameNameTextField);
            this.label3.Width = 3;
            this.label3.Height = 1;
            this.label3.X = 0;
            this.label3.Y = 4;
            this.label3.Data = "label3";
            this.label3.Text = "Description";
            this.label3.TextAlignment = Terminal.Gui.TextAlignment.Left;
            this.Add(this.label3);
            this.descriptionTextField.Width = Dim.Fill(10);
            this.descriptionTextField.Height = 1;
            this.descriptionTextField.X = 12;
            this.descriptionTextField.Y = 4;
            this.descriptionTextField.Secret = false;
            this.descriptionTextField.Data = "descriptionTextField";
            this.descriptionTextField.Text = "";
            this.descriptionTextField.TextAlignment = Terminal.Gui.TextAlignment.Left;
            this.Add(this.descriptionTextField);
            this.label4.Width = 1;
            this.label4.Height = 1;
            this.label4.X = 0;
            this.label4.Y = 6;
            this.label4.Data = "label4";
            this.label4.Text = "Teams";
            this.label4.TextAlignment = Terminal.Gui.TextAlignment.Left;
            this.Add(this.label4);
            this.teamsListView.Width = 20;
            this.teamsListView.Height = 10;
            this.teamsListView.X = 0;
            this.teamsListView.Y = 7;
            this.teamsListView.Data = "teamsListView";
            this.teamsListView.TextAlignment = Terminal.Gui.TextAlignment.Left;
            this.teamsListView.Source = new Terminal.Gui.ListWrapper(new string[] {
                        "Item1",
                        "Item2",
                        "Item3"});
            this.teamsListView.AllowsMarking = false;
            this.teamsListView.AllowsMultipleSelection = true;
            this.Add(this.teamsListView);
            this.removeTeamBtn.Width = 15;
            this.removeTeamBtn.Height = 1;
            this.removeTeamBtn.X = 23;
            this.removeTeamBtn.Y = 7;
            this.removeTeamBtn.Data = "removeTeamBtn";
            this.removeTeamBtn.Text = "Remove Team";
            this.removeTeamBtn.TextAlignment = Terminal.Gui.TextAlignment.Centered;
            this.removeTeamBtn.IsDefault = false;
            this.Add(this.removeTeamBtn);
            this.addTeamBtn.Width = 12;
            this.addTeamBtn.Height = 1;
            this.addTeamBtn.X = 24;
            this.addTeamBtn.Y = 9;
            this.addTeamBtn.Data = "addTeamBtn";
            this.addTeamBtn.Text = "Add Team";
            this.addTeamBtn.TextAlignment = Terminal.Gui.TextAlignment.Centered;
            this.addTeamBtn.IsDefault = false;
            this.Add(this.addTeamBtn);
            this.randomizeTeamsBtn.Width = 19;
            this.randomizeTeamsBtn.Height = 1;
            this.randomizeTeamsBtn.X = 22;
            this.randomizeTeamsBtn.Y = 11;
            this.randomizeTeamsBtn.Data = "randomizeTeamsBtn";
            this.randomizeTeamsBtn.Text = "Randomize Teams";
            this.randomizeTeamsBtn.TextAlignment = Terminal.Gui.TextAlignment.Centered;
            this.randomizeTeamsBtn.IsDefault = false;
            this.Add(this.randomizeTeamsBtn);
            this.createTournamentBtn.Width = 21;
            this.createTournamentBtn.Height = 1;
            this.createTournamentBtn.X = 48;
            this.createTournamentBtn.Y = 16;
            this.createTournamentBtn.Data = "createTournamentBtn";
            this.createTournamentBtn.Text = "Create Tournament";
            this.createTournamentBtn.TextAlignment = Terminal.Gui.TextAlignment.Centered;
            this.createTournamentBtn.IsDefault = false;
            this.Add(this.createTournamentBtn);
        }
    }
}