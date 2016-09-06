using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AzureBlog
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            BindComments();
        }

        protected async void btnCreate_Click(object sender, EventArgs e)
        {
           await DocumentDb.CreateDocument(txtComment.Text);
           BindComments();
            Clear();
        }

        private  void BindComments()
        {
            var comments =   DocumentDb.Initialize();
            commentsRepeater.DataSource = comments;
            commentsRepeater.DataBind();
        }

        private void Clear()
        {
            txtComment.Text = string.Empty;
        }
    }
}