using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace securevotngsystem.forms
{
    public partial class GeneralVotingForm : Form
    {
        private readonly int _userId;

        public GeneralVotingForm(int userId)
        {
            _userId = userId;
            InitializeComponent();

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
            lblUserId.ForeColor = Color.FromArgb(0, 102, 51);
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
            btn.BackColor = Color.FromArgb(0, 153, 76);
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btn.Cursor = Cursors.Hand;
        }

        private void LoadCandidates()
        {
            var db = DbManager.Instance;

            try
            {
                if (db.Connection.State != System.Data.ConnectionState.Open)
                    db.Connection.Open();

                SqlCommand cmd = new SqlCommand("SELECT CandidateId, Name FROM Candidates WHERE ElectionType = 'General'", db.Connection);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                cmbCandidates.DataSource = dt;
                cmbCandidates.DisplayMember = "Name";
                cmbCandidates.ValueMember = "CandidateId";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading candidates: " + ex.Message);
            }
            finally
            {
                if (db.Connection.State == System.Data.ConnectionState.Open)
                    db.Connection.Close();
            }
        }

        private void btnVote_Click(object sender, EventArgs e)
        {
            if (cmbCandidates.SelectedValue == null)
            {
                MessageBox.Show("Please select a candidate.");
                return;
            }

            int candidateId = Convert.ToInt32(cmbCandidates.SelectedValue);

            try
            {
                IVoteStrategy strategy = new GeneralVoteStrategy();
                strategy.Vote(_userId, candidateId);

                MessageBox.Show("Vote cast successfully!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Vote failed: " + ex.Message);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
