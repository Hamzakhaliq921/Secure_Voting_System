using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using securevotngsystem.Singleton;

namespace securevotngsystem.forms
{
    public partial class VoteDashboardForm : Form
    {
        private int _userId;

        private bool hasVotedLocal = false;
        private bool hasVotedGeneral = false;

        public VoteDashboardForm(int userId)
        {
            InitializeComponent();
            _userId = userId;

            CheckUserVotingStatus();
            ApplyButtonStates();
        }

        private void CheckUserVotingStatus()
        {
            try
            {
                var db = DbManager.Instance;
                if (db.Connection.State != ConnectionState.Open)
                    db.Connection.Open();

                string query = "SELECT ElectionType FROM Votes WHERE UserId = @UserId";

                using (SqlCommand cmd = new SqlCommand(query, db.Connection))
                {
                    cmd.Parameters.AddWithValue("@UserId", _userId);

                    hasVotedLocal = false;
                    hasVotedGeneral = false;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string electionType = reader["ElectionType"].ToString();
                            if (string.Equals(electionType, "Local", StringComparison.OrdinalIgnoreCase))
                                hasVotedLocal = true;
                            else if (string.Equals(electionType, "General", StringComparison.OrdinalIgnoreCase))
                                hasVotedGeneral = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error checking vote status: " + ex.Message);
            }
            finally
            {
                if (DbManager.Instance.Connection.State == ConnectionState.Open)
                    DbManager.Instance.Connection.Close();
            }
        }

        private void ApplyButtonStates()
        {
            btnVoteLocal.Enabled = !hasVotedLocal && !hasVotedGeneral;
            btnVoteGeneral.Enabled = !hasVotedGeneral && !hasVotedLocal;
        }

        private void OpenVotingForm(string electionType)
        {
            VotingForm votingForm = new VotingForm(electionType, _userId);
            votingForm.VoteCasted += VotingForm_VoteCasted;
            votingForm.ShowDialog();
        }

        private void VotingForm_VoteCasted(object sender, EventArgs e)
        {
            if (sender is VotingForm votingForm)
            {
                if (string.Equals(votingForm.ElectionType, "Local", StringComparison.OrdinalIgnoreCase))
                    hasVotedLocal = true;
                else if (string.Equals(votingForm.ElectionType, "General", StringComparison.OrdinalIgnoreCase))
                    hasVotedGeneral = true;

                btnVoteLocal.Enabled = false;
                btnVoteGeneral.Enabled = false;

                MessageBox.Show("Thank you for voting! You cannot vote in both elections during this session.");
            }
        }

        private void LoadResults(string electionType)
        {
            try
            {
                var db = DbManager.Instance;
                if (db.Connection.State != ConnectionState.Open)
                    db.Connection.Open();

                string query = @"
                    SELECT 
                        Name, Position, Party, 
                        COUNT(v.VoteId) AS VoteCount
                    FROM Candidates c
                    LEFT JOIN Votes v ON c.CandidateId = v.CandidateId
                    WHERE c.ElectionType = @ElectionType
                    GROUP BY Name, Position, Party
                    ORDER BY VoteCount DESC";

                using (SqlCommand cmd = new SqlCommand(query, db.Connection))
                {
                    cmd.Parameters.AddWithValue("@ElectionType", electionType);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dataGridViewResults.DataSource = dt;

                        // Styling DataGridView
                        dataGridViewResults.EnableHeadersVisualStyles = false;
                        dataGridViewResults.BackgroundColor = Color.White;
                        dataGridViewResults.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 255, 240); // Light green
                        dataGridViewResults.DefaultCellStyle.BackColor = Color.White;
                        dataGridViewResults.DefaultCellStyle.ForeColor = Color.Black;
                        dataGridViewResults.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 128, 0); // Dark green
                        dataGridViewResults.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                        dataGridViewResults.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
                        dataGridViewResults.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
                        dataGridViewResults.RowHeadersVisible = false;
                        dataGridViewResults.GridColor = Color.LightGray;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading results: " + ex.Message);
            }
            finally
            {
                if (DbManager.Instance.Connection.State == ConnectionState.Open)
                    DbManager.Instance.Connection.Close();
            }
        }

        private void BtnVoteHistory_Click(object sender, EventArgs e)
        {
            VoteHistoryForm historyForm = new VoteHistoryForm(_userId);
            historyForm.ShowDialog();
        }
    }
}
