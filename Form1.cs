using System;
using System.Drawing;
using System.Windows.Forms;

namespace Vehicle_Rental_Management_System
{
    public partial class Form1 : Form
    {
        // Start at 4 because IDs 001, 002, and 003 already exist.
        private int nextVehicleId = 4;

        private int nextRentalId = 1;

        public Form1()
        {
            InitializeComponent();

            btnNewRental.Click -= btnNewRental_Click;
            btnNewRental.Click += btnNewRental_Click;

            // Edit Rental
            btnEditRental.Click -= btnEditRental_Click;
            btnEditRental.Click += btnEditRental_Click;

            // Return Vehicle
            btnReturnVehicle.Click -= btnReturnVehicle_Click;
            btnReturnVehicle.Click += btnReturnVehicle_Click;

            // Delete Rental
            btnDeleteRental.Click -= btnDeleteRental_Click;
            btnDeleteRental.Click += btnDeleteRental_Click;

            pnlRentals.Bounds = pnlDashboard.Bounds;
            pnlRentals.Visible = false;

            btnRentals.Click -= btnRentals_Click;
            btnRentals.Click += btnRentals_Click;

            // Connect button Click events.
            // Removing first prevents duplicate event calls.
            btnAdd.Click -= btnAdd_Click;
            btnAdd.Click += btnAdd_Click;

            btnEdit.Click -= btnEdit_Click;
            btnEdit.Click += btnEdit_Click;

            btnDelete.Click -= btnDelete_Click;
            btnDelete.Click += btnDelete_Click;

            btnDashboard.Click -= btnDashboard_Click;
            btnDashboard.Click += btnDashboard_Click;

            btnVehicles.Click -= btnVehicles_Click;
            btnVehicles.Click += btnVehicles_Click;


            // DataGridView settings
            dgvVehicles.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvVehicles.MultiSelect = false;
            dgvVehicles.ReadOnly = true;
            dgvVehicles.AllowUserToAddRows = false;

            // Sample vehicles
            dgvVehicles.Rows.Add(
                "001", "Toyota Vios", "Available");

            dgvVehicles.Rows.Add(
                "002", "Mitsubishi Xpander", "Rented");

            dgvVehicles.Rows.Add(
                "003", "Toyota Hiace", "Available");

            // Start on the Vehicles page
            pnlDashboard.Visible = false;

            SetActiveButton(btnVehicles);

            // Clear initial row selection
            dgvVehicles.ClearSelection();

            // Update Dashboard statistics
            UpdateDashboard();
        }

        // ======================================
        // NAVIGATION BUTTON COLORS
        // ======================================

        private void SetActiveButton(Button activeButton)
        {
            Color activeColor =
                Color.FromArgb(30, 101, 200);

            Color normalColor =
                Color.FromArgb(231, 238, 248);

            Color darkText =
                Color.FromArgb(25, 48, 75);

            // Reset navigation buttons
            btnDashboard.BackColor = normalColor;
            btnDashboard.ForeColor = darkText;

            btnVehicles.BackColor = normalColor;
            btnVehicles.ForeColor = darkText;

            btnRentals.BackColor = normalColor;
            btnRentals.ForeColor = darkText;

            // Highlight selected button
            activeButton.BackColor = activeColor;
            activeButton.ForeColor = Color.White;
        }

        // ======================================
        // DASHBOARD BUTTON
        // ======================================

        private void btnDashboard_Click(
            object sender, EventArgs e)
        {
            pnlRentals.Visible = false;

            pnlDashboard.Visible = true;
            pnlDashboard.BringToFront();

            pnlMaintenance.Visible = true;
            pnlMaintenance.BringToFront();

            UpdateDashboard();

            SetActiveButton(btnDashboard);
        }

        // ======================================
        // VEHICLES BUTTON
        // ======================================

        private void btnVehicles_Click(
            object sender, EventArgs e)
        {
            pnlDashboard.Visible = false;
            pnlRentals.Visible = false;

            SetActiveButton(btnVehicles);
        }

        // ======================================
        // CREATE - ADD VEHICLE
        // ======================================

        private void btnAdd_Click(
            object sender, EventArgs e)
        {
            using (frmVehicle form = new frmVehicle())
            {
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    string id =
                        nextVehicleId.ToString("D3");

                    dgvVehicles.Rows.Add(
                        id,
                        form.VehicleName,
                        form.VehicleStatus
                    );

                    nextVehicleId++;

                    UpdateDashboard();

                    dgvVehicles.ClearSelection();

                    MessageBox.Show(
                        "Vehicle added successfully!",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
            }
        }

        // ======================================
        // UPDATE - EDIT VEHICLE
        // ======================================

        private void btnEdit_Click(
            object sender, EventArgs e)
        {
            if (dgvVehicles.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "Please select a vehicle to edit.",
                    "No Vehicle Selected",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            DataGridViewRow row =
                dgvVehicles.SelectedRows[0];

            string vehicleName =
                row.Cells["colVehicle"].Value?
                .ToString() ?? "";

            string vehicleStatus =
                row.Cells["colStatus"].Value?
                .ToString() ?? "";

            using (frmVehicle form =
                new frmVehicle(vehicleName, vehicleStatus))
            {
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    row.Cells["colVehicle"].Value =
                        form.VehicleName;

                    row.Cells["colStatus"].Value =
                        form.VehicleStatus;

                    UpdateDashboard();

                    dgvVehicles.ClearSelection();

                    MessageBox.Show(
                        "Vehicle updated successfully!",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
            }
        }

        // ======================================
        // DELETE - REMOVE VEHICLE
        // ======================================

        private void btnDelete_Click(
            object sender, EventArgs e)
        {
            if (dgvVehicles.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "Please select a vehicle to delete.",
                    "No Vehicle Selected",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            DataGridViewRow row =
                dgvVehicles.SelectedRows[0];

            string vehicleName =
                row.Cells["colVehicle"].Value?
                .ToString() ?? "";

            DialogResult result = MessageBox.Show(
                "Are you sure you want to delete "
                + vehicleName + "?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                dgvVehicles.Rows.Remove(row);

                UpdateDashboard();

                dgvVehicles.ClearSelection();

                MessageBox.Show(
                    "Vehicle deleted successfully!",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
        }

        // ======================================
        // DASHBOARD STATISTICS
        // ======================================

        private void UpdateDashboard()
        {
            int total = 0;
            int available = 0;
            int rented = 0;
            int maintenance = 0;

            foreach (DataGridViewRow row in dgvVehicles.Rows)
            {
                if (row.IsNewRow)
                    continue;

                total++;

                string status =
                    row.Cells["colStatus"].Value?
                    .ToString()?.Trim() ?? "";

                if (status.Equals(
                    "Available",
                    StringComparison.OrdinalIgnoreCase))
                {
                    available++;
                }
                else if (status.Equals(
                    "Rented",
                    StringComparison.OrdinalIgnoreCase))
                {
                    rented++;
                }
                else if (status.Equals(
                    "Maintenance",
                    StringComparison.OrdinalIgnoreCase))
                {
                    maintenance++;
                }
            }

            lblTotalVehicles.Text =
                total.ToString();

            lblAvailableVehicles.Text =
                available.ToString();

            lblRentedVehicles.Text =
                rented.ToString();

            lblMaintenanceVehicles.Text =
                maintenance.ToString();
        }

        // ======================================
        // EXISTING DESIGNER EVENTS
        // ======================================

        private void dgvVehicles_CellContentClick(
            object sender, DataGridViewCellEventArgs e)
        {
        }

        private void label3_Click(
            object sender, EventArgs e)
        {
        }

        private void label7_Click(
            object sender, EventArgs e)
        {
        }

        private void label8_Click(
            object sender, EventArgs e)
        {
        }

        private void label1_Click(
            object sender, EventArgs e)
        {
        }

        private void lblRentedVehicles_Click(
            object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void btnDeleteRental_Click(object sender, EventArgs e)
        {
            // Check if a rental is selected
            if (dgvRentals.CurrentRow == null ||
                dgvRentals.CurrentRow.IsNewRow)
            {
                MessageBox.Show(
                    "Please select a rental to delete.",
                    "No Rental Selected",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow row = dgvRentals.CurrentRow;

            string customer =
                row.Cells["colCustomer"].Value?.ToString() ?? "";

            string status =
                row.Cells["colRentalStatus"].Value?
                .ToString()?.Trim() ?? "";

            // Prevent deleting active rentals
            if (status.Equals(
                "Active",
                StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show(
                    "Please return the vehicle before deleting this rental.",
                    "Active Rental",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            // Ask for confirmation
            DialogResult result = MessageBox.Show(
                "Are you sure you want to delete the rental of "
                + customer + "?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Remove rental record
                dgvRentals.Rows.Remove(row);

                MessageBox.Show(
                    "Rental deleted successfully!",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        private void btnNewRental_Click(object sender, EventArgs e)
        {
            using (frmRental form = new frmRental())
            {
                // Load only AVAILABLE vehicles
                foreach (DataGridViewRow row in dgvVehicles.Rows)
                {
                    if (row.IsNewRow)
                        continue;

                    string status =
                        row.Cells["colStatus"].Value?
                        .ToString()?.Trim() ?? "";

                    if (status.Equals(
                        "Available",
                        StringComparison.OrdinalIgnoreCase))
                    {
                        string id =
                            row.Cells["colID"].Value?
                            .ToString() ?? "";

                        string name =
                            row.Cells["colVehicle"].Value?
                            .ToString() ?? "";

                        form.AddAvailableVehicle(id, name);
                    }
                }

                // Check if there are available vehicles
                if (form.AvailableVehicleCount == 0)
                {
                    MessageBox.Show(
                        "No vehicles are currently available.",
                        "No Available Vehicles",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                // Open Rental Details
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    // Find the selected vehicle by its ID
                    DataGridViewRow selectedVehicle = null;

                    foreach (DataGridViewRow row in dgvVehicles.Rows)
                    {
                        if (row.IsNewRow)
                            continue;

                        string id =
                            row.Cells["colID"].Value?
                            .ToString() ?? "";

                        if (id == form.SelectedVehicleId)
                        {
                            selectedVehicle = row;
                            break;
                        }
                    }

                    // Verify vehicle is still available
                    if (selectedVehicle == null ||
                        !string.Equals(
                            selectedVehicle.Cells["colStatus"]
                                .Value?.ToString(),
                            "Available",
                            StringComparison.OrdinalIgnoreCase))
                    {
                        MessageBox.Show(
                            "The selected vehicle is no longer available.");
                        return;
                    }

                    // Generate rental ID
                    string rentalId =
                        nextRentalId.ToString("D3");

                    // Add a new rental row
                    int index = dgvRentals.Rows.Add();

                    DataGridViewRow rentalRow =
                        dgvRentals.Rows[index];

                    rentalRow.Cells["colRentalID"].Value =
                        rentalId;

                    rentalRow.Cells["colCustomer"].Value =
                        form.CustomerName;

                    rentalRow.Cells["colRentalVehicle"].Value =
                        form.SelectedVehicleName;

                    rentalRow.Cells["colStartDate"].Value =
                        form.StartDate.ToShortDateString();

                    rentalRow.Cells["colEndDate"].Value =
                        form.EndDate.ToShortDateString();

                    rentalRow.Cells["colRentalStatus"].Value =
                        "Active";

                    rentalRow.Cells["colRentalVehicleID"].Value =
                        form.SelectedVehicleId;

                    // Change vehicle status to Rented
                    selectedVehicle.Cells["colStatus"].Value =
                        "Rented";

                    nextRentalId++;

                    // Refresh Dashboard
                    UpdateDashboard();

                    dgvRentals.ClearSelection();
                    dgvVehicles.ClearSelection();

                    MessageBox.Show(
                        "Rental added successfully!",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
            }
        }

        private void btnEditRental_Click(object sender, EventArgs e)
        {
            // Check if a rental is selected
            if (dgvRentals.CurrentRow == null ||
                dgvRentals.CurrentRow.IsNewRow)
            {
                MessageBox.Show(
                    "Please select a rental to edit.",
                    "No Rental Selected",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow row = dgvRentals.CurrentRow;

            // Get existing rental information
            string customer =
                row.Cells["colCustomer"].Value?.ToString() ?? "";

            string vehicleId =
                row.Cells["colRentalVehicleID"].Value?.ToString() ?? "";

            string vehicleName =
                row.Cells["colRentalVehicle"].Value?.ToString() ?? "";

            DateTime startDate;
            DateTime endDate;

            if (!DateTime.TryParse(
                    row.Cells["colStartDate"].Value?.ToString(),
                    out startDate) ||
                !DateTime.TryParse(
                    row.Cells["colEndDate"].Value?.ToString(),
                    out endDate))
            {
                MessageBox.Show(
                    "The rental dates are invalid.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            // Open Rental Details
            using (frmRental form = new frmRental())
            {
                form.LoadRentalForEdit(
                    customer,
                    vehicleId,
                    vehicleName,
                    startDate,
                    endDate);

                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    // Update rental information
                    row.Cells["colCustomer"].Value =
                        form.CustomerName;

                    row.Cells["colStartDate"].Value =
                        form.StartDate.ToShortDateString();

                    row.Cells["colEndDate"].Value =
                        form.EndDate.ToShortDateString();

                    MessageBox.Show(
                        "Rental updated successfully!",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
        }

        private void btnRentals_Click(object sender, EventArgs e)
        {

            // Hide Dashboard
            pnlDashboard.Visible = false;

            // Show Rentals
            pnlRentals.Visible = true;
            pnlRentals.BringToFront();

            // Highlight Rentals button
            btnRentals.BackColor = Color.FromArgb(30, 101, 200);
            btnRentals.ForeColor = Color.White;

            // Reset other buttons
            btnDashboard.BackColor = Color.FromArgb(231, 238, 248);
            btnDashboard.ForeColor = Color.FromArgb(25, 48, 75);

            btnVehicles.BackColor = Color.FromArgb(231, 238, 248);
            btnVehicles.ForeColor = Color.FromArgb(25, 48, 75);
        }
        private void btnReturnVehicle_Click(object sender, EventArgs e)
        {
            // Check if a rental is selected
            if (dgvRentals.CurrentRow == null ||
                dgvRentals.CurrentRow.IsNewRow)
            {
                MessageBox.Show(
                    "Please select a rental to return.",
                    "No Rental Selected",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow rentalRow = dgvRentals.CurrentRow;

            // Get rental status
            string rentalStatus =
                rentalRow.Cells["colRentalStatus"].Value?
                .ToString()?.Trim() ?? "";

            // Prevent returning the same rental twice
            if (rentalStatus != "Active")
            {
                MessageBox.Show(
                    "This rental is already returned or inactive.",
                    "Information",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            // Get rented vehicle ID
            string vehicleId =
                rentalRow.Cells["colRentalVehicleID"].Value?
                .ToString() ?? "";

            // Find vehicle in the Vehicles table
            DataGridViewRow vehicleRow = null;

            foreach (DataGridViewRow row in dgvVehicles.Rows)
            {
                if (row.IsNewRow)
                    continue;

                string id =
                    row.Cells["colID"].Value?.ToString() ?? "";

                if (id == vehicleId)
                {
                    vehicleRow = row;
                    break;
                }
            }

            if (vehicleRow == null)
            {
                MessageBox.Show(
                    "Vehicle record not found.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            // Confirm vehicle return
            DialogResult result = MessageBox.Show(
                "Are you sure you want to return this vehicle?",
                "Confirm Return",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Update rental status
                rentalRow.Cells["colRentalStatus"].Value =
                    "Returned";

                // Update vehicle status
                vehicleRow.Cells["colStatus"].Value =
                    "Available";

                // Refresh Dashboard
                UpdateDashboard();

                MessageBox.Show(
                    "Vehicle returned successfully!",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Are you sure you want to logout?",
                "Confirm Logout",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

    }
}
