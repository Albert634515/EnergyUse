using EnergyUse.Core.Import;

namespace WinFormsEF.Views;

internal sealed class ImportPreviewForm : Form
{
    public ImportPreviewForm(ImportPreview preview, bool allowSave)
    {
        Text = "Import preview — all meters";
        Size = new Size(1250, 650);
        MinimumSize = new Size(800, 400);
        StartPosition = FormStartPosition.CenterParent;
        var summary = new Label { Dock = DockStyle.Top, Height = 65, Padding = new Padding(8),
            Text = preview.Summary + "\nFor each conflict, choose a source row or Skip. Stored readings are kept until you save." };
        var buttons = new FlowLayoutPanel { Dock = DockStyle.Bottom, Height = 48, FlowDirection = FlowDirection.RightToLeft };
        var save = new Button { Text = "Save choices…", AutoSize = true, Enabled = allowSave && preview.UnresolvedCount == 0 && preview.SelectedCount > 0 };
        var close = new Button { Text = "Close", AutoSize = true, DialogResult = DialogResult.Cancel };
        buttons.Controls.Add(save);
        buttons.Controls.Add(close);
        CancelButton = close;
        if (!allowSave) save.Visible = false;
        var grid = new DataGridView
        {
            Dock = DockStyle.Fill, AutoGenerateColumns = false, AllowUserToAddRows = false,
            AllowUserToDeleteRows = false, RowHeadersVisible = false
        };
        foreach (var (header, width) in new[] { ("Date", 90), ("Meter", 100), ("Status", 90), ("Source rows", 75),
            ("Stored readings", 240), ("CSV readings", 280), ("Details", 250) })
            grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = header, Width = width, ReadOnly = true });
        grid.Columns.Add(new DataGridViewComboBoxColumn { HeaderText = "Choice (required for conflicts)", Width = 320 });
        grid.Columns[7].DisplayIndex = 3;
        foreach (var row in preview.Rows)
        {
            var index = grid.Rows.Add(row.Date.ToString("dd-MM-yyyy"), row.MeterName, row.StatusText,
                row.SourceRows, row.ExistingValues, row.IncomingValues, row.Reason);
            var gridRow = grid.Rows[index];
            gridRow.Tag = row;
            var choice = (DataGridViewComboBoxCell)gridRow.Cells[7];
            foreach (var option in row.Choices) choice.Items.Add(option.Description);
            choice.Value = row.SelectedChoice?.Description;
            gridRow.DefaultCellStyle.BackColor = row.Status switch
            {
                ImportStatus.Conflict => Color.MistyRose, ImportStatus.New => Color.Honeydew,
                ImportStatus.Changed => Color.LightYellow, _ => Color.White
            };
        }
        grid.CurrentCellDirtyStateChanged += (_, _) =>
        {
            if (grid.IsCurrentCellDirty) grid.CommitEdit(DataGridViewDataErrorContexts.Commit);
        };
        grid.CellValueChanged += (_, e) =>
        {
            if (e.RowIndex < 0 || e.ColumnIndex != 7) return;
            var gridRow = grid.Rows[e.RowIndex];
            var row = (ImportPreviewRow)gridRow.Tag;
            row.SelectedChoice = row.Choices.SingleOrDefault(choice => choice.Description == (string)gridRow.Cells[7].Value);
            summary.Text = preview.Summary + "\nFor each conflict, choose a source row or Skip.";
            save.Enabled = allowSave && preview.UnresolvedCount == 0 && preview.SelectedCount > 0;
        };
        save.Click += (_, _) =>
        {
            if (MessageBox.Show(this, preview.Summary + "\n\nSave these choices? Changed readings will be replaced.",
                "Save import", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                DialogResult = DialogResult.OK;
        };
        Controls.Add(grid);
        Controls.Add(summary);
        Controls.Add(buttons);
    }
}
