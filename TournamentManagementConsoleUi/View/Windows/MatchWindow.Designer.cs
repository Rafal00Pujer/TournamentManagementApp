
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
    
    
    public partial class MatchWindow : Terminal.Gui.Window {
        
        private Terminal.Gui.Label label;
        
        private Terminal.Gui.DateField dateField;
        
        private Terminal.Gui.Label firstTeamLabel;
        
        private Terminal.Gui.Label secondTeamLabel;
        
        private Terminal.Gui.ListView setsListView;
        
        private Terminal.Gui.Label label2;
        
        private Terminal.Gui.TextField firstScoreTextField;
        
        private Terminal.Gui.TextField secondScoreTextField;
        
        private Terminal.Gui.Button saveNewScoreBtn;
        
        private Terminal.Gui.Button addSetBtn;
        
        private Terminal.Gui.Button removeSetBtn;
        
        private Terminal.Gui.Button setWinnerBtn;
        
        private void InitializeComponent() {
            this.setWinnerBtn = new Terminal.Gui.Button();
            this.removeSetBtn = new Terminal.Gui.Button();
            this.addSetBtn = new Terminal.Gui.Button();
            this.saveNewScoreBtn = new Terminal.Gui.Button();
            this.secondScoreTextField = new Terminal.Gui.TextField();
            this.firstScoreTextField = new Terminal.Gui.TextField();
            this.label2 = new Terminal.Gui.Label();
            this.setsListView = new Terminal.Gui.ListView();
            this.secondTeamLabel = new Terminal.Gui.Label();
            this.firstTeamLabel = new Terminal.Gui.Label();
            this.dateField = new Terminal.Gui.DateField();
            this.label = new Terminal.Gui.Label();
            this.Width = Dim.Fill(0);
            this.Height = Dim.Fill(0);
            this.X = 0;
            this.Y = 0;
            this.Modal = false;
            this.Border.BorderStyle = Terminal.Gui.BorderStyle.Single;
            this.Border.BorderBrush = Terminal.Gui.Color.White;
            this.Border.Effect3D = false;
            this.Border.Effect3DBrush = null;
            this.Border.DrawMarginFrame = true;
            this.TextAlignment = Terminal.Gui.TextAlignment.Left;
            this.Title = "Match Window (Press Ctrl+Q to go back)";
            this.label.Width = 4;
            this.label.Height = 1;
            this.label.X = 0;
            this.label.Y = 0;
            this.label.Data = "label";
            this.label.Text = "Date";
            this.label.TextAlignment = Terminal.Gui.TextAlignment.Left;
            this.Add(this.label);
            this.dateField.Width = 12;
            this.dateField.Height = 1;
            this.dateField.X = 5;
            this.dateField.Y = 0;
            this.dateField.Secret = false;
            this.dateField.Data = "dateField";
            this.dateField.Text = " 01.01.0001";
            this.dateField.TextAlignment = Terminal.Gui.TextAlignment.Left;
            this.Add(this.dateField);
            this.firstTeamLabel.Width = 20;
            this.firstTeamLabel.Height = 1;
            this.firstTeamLabel.X = 3;
            this.firstTeamLabel.Y = 3;
            this.firstTeamLabel.Data = "firstTeamLabel";
            this.firstTeamLabel.Text = "First Team Name";
            this.firstTeamLabel.TextAlignment = Terminal.Gui.TextAlignment.Right;
            this.Add(this.firstTeamLabel);
            this.secondTeamLabel.Width = 20;
            this.secondTeamLabel.Height = 1;
            this.secondTeamLabel.X = 25;
            this.secondTeamLabel.Y = 3;
            this.secondTeamLabel.Data = "secondTeamLabel";
            this.secondTeamLabel.Text = "Second Team Name";
            this.secondTeamLabel.TextAlignment = Terminal.Gui.TextAlignment.Left;
            this.Add(this.secondTeamLabel);
            this.setsListView.Width = 40;
            this.setsListView.Height = 6;
            this.setsListView.X = 4;
            this.setsListView.Y = 5;
            this.setsListView.Data = "setsListView";
            this.setsListView.TextAlignment = Terminal.Gui.TextAlignment.Centered;
            this.setsListView.Source = new Terminal.Gui.ListWrapper(new string[] {
                        "              999 999",
                        "Item2",
                        "Item3"});
            this.setsListView.AllowsMarking = false;
            this.setsListView.AllowsMultipleSelection = true;
            this.Add(this.setsListView);
            this.label2.Width = 2;
            this.label2.Height = 1;
            this.label2.X = 50;
            this.label2.Y = 5;
            this.label2.Data = "label2";
            this.label2.Text = "Edit Score";
            this.label2.TextAlignment = Terminal.Gui.TextAlignment.Left;
            this.Add(this.label2);
            this.firstScoreTextField.Width = 3;
            this.firstScoreTextField.Height = 1;
            this.firstScoreTextField.X = 51;
            this.firstScoreTextField.Y = 7;
            this.firstScoreTextField.Secret = false;
            this.firstScoreTextField.Data = "firstScoreTextField";
            this.firstScoreTextField.Text = "999";
            this.firstScoreTextField.TextAlignment = Terminal.Gui.TextAlignment.Left;
            this.Add(this.firstScoreTextField);
            this.secondScoreTextField.Width = 3;
            this.secondScoreTextField.Height = 4;
            this.secondScoreTextField.X = 55;
            this.secondScoreTextField.Y = 7;
            this.secondScoreTextField.Secret = false;
            this.secondScoreTextField.Data = "secondScoreTextField";
            this.secondScoreTextField.Text = "999";
            this.secondScoreTextField.TextAlignment = Terminal.Gui.TextAlignment.Left;
            this.Add(this.secondScoreTextField);
            this.saveNewScoreBtn.Width = 18;
            this.saveNewScoreBtn.Height = 1;
            this.saveNewScoreBtn.X = 46;
            this.saveNewScoreBtn.Y = 9;
            this.saveNewScoreBtn.Data = "saveNewScoreBtn";
            this.saveNewScoreBtn.Text = "Save New Score";
            this.saveNewScoreBtn.TextAlignment = Terminal.Gui.TextAlignment.Centered;
            this.saveNewScoreBtn.IsDefault = false;
            this.Add(this.saveNewScoreBtn);
            this.addSetBtn.Width = 11;
            this.addSetBtn.Height = 1;
            this.addSetBtn.X = 19;
            this.addSetBtn.Y = 12;
            this.addSetBtn.Data = "addSetBtn";
            this.addSetBtn.Text = "Add Set";
            this.addSetBtn.TextAlignment = Terminal.Gui.TextAlignment.Centered;
            this.addSetBtn.IsDefault = false;
            this.Add(this.addSetBtn);
            this.removeSetBtn.Width = 14;
            this.removeSetBtn.Height = 1;
            this.removeSetBtn.X = 18;
            this.removeSetBtn.Y = 14;
            this.removeSetBtn.Data = "removeSetBtn";
            this.removeSetBtn.Text = "Remove Set";
            this.removeSetBtn.TextAlignment = Terminal.Gui.TextAlignment.Centered;
            this.removeSetBtn.IsDefault = false;
            this.Add(this.removeSetBtn);
            this.setWinnerBtn.Width = 14;
            this.setWinnerBtn.Height = 1;
            this.setWinnerBtn.X = 18;
            this.setWinnerBtn.Y = 17;
            this.setWinnerBtn.Data = "setWinnerBtn";
            this.setWinnerBtn.Text = "Set Winner";
            this.setWinnerBtn.TextAlignment = Terminal.Gui.TextAlignment.Centered;
            this.setWinnerBtn.IsDefault = false;
            this.Add(this.setWinnerBtn);
        }
    }
}