using Shop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Shop
{
	public partial class Orders : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			ViewOrders();
		}

		private void ViewOrders()
		{
			LinkedList<Ordder> orders;
			// if not logged in cant view orders
			if (Session["login"] == null)
				Response.Redirect("/Accounts/Login.aspx");
			if (Session["type"].Equals("admin"))
			{
				//orders = ConnectionClass.GetAllOrders();
				grdOrders.Visible = true;
			}
			else
			{
				int id = Convert.ToInt32(Session["id"].ToString());		// user id
				orders = ConnectionClass.GetAllOrdersForClient(id);
				string str = "";
				foreach (Ordder order in orders)
				{
					Item item = ConnectionClass.GetProductByID(order.ProductID);
					str += "<table caption='Order ID: "+order.Id+"' class='ordersTable'>" +
						"<tr><td><b>Product:</b></td><td>" + item.Category + " " + item.Type + " " + item.Name + "</td></tr>" +
						"<tr><td><b>Price:</b></td><td>" + order.Price + "</td></tr>" +
						"<tr><td><b>Date:</b></td><td>" + order.Date + "</td></tr>" +
						"<tr><td><b>Address:</b></td><td>" + order.Info + "</td></tr>" +
						"<table>";
				}
				lblOut.Text = str;
				lblOut.Visible = true;
			}
			
		}



	}
}