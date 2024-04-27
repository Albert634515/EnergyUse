namespace WinFormsEF.Managers
{
    internal class GeneralDialogs
    {
        internal static string GetExportFileName(string chartType, EnergyUse.Models.EnergyType energyType)
        {
            var exportFileName = $"{chartType}_{DateTime.Now:yyyyMMddHHmmss}_{energyType.Name}.xlsx";
            var exportDirectory = string.Empty;

            SaveFileDialog sf = new SaveFileDialog();
            sf.FileName = Path.Combine(exportDirectory, exportFileName);

            if (sf.ShowDialog() == DialogResult.OK)
            {
                // Now here's our save folder
                exportDirectory = Path.GetDirectoryName(sf.FileName);
                exportFileName = Path.GetFileName(sf.FileName);
            }

            if (string.IsNullOrWhiteSpace(exportDirectory))
                return string.Empty;

            if (!Directory.Exists(Path.GetDirectoryName(exportDirectory)))
                Directory.CreateDirectory(Path.GetDirectoryName(exportDirectory));

            System.IO.FileInfo f = new(Path.Combine(exportDirectory, exportFileName));
            if (f.Exists)
            {
                var message = Languages.GetResourceString("FileExistsOverWrite", "File already exists, do you want overwrite?");
                DialogResult dialogResult = MessageBox.Show(message, Languages.GetResourceString("FileAlreadyExists", "File already exists"), MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                    f.Delete();
                else
                    return string.Empty;
            }

            return Path.Combine(exportDirectory,exportFileName);
        }

        internal static bool WarningUnsavedChanges(IWin32Window owner)
        {
            var message = Languages.GetResourceString("UnsavedChanges", "There are unsaved changes are you sure you want to close this form?");
            var message2 = Languages.GetResourceString("UnsavedChangesTitle", "Unsaved changes");
            if (MessageBox.Show(owner, message, message2, MessageBoxButtons.YesNo) == DialogResult.No)
                return true;
            else
                return false;
        }

        internal static bool WarningUnsavedChangesIn(IWin32Window owner, string unsavedIn)
        {
            var message = Languages.GetResourceString("UnsavedChangesIn", $"There are unsaved changes in {unsavedIn} are you sure you want to close this form??");
            var message2 = Languages.GetResourceString("UnsavedChangesTitle", "Unsaved changes");
            if (MessageBox.Show(owner, message, message2, MessageBoxButtons.YesNo) == DialogResult.No)
                return true;
            else
                return false;
        }
    }
}
