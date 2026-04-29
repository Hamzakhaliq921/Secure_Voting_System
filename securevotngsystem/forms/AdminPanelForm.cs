using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using securevotngsystem.Singleton;

namespace securevotngsystem.forms
{
    public partial class AdminPanelForm : Form
    {
        public AdminPanelForm()
        {
            InitializeComponent();
            ApplyPakistaniTheme();
        }

        private void ApplyPakistaniTheme()
        {
            this.BackColor = Color.White;

            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is Label lbl)
                    lbl.ForeColor = Color.ForestGreen;
                else if (ctrl is ComboBox cmb)
                {
                    cmb.BackColor = Color.White;
                    cmb.ForeColor = Color.ForestGreen;
                }
                else if (ctrl is TextBox txt)
                {
                    txt.BackColor = Color.White;
                    txt.ForeColor = Color.DarkGreen;
                }
                else if (ctrl is Button btn)
                    StyleButton(btn);
            }

            // DataGridView Styling
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.DefaultCellStyle.BackColor = Color.White;
            dataGridView1.DefaultCellStyle.ForeColor = Color.DarkGreen;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.ForestGreen;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.EnableHeadersVisualStyles = false;
        }

        private void StyleButton(Button btn)
        {
            btn.BackColor = Color.ForestGreen;
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
        }

        private void btnAddCandidate_Click(object sender, EventArgs e)
        {
            string name = txtCandidateName.Text.Trim();
            string position = txtCandidatePosition.Text.Trim();
            string party = txtCandidateParty.Text.Trim();
            string electionType = cmbElectionType.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(position) || string.IsNullOrEmpty(party) || string.IsNullOrEmpty(electionType))
            {
                MessageBox.Show("Please enter candidate name, position, party, and select election type.");
                return;
            }

            try
            {
                var db = DbManager.Instance;
                if (db.Connection.State != ConnectionState.Open)
                    db.Connection.Open();

                string query = "INSERT INTO Candidates (Name, Position, Party, ElectionType) VALUES (@Name, @Position, @Party, @ElectionType)";
                using (SqlCommand cmd = new SqlCommand(query, db.Connection))
                {
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Position", position);
                    cmd.Parameters.AddWithValue("@Party", party);
                    cmd.Parameters.AddWithValue("@ElectionType", electionType);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Candidate added successfully!");
                txtCandidateName.Clear();
                txtCandidatePosition.Clear();
                txtCandidateParty.Clear();
                cmbElectionType.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding candidate: " + ex.Message);
            }
            finally
            {
                DbManager.Instance.Connection.Close();
            }
        }

        private void btnViewCandidates_Click(object sender, EventArgs e)
        {
            try
            {
                var db = DbManager.Instance;
                if (db.Connection.State != ConnectionState.Open)
                    db.Connection.Open();

                string query = "SELECT CandidateId, Name, Position, Party, ElectionType FROM Candidates";
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, db.Connection))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt;
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

        private void btnViewResults_Click(object sender, EventArgs e)
        {
            try
            {
                var db = DbManager.Instance;
                if (db.Connection.State != ConnectionState.Open)
                    db.Connection.Open();

                string query = @"
                    SELECT c.Name, c.Position, c.Party, c.ElectionType, COUNT(v.VoteId) AS VoteCount
                    FROM Votes v
                    INNER JOIN Candidates c ON v.CandidateId = c.CandidateId
                    GROUP BY c.Name, c.Position, c.Party, c.ElectionType";

                using (SqlDataAdapter adapter = new SqlDataAdapter(query, db.Connection))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading results: " + ex.Message);
            }
            finally
            {
                DbManager.Instance.Connection.Close();
            }
        }

        private void btnDeleteCandidate_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a candidate to delete.");
                return;
            }

            DialogResult confirm = MessageBox.Show("Are you sure you want to delete the selected candidate?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm != DialogResult.Yes) return;

            try
            {
                var selectedRow = dataGridView1.SelectedRows[0];
                int candidateId = Convert.ToInt32(selectedRow.Cells["CandidateId"].Value);

                var db = DbManager.Instance;
                if (db.Connection.State != ConnectionState.Open)
                    db.Connection.Open();

                string query = "DELETE FROM Candidates WHERE CandidateId = @CandidateId";
                using (SqlCommand cmd = new SqlCommand(query, db.Connection))
                {
                    cmd.Parameters.AddWithValue("@CandidateId", candidateId);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Candidate deleted successfully!");
                btnViewCandidates_Click(null, null); // Refresh list
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting candidate: " + ex.Message);
            }
            finally
            {
                DbManager.Instance.Connection.Close();
            }
        }
    }
}
