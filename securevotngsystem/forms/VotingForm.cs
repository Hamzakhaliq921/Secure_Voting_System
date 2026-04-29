using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using securevotngsystem.Singleton;

namespace securevotngsystem.forms
{
    public partial class VotingForm : Form
    {
        private string _electionType;
        private int _userId;

        public string ElectionType => _electionType;

        public event EventHandler VoteCasted;

        public VotingForm(string electionType, int userId)
        {
            InitializeComponent();
            _electionType = electionType;
            _userId = userId;
            LoadCandidates();
        }

        private void LoadCandidates()
        {
            try
            {
                var db = DbManager.Instance;
                if (db.Connection.State != ConnectionState.Open)
                    db.Connection.Open();

                string query = "SELECT CandidateId, Name FROM Candidates WHERE ElectionType = @ElectionType";
                using (SqlCommand cmd = new SqlCommand(query, db.Connection))
                {
                    cmd.Parameters.AddWithValue("@ElectionType", _electionType);
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        cmbCandidates.DisplayMember = "Name";
                        cmbCandidates.ValueMember = "CandidateId";
                        cmbCandidates.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading candidates: " + ex.Message);
            }
            finally
            {
                DbManager.Instance.Connection.Close();
            }
        }

        private void btnVote_Click(object sender, EventArgs e)
        {
            if (cmbCandidates.SelectedValue == null)
            {
                MessageBox.Show("Please select a candidate.");
                return;
            }

            try
            {
                var db = DbManager.Instance;
                if (db.Connection.State != ConnectionState.Open)
                    db.Connection.Open();

                // Check if already voted
                string checkQuery = "SELECT COUNT(*) FROM Votes WHERE UserId = @UserId AND ElectionType = @ElectionType";
                using (SqlCommand checkCmd = new SqlCommand(checkQuery, db.Connection))
                {
                    checkCmd.Parameters.AddWithValue("@UserId", _userId);
                    checkCmd.Parameters.AddWithValue("@ElectionType", _electionType);
                    int count = (int)checkCmd.ExecuteScalar();
                    if (count > 0)
                    {
                        MessageBox.Show("You have already voted in this election.");
                        return;
                    }
                }

                // Insert vote
                string voteQuery = @"
                    INSERT INTO Votes 
                    (UserId, CandidateId, ElectionType, Timestamp, VoteDate) 
                    VALUES (@UserId, @CandidateId, @ElectionType, @Timestamp, @VoteDate)";
                using (SqlCommand cmd = new SqlCommand(voteQuery, db.Connection))
                {
                    DateTime now = DateTime.Now;
                    cmd.Parameters.AddWithValue("@UserId", _userId);
                    cmd.Parameters.AddWithValue("@CandidateId", cmbCandidates.SelectedValue);
                    cmd.Parameters.AddWithValue("@ElectionType", _electionType);
                    cmd.Parameters.AddWithValue("@Timestamp", now);
                    cmd.Parameters.AddWithValue("@VoteDate", now.Date);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("✅ Vote cast successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                VoteCasted?.Invoke(this, EventArgs.Empty);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error casting vote: " + ex.Message);
            }
            finally
            {
                DbManager.Instance.Connection.Close();
            }
        }
    }
}
