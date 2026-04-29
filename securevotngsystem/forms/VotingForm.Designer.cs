namespace securevotngsystem.forms
{
    partial class VotingForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ComboBox cmbCandidates;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnVote;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.cmbCandidates = new System.Windows.Forms.ComboBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnVote = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblTitle.Location = new System.Drawing.Point(30, 30);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(146, 28);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Select Candidate:";

            // cmbCandidates
            this.cmbCandidates.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCandidates.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbCandidates.FormattingEnabled = true;
            this.cmbCandidates.Location = new System.Drawing.Point(30, 70);
            this.cmbCandidates.Name = "cmbCandidates";
            this.cmbCandidates.Size = new System.Drawing.Size(300, 31);
            this.cmbCandidates.TabIndex = 1;

            // btnVote
            this.btnVote.BackColor = System.Drawing.Color.Green;
            this.btnVote.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVote.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnVote.ForeColor = System.Drawing.Color.White;
            this.btnVote.Location = new System.Drawing.Point(30, 120);
            this.btnVote.Name = "btnVote";
            this.btnVote.Size = new System.Drawing.Size(100, 35);
            this.btnVote.TabIndex = 2;
            this.btnVote.Text = "Vote";
            this.btnVote.UseVisualStyleBackColor = false;
            this.btnVote.Click += new System.EventHandler(this.btnVote_Click);

            // VotingForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(380, 200);
            this.Controls.Add(this.btnVote);
            this.Controls.Add(this.cmbCandidates);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "VotingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "🇵🇰 Cast Your Vote";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
