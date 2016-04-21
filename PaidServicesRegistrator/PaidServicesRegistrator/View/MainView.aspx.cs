using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.SqlServer.Server;
using PaidServicesRegistrator.Utils;
using PaidServicesRegistrator.Utils.ClientRegister;
using PaidServicesRegistrator.Utils.ServiceRegister;
using PaidServicesRegistrator.WcfService;

namespace PaidServicesRegistrator.View
{
    public partial class MainView : Page
    {
        private static ClientDAO clientDao;
        private static ServiceDAO serviceDao;

        protected void Page_Load(object sender, EventArgs e)
        {
            clientDao = new ClientDAO();
            serviceDao = new ServiceDAO();

            //====testing=====
            //var p = new WcfService.PaidServicesRegistrator();//new PaidService();
            //var r = p.CheckUserRegistration("Z7jWh7m/XNFUdq6A8GkW4WWeoJhIwymiBSdLNPHdOJU=", "DNrYkl9CLUjUzCPwNcU1QBHCjK6bL3+UzDD8uii8D8M=");

            if (!Page.IsPostBack)
            {
                TokenTypeDropDownList.DataSource = new[]
                {
                    TokenUtil.TokenType.OneOff.ToString(),
                    TokenUtil.TokenType.ThirtyDays.ToString()
                };

                ServiceNameDropDownList.DataSource = serviceDao.GetServiceNames();
                TokenTypeDropDownList.DataBind();
                ServiceNameDropDownList.DataBind();
            }
        }

        protected void OnGetTokenButtonClick(object sender, EventArgs e)
        {
            TokenUtil.TokenType tokenType;

            if (!Enum.TryParse(TokenTypeDropDownList.SelectedValue, out tokenType)) 
                return;

            var token = clientDao.RegisterClient(ServiceNameDropDownList.SelectedValue, tokenType);
            TokenValueLabel.Text = token;
            TokenDiv.Style["display"] = "block";
        }
    }
}