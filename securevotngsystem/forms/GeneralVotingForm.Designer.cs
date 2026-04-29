using System.Windows.Forms;
using System.Drawing;

namespace securevotngsystem.forms
{
    partial class GeneralVotingForm
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblUserId;
        private ComboBox cmbCandidates;
        private Button btnVote;
        private Button btnBack;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblUserId = new Label();
            this.cmbCandidates = new ComboBox();
            this.btnVote = new Button();
            this.btnBack = new Button();

            this.SuspendLayout();

            // lblUserId
            this.lblUserId.AutoSize = true;
            this.lblUserId.Location = new Point(20, 20);
            this.lblUserId.Name = "lblUserId";
            this.lblUserId.Size = new Size(60, 15);
            this.lblUserId.TabIndex = 0;
            this.lblUserId.Text = "User ID:";

            // cmbCandidates
            this.cmbCandidates.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbCandidates.FormattingEnabled = true;
            this.cmbCandidates.Location = new Point(20, 60);
            this.cmbCandidates.Name = "cmbCandidates";
            this.cmbCandidates.Size = new Size(340, 23);
            this.cmbCandidates.TabIndex = 1;

            // btnVote
            this.btnVote.Location = new Point(20, 100);
            this.btnVote.Name = "btnVote";
            this.btnVote.Size = new Size(100, 35);
            this.btnVote.TabIndex = 2;
            this.btnVote.Text = "Cast Vote";
            this.btnVote.UseVisualStyleBackColor = false;

            // btnBack
            this.btnBack.Location = new Point(140, 100);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new Size(100, 35);
            this.btnBack.TabIndex = 3;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = false;

            // GeneralVotingForm
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(400, 160);
            this.Controls.Add(this.lblUserId);
            this.Controls.Add(this.cmbCandidates);
            this.Controls.Add(this.btnVote);
            this.Controls.Add(this.btnBack);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GeneralVotingForm";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "General Voting";

            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
