using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace securevotngsystem.forms
{
    public partial class LocalVotingForm : Form
    {
        private int _userId;

        public LocalVotingForm(int userId)
        {
            InitializeComponent();
            _userId = userId;

            StyleControls();

            lblUserId.Text = $"User ID: {_userId}";

            LoadCandidates();

            btnVote.Click += btnVote_Click;
            btnBack.Click += btnBack_Click;
        }

        private void StyleControls()
        {
            // Form style
            this.BackColor = Color.White;
            this.Font = new Font("Segoe UI", 9F);

            // Label styling
            lblUserId.ForeColor = Color.FromArgb(0, 102, 51); // Dark Green
            lblUserId.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            // ComboBox styling
            cmbCandidates.BackColor = Color.White;
            cmbCandidates.ForeColor = Color.FromArgb(0, 102, 51);
            cmbCandidates.Font = new Font("Segoe UI", 10, FontStyle.Regular);

            // Buttons styling
            StyleButton(btnVote);
            StyleButton(btnBack);
        }

        private void StyleButton(Button btn)
        {
            btn.BackColor = Color.FromArgb(0, 153, 76); // Medium Green
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btn.Cursor = Cursors.Hand;
        }

        private void LoadCandidates()
        {
            try
            {
                var db = DbManager.Instance;
                db.Connection.Open();

                SqlCommand cmd = new SqlCommand("SELECT CandidateId, Name FROM Candidates WHERE ElectionType = 'Local'", db.Connection);
                SqlDataReader reader = cmd.ExecuteReader();

                Dictionary<int, string> candidates = new Dictionary<int, string>();
                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader["CandidateId"]);
                    string name = reader["Name"].ToString();
                    candidates.Add(id, name);
                }

                cmbCandidates.DataSource = new BindingSource(candidates, null);
                cmbCandidates.DisplayMember = "Value";
                cmbCandidates.ValueMember = "Key";

                reader.Close();
                db.Connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading candidates: " + ex.Message);
            }
        }

        private void btnVote_Click(object sender, EventArgs e)
        {
            if (cmbCandidates.SelectedItem == null)
            {
                MessageBox.Show("Please select a candidate.");
                return;
            }

            int candidateId = ((KeyValuePair<int, string>)cmbCandidates.SelectedItem).Key;

            try
            {
                var db = DbManager.Instance;
                db.Connection.Open();

                SqlCommand voteCmd = new SqlCommand(
                    "INSERT INTO Votes (UserId, CandidateId, ElectionType) VALUES (@UserId, @CandidateId, 'Local')",
                    db.Connection);
                voteCmd.Parameters.AddWithValue("@UserId", _userId);
                voteCmd.Parameters.AddWithValue("@CandidateId", candidateId);
                voteCmd.ExecuteNonQuery();

                SqlCommand updateCmd = new SqlCommand(
                    "UPDATE Users SET HasVotedLocal = 1 WHERE UserId = @UserId",
                    db.Connection);
                updateCmd.Parameters.AddWithValue("@UserId", _userId);
                updateCmd.ExecuteNonQuery();

                db.Connection.Close();

                MessageBox.Show("Vote cast successfully!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error casting vote: " + ex.Message);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close(); // Returns to dashboard
        }
    }
}
