namespace securevotngsystem.forms
{
    partial class LocalVotingForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblUserId;
        private System.Windows.Forms.ComboBox cmbCandidates;
        private System.Windows.Forms.Button btnVote;
        private System.Windows.Forms.Button btnBack;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblUserId = new System.Windows.Forms.Label();
            this.cmbCandidates = new System.Windows.Forms.ComboBox();
            this.btnVote = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();

            this.SuspendLayout();

            // lblUserId
            this.lblUserId.AutoSize = true;
            this.lblUserId.Location = new System.Drawing.Point(20, 20);
            this.lblUserId.Name = "lblUserId";
            this.lblUserId.Size = new System.Drawing.Size(60, 15);
            this.lblUserId.TabIndex = 0;
            this.lblUserId.Text = "User ID:";

            // cmbCandidates
            this.cmbCandidates.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCandidates.FormattingEnabled = true;
            this.cmbCandidates.Location = new System.Drawing.Point(20, 60);
            this.cmbCandidates.Name = "cmbCandidates";
            this.cmbCandidates.Size = new System.Drawing.Size(340, 23);
            this.cmbCandidates.TabIndex = 1;

            // btnVote
            this.btnVote.Location = new System.Drawing.Point(20, 100);
            this.btnVote.Name = "btnVote";
            this.btnVote.Size = new System.Drawing.Size(100, 35);
            this.btnVote.TabIndex = 2;
            this.btnVote.Text = "Cast Vote";
            this.btnVote.UseVisualStyleBackColor = true;

            // btnBack
            this.btnBack.Location = new System.Drawing.Point(140, 100);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(100, 35);
            this.btnBack.TabIndex = 3;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = true;

            // LocalVotingForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 160);
            this.Controls.Add(this.lblUserId);
            this.Controls.Add(this.cmbCandidates);
            this.Controls.Add(this.btnVote);
            this.Controls.Add(this.btnBack);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LocalVotingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Local Voting";

            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
