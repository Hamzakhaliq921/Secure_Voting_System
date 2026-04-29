using System.Windows.Forms;
using System.Drawing;

namespace securevotngsystem.forms
{
    partial class VoteDashboardForm
    {
        private System.ComponentModel.IContainer components = null;

        private Panel localElectionPanel;
        private Panel generalElectionPanel;
        private DataGridView dataGridViewResults;

        private Button btnVoteLocal;
        private Button btnVoteGeneral;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            this.SuspendLayout();

            // 
            // VoteDashboardForm
            // 
            this.ClientSize = new Size(800, 600);
            this.Name = "VoteDashboardForm";
            this.Text = "Vote Dashboard";
            this.BackColor = Color.White;

            // Local Election Panel
            localElectionPanel = new Panel
            {
                Location = new Point(20, 20),
                Size = new Size(350, 150),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.WhiteSmoke
            };

            Label lblLocal = new Label
            {
                Text = "Local Election",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Location = new Point(10, 10),
                AutoSize = true,
                ForeColor = Color.FromArgb(0, 102, 51) // Dark Green
            };

            btnVoteLocal = new Button
            {
                Text = "Vote",
                Location = new Point(10, 50),
                Size = new Size(150, 40),
                BackColor = Color.FromArgb(0, 153, 76), // Pakistani Green
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat
            };
            btnVoteLocal.FlatAppearance.BorderSize = 0;
            btnVoteLocal.Click += (s, e) => OpenVotingForm("Local");

            Button btnResultLocal = new Button
            {
                Text = "See Result",
                Location = new Point(180, 50),
                Size = new Size(150, 40),
                BackColor = Color.FromArgb(0, 153, 76),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat
            };
            btnResultLocal.FlatAppearance.BorderSize = 0;
            btnResultLocal.Click += (s, e) => LoadResults("Local");

            localElectionPanel.Controls.Add(lblLocal);
            localElectionPanel.Controls.Add(btnVoteLocal);
            localElectionPanel.Controls.Add(btnResultLocal);

            // General Election Panel
            generalElectionPanel = new Panel
            {
                Location = new Point(400, 20),
                Size = new Size(350, 150),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.WhiteSmoke
            };

            Label lblGeneral = new Label
            {
                Text = "General Election",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Location = new Point(10, 10),
                AutoSize = true,
                ForeColor = Color.FromArgb(0, 102, 51)
            };

            btnVoteGeneral = new Button
            {
                Text = "Vote",
                Location = new Point(10, 50),
                Size = new Size(150, 40),
                BackColor = Color.FromArgb(0, 153, 76),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat
            };
            btnVoteGeneral.FlatAppearance.BorderSize = 0;
            btnVoteGeneral.Click += (s, e) => OpenVotingForm("General");

            Button btnResultGeneral = new Button
            {
                Text = "See Result",
                Location = new Point(180, 50),
                Size = new Size(150, 40),
                BackColor = Color.FromArgb(0, 153, 76),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat
            };
            btnResultGeneral.FlatAppearance.BorderSize = 0;
            btnResultGeneral.Click += (s, e) => LoadResults("General");

            generalElectionPanel.Controls.Add(lblGeneral);
            generalElectionPanel.Controls.Add(btnVoteGeneral);
            generalElectionPanel.Controls.Add(btnResultGeneral);

            // DataGridView for Results
            dataGridViewResults = new DataGridView
            {
                Location = new Point(20, 200),
                Size = new Size(730, 300),
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            // Vote History Button
            Button btnVoteHistory = new Button
            {
                Text = "View Vote History",
                Location = new Point(20, 520),
                Size = new Size(200, 40),
                BackColor = Color.FromArgb(0, 153, 76),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat
            };
            btnVoteHistory.FlatAppearance.BorderSize = 0;
            btnVoteHistory.Click += BtnVoteHistory_Click;

            // Add controls to form
            this.Controls.Add(localElectionPanel);
            this.Controls.Add(generalElectionPanel);
            this.Controls.Add(dataGridViewResults);
            this.Controls.Add(btnVoteHistory);

            this.ResumeLayout(false);
        }
    }
}
