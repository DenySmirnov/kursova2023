using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Shop
{
	public partial class Register : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			
		}

		protected void cmdRegister_Click(object sender, EventArgs e)
		{
			if (txtName.Text == "" || txtPass.Text == "" || txtConfPass.Text == "" || txtEmail.Text == "" || txtCreditCard.Text == "")
			{
				return;
			}
			string pass = txtPass.Text.Trim();
			string name = txtName.Text.Trim();
			string email = txtEmail.Text.Trim();
			string credit = txtCreditCard.Text.Trim();
			if (pass.Length < 6)
			{
				lblErr.Text = "пароль має містити мінімум 6 символів.";
				return;
			}
			if(IsUserExist(name))
			{
				lblErr.Text = "Корстувач з таким іменем вже існує. Спробуйте інше.";
				return;
			}
			if (credit.Length < 16)
			{
				lblErr.Text = "Номер кредитної карти має складатися з 16 символів.";
				return;
			}
			if (!IsEmail(email))
			{
				lblErr.Text = "Будь-ласка введіть правильну пошту.";
				return;
			}
			User usr = new User(1, name, pass, email, "user", txtInfo.Text, credit);
			AddUser(usr);
		}

		private void AddUser(User usr)
		{
			if (ConnectionClass.AddUser(usr))
			{
				Session["login"] = usr.Name;
				Session["type"] = usr.Type;
				User tempUsr = ConnectionClass.GetUser(usr.Name, usr.Password);
				Session["id"] = tempUsr.Id;
				lblErr.Text = "";
				Response.Redirect("~/Default.aspx");
			}
			else
			{
				lblErr.Text = "Failed to add user. Try to enter different name.";
			}
		}

		private int GetCreditNumber(string str)
		{
			str = str.Trim();
			int ans = 0;
			if (!int.TryParse(str, out ans))
			{
				return -1;
			}
			return ans;
		}

		private bool IsUserExist(string usr)
		{
			LinkedList<User> users = ConnectionClass.GetAllUsers();
			foreach (User user in users)
			{
				if (user.Name.Equals(usr))
				{
					return true;
				}
			}
			return false;
		}

		private bool IsEmail(string email)
		{
			try
			{
				MailAddress m = new MailAddress(email);
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

	}
}