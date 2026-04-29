using System;
using System.Windows.Forms;
namespace securevotngsystem.forms
{
    partial class AdminPanelForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ComboBox cmbElectionType;
        private System.Windows.Forms.TextBox txtCandidateName;
        private System.Windows.Forms.TextBox txtCandidatePosition;
        private System.Windows.Forms.TextBox txtCandidateParty;
        private System.Windows.Forms.Button btnAddCandidate;
        private System.Windows.Forms.Button btnViewCandidates;
        private System.Windows.Forms.Button btnViewResults;
        private System.Windows.Forms.Button btnDeleteCandidate;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label lblCandidateName;
        private System.Windows.Forms.Label lblCandidatePosition;
        private System.Windows.Forms.Label lblCandidateParty;
        private System.Windows.Forms.Label lblElectionType;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.cmbElectionType = new System.Windows.Forms.ComboBox();
            this.txtCandidateName = new System.Windows.Forms.TextBox();
            this.txtCandidatePosition = new System.Windows.Forms.TextBox();
            this.txtCandidateParty = new System.Windows.Forms.TextBox();
            this.btnAddCandidate = new System.Windows.Forms.Button();
            this.btnViewCandidates = new System.Windows.Forms.Button();
            this.btnViewResults = new System.Windows.Forms.Button();
            this.btnDeleteCandidate = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.lblCandidateName = new System.Windows.Forms.Label();
            this.lblCandidatePosition = new System.Windows.Forms.Label();
            this.lblCandidateParty = new System.Windows.Forms.Label();
            this.lblElectionType = new System.Windows.Forms.Label();

            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();

            // lblCandidateName
            this.lblCandidateName.AutoSize = true;
            this.lblCandidateName.Location = new System.Drawing.Point(30, 30);
            this.lblCandidateName.Text = "Candidate Name:";

            // txtCandidateName
            this.txtCandidateName.Location = new System.Drawing.Point(150, 27);
            this.txtCandidateName.Size = new System.Drawing.Size(200, 22);

            // lblCandidatePosition
            this.lblCandidatePosition.AutoSize = true;
            this.lblCandidatePosition.Location = new System.Drawing.Point(30, 70);
            this.lblCandidatePosition.Text = "Candidate Position:";

            // txtCandidatePosition
            this.txtCandidatePosition.Location = new System.Drawing.Point(150, 67);
            this.txtCandidatePosition.Size = new System.Drawing.Size(200, 22);

            // lblCandidateParty
            this.lblCandidateParty.AutoSize = true;
            this.lblCandidateParty.Location = new System.Drawing.Point(30, 110);
            this.lblCandidateParty.Text = "Candidate Party:";

            // txtCandidateParty
            this.txtCandidateParty.Location = new System.Drawing.Point(150, 107);
            this.txtCandidateParty.Size = new System.Drawing.Size(200, 22);

            // lblElectionType
            this.lblElectionType.AutoSize = true;
            this.lblElectionType.Location = new System.Drawing.Point(30, 150);
            this.lblElectionType.Text = "Election Type:";

            // cmbElectionType
            this.cmbElectionType.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbElectionType.Items.AddRange(new object[] { "General", "Local" });
            this.cmbElectionType.Location = new System.Drawing.Point(150, 147);
            this.cmbElectionType.Size = new System.Drawing.Size(200, 24);

            // btnAddCandidate
            this.btnAddCandidate.Location = new System.Drawing.Point(370, 60);
            this.btnAddCandidate.Size = new System.Drawing.Size(130, 30);
            this.btnAddCandidate.Text = "Add Candidate";
            this.btnAddCandidate.Click += new System.EventHandler(this.btnAddCandidate_Click);

            // btnViewCandidates
            this.btnViewCandidates.Location = new System.Drawing.Point(30, 190);
            this.btnViewCandidates.Size = new System.Drawing.Size(130, 30);
            this.btnViewCandidates.Text = "View Candidates";
            this.btnViewCandidates.Click += new System.EventHandler(this.btnViewCandidates_Click);

            // btnViewResults
            this.btnViewResults.Location = new System.Drawing.Point(180, 190);
            this.btnViewResults.Size = new System.Drawing.Size(130, 30);
            this.btnViewResults.Text = "View Results";
            this.btnViewResults.Click += new System.EventHandler(this.btnViewResults_Click);

            // btnDeleteCandidate
            this.btnDeleteCandidate.Location = new System.Drawing.Point(330, 190);
            this.btnDeleteCandidate.Size = new System.Drawing.Size(130, 30);
            this.btnDeleteCandidate.Text = "Delete Candidate";
            this.btnDeleteCandidate.Click += new System.EventHandler(this.btnDeleteCandidate_Click);

            // dataGridView1
            this.dataGridView1.Location = new System.Drawing.Point(30, 240);
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Size = new System.Drawing.Size(640, 200);

            // AdminPanelForm
            this.ClientSize = new System.Drawing.Size(720, 470);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnDeleteCandidate);
            this.Controls.Add(this.btnViewResults);
            this.Controls.Add(this.btnViewCandidates);
            this.Controls.Add(this.btnAddCandidate);
            this.Controls.Add(this.cmbElectionType);
            this.Controls.Add(this.lblElectionType);
            this.Controls.Add(this.txtCandidateParty);
            this.Controls.Add(this.lblCandidateParty);
            this.Controls.Add(this.txtCandidatePosition);
            this.Controls.Add(this.lblCandidatePosition);
            this.Controls.Add(this.txtCandidateName);
            this.Controls.Add(this.lblCandidateName);
            this.Text = "Admin Panel";

            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
