using System;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private bool _searching = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            webBrowser1.ScriptErrorsSuppressed = true;
            webBrowser1.Navigate("https://www.ebay.com");
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                MessageBox.Show("Please enter a search term.");
                return;
            }

            _searching = true;
            // Navigate directly with search query in URL
            string searchUrl = "https://www.ebay.com/sch/i.html?_nkw="
                               + Uri.EscapeDataString(txtSearch.Text);
            webBrowser1.Navigate(searchUrl);
        }

        private void webBrowser1_DocumentCompleted(object sender,
            WebBrowserDocumentCompletedEventArgs e)
        {
            if (_searching && e.Url == webBrowser1.Url)
            {
                _searching = false;

                string url = webBrowser1.Url?.ToString();

                if (!string.IsNullOrEmpty(url))
                {
                    // Set link to search result field
                    txtLink.Text = url;

                    // Add to search history
                    listHistory.AppendText(url + "\n");
                }
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (webBrowser1.CanGoBack)
            {
                webBrowser1.GoBack();
            }
            txtSearch.Text = "";
            txtLink.Text = "";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}