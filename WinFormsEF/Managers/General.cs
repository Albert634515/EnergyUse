namespace WinFormsEF.Managers
{
    internal class General
    {
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
