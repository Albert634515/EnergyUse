using System.Reflection;

namespace WinFormsEF.Views
{
    public partial class frmInfo : Form
    {
        #region FormProperties

        #endregion

        #region InitForm

        public frmInfo()
        {
            InitializeComponent();
            setBaseFormSettings();
            setInfo();
            loadSettings();
        }

        #endregion

        #region Events
        private void chkHideInfoFormOnStart_CheckedChanged(object sender, EventArgs e)
        {
            Managers.Settings.SaveSettingCheckBox(chkHideInfoFormOnStart);
        }

        private void cmdLinkLabel_Click(object sender, EventArgs e)
        {
            string url;
            TextBox currentTextBox;

            currentTextBox = (TextBox)sender;
            url = currentTextBox.Tag.ToString();
            if (!string.IsNullOrWhiteSpace(url))
                System.Diagnostics.Process.Start(url);
        }

        #endregion

        #region ButtonEvents

        private void cmdClose_Click(object sender, EventArgs e)
        {
            closeInfoForm();
        }

        #endregion

        #region Methods

        private void closeInfoForm()
        {
            Close();
        }

        private void loadSettings()
        {
            Managers.Settings.LoadSettingCheckBox(chkHideInfoFormOnStart);
        }

        private void setInfo()
        {
            Text = $"Version: {Assembly.GetExecutingAssembly().GetName().Version}";

            Point location;

            location = lblInfo.Location;
            location.Y = lblInfo.Location.Y + lblInfo.Height;

            location = addTitleBox("Icons where used from:", "icons", location);
            location.Y += 5;
            location = addReferenceList(getIconReferefences(), "icons", location);
            location.Y += 10;

            location = addTitleBox("The following plugins where used:", "plugins", location);
            location.Y += 5;
            location = addReferenceList(getPluginReferefences(), "plugin", location);
            location.Y += 10;
        }

        private Point addTitleBox(string titleText, string name, Point startPosition)
        {
            Point location = startPosition;

            TextBox linkLabel = new();
            linkLabel.Name = $"Title{name}";
            linkLabel.Text = titleText;
            linkLabel.Location = location;
            linkLabel.Width = lblInfo.Width;
            linkLabel.Anchor = lblInfo.Anchor;
            linkLabel.BackColor = this.BackColor;
            linkLabel.ReadOnly = true;
            linkLabel.BorderStyle = BorderStyle.None;
            linkLabel.Font = new Font(linkLabel.Font, FontStyle.Bold);

            Controls.Add(linkLabel);
            location.Y += linkLabel.Height;
            this.Height += linkLabel.Height;

            return location;
        }

        private Point addReferenceList(Dictionary<string, string> referenceList, string name, Point startPosition)
        {
            int linkLabelCount = 0;
            Point location = startPosition;

            foreach (KeyValuePair<string, string> iconEntry in referenceList)
            {
                TextBox linkLabel = new TextBox();
                linkLabel.Name = $"{name}{linkLabelCount++}";
                linkLabel.Text = iconEntry.Key;
                linkLabel.Tag = iconEntry.Value;
                linkLabel.Location = location;
                linkLabel.Width = lblInfo.Width;
                linkLabel.Anchor = lblInfo.Anchor;
                linkLabel.BackColor = Color.White;
                linkLabel.ReadOnly = true;
                linkLabel.BorderStyle = BorderStyle.None;
                linkLabel.Click += new System.EventHandler(this.cmdLinkLabel_Click);
                linkLabel.ForeColor = Color.RoyalBlue;
                linkLabel.Font = new Font(linkLabel.Font, FontStyle.Underline);
                linkLabel.Margin = new Padding(5, 5, 5, 5);

                Controls.Add(linkLabel);
                location.Y += linkLabel.Height + 2;
                this.Height += linkLabel.Height;
            }

            return location;
        }
        private Dictionary<string, string> getIconReferefences()
        {
            Dictionary<string, string> iconList = new Dictionary<string, string>();

            iconList.Add("Icons from FlatIcon.com", "https://www.flaticon.com");
            iconList.Add("Icons made by Pixel perfect", "https://www.flaticon.com/packs/basic-ui-30/");

            return iconList;
        }

        private Dictionary<string, string> getPluginReferefences()
        {
            Dictionary<string, string> pluginList = new Dictionary<string, string>();

            pluginList.Add("EpPlus", "https://www.epplussoftware.com/");
            pluginList.Add("iText7", "https://itextpdf.com/");
            pluginList.Add("LiveCharts", "https://lvcharts.com/");            
            pluginList.Add("Microsoft EntityFrameworkCore", "https://docs.microsoft.com/en-us/ef/core/");

            return pluginList;
        }

        private void setBaseFormSettings()
        {
            Managers.Settings.SetBaseFormSettings(this);
        }

        #endregion
    }
}