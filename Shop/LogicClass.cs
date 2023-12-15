using Shop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop
{
	public static class LogicClass
	{

		// order the product for user
		// function that takes money from client doesn't exist for yet
		// redirect to client orders page
		public static string BuyItem(int ProductID, int userID, string addres)
		{
			// place for fields validation
			Item item = ConnectionClass.GetProductByID(ProductID);
			if (item == null || item.Quantity <= 0)
			{
				return "Cannot buy this product. Please try other one.";
			}
			User user = ConnectionClass.GetUserByID(userID);
			Ordder order = new Ordder(1, user.Id, ProductID, 1, item.Price, DateTime.Now.ToString("d.M.yyyy"), addres);
			ConnectionClass.AddOrder(order);
			ConnectionClass.ReduceQuantity(ProductID);
			// function that draws money from client
			//Response.Redirect("/Orders.aspx");
			return "";
		}

		public static string GetDesktopProducts(LinkedList<Item> des)
		{
			string sb = "";
			foreach (Item d in des)
			{
				sb += @"<table class='desktopTable'>" +
				"<tr><th rowspan='3' width='150px'><a href='/Buy.aspx?id=" + d.ID + "'><img runat='server' src='" + d.Image + "' /></a></th>" +
				"<th width='50px'>Name: </td><td>" + d.Type + " " + d.Name + "</td>" +
				"<th rowspan='3' width='50px'>" +
				"<button type='button' onclick='redirect(" + d.ID + ")' name='buy' class='css3button'>buy</button>" +
				"</th></tr><tr><th>Price: </th><td>" + d.Price + " $</td></tr>" +
				"<tr><th>Description: </th><td>" + d.Description + "</td></tr></table>";
			}
			return sb;
		}

		public static string GetNotebookProducts(LinkedList<Item> list)
		{
			string sb = "";
			foreach (Item d in list)
			{
				sb += @"<table class='desktopTable'>" +
				"<tr><th rowspan='3' width='150px'><a href='/Buy.aspx?id=" + d.ID + "'><img runat='server' src='" + d.Image + "' /></a></th>" +
				"<th width='50px'>Name: </td><td>" + d.Type + " " + d.Name + "</td>" +
				"<th rowspan='3' width='50px'>" +
				"<button type='button' name='buy' onclick='redirect(" + d.ID + ")' value='" + d.ID + "' class='css3button'>buy</button>" +
				"</th></tr><tr><th>Price: </th><td>" + d.Price + " $</td></tr>" +
				"<tr><th>Description: </th><td>" + d.Description + "</td></tr></table>";
			}
			return sb;
		}

		public static string GetTabletProducts(LinkedList<Item> list)
		{
			string sb = "";
			foreach (Item d in list)
			{
				sb += @"<table class='desktopTable'>" +
				"<tr><th rowspan='3' width='150px'><a href='/Buy.aspx?id=" + d.ID + "'><img runat='server' src='" + d.Image + "' /></a></th>" +
				"<th width='50px'>Name: </td><td>" + d.Type + " " + d.Name + "</td>" +
				"<th rowspan='3' width='50px'>" +
				"<button type='button' onclick='redirect(" + d.ID + ")' name='buy' value='" + d.ID + "' class='css3button'>buy</button>" +
				"</th></tr><tr><th>Price: </th><td>" + d.Price + " $</td></tr>" +
				"<tr><th>Description: </th><td>" + d.Description + "</td></tr></table>";
			}
			return sb;
		}

		public static string ViewInfoBeforeBuyProduct(int ProductID)
		{
			Item item = ConnectionClass.GetProductByID(ProductID);
			if (item == null)
			{
				return "Sorry this product doesnt exist.";
				//Response.Redirect("/Default.aspx");
			}
			string[] des = item.Description.Split(',');
			for (int i = 0; i < des.Length; i++)
			{
				des[i] = des[i].Substring(0, des[i].LastIndexOf(' '));
			}
			string outStr = "";
			if (item.Category.Equals("Desktop"))
			{
				outStr += "<table class='buyTable'>" +
						"<tr><td rowspan='9'><img runat='server' src='" + item.Image + "' /></td>" +
						"<td colspan='2' style='text-align:center'><b>" + item.Category + " " + item.Type + " " + item.Name + "</b></td></tr>" +
						"<tr bgcolor='#eeeeee'><td>Proccessor</td><td>" + des[0] + "</td></tr>" +
						"<tr><td>Motherboard</td><td>" + des[1] + "</td></tr>" +
						"<tr bgcolor='#eeeeee'><td>Video</td><td>NVIDIA GeForce GTX650 1GB</td></tr>" +
						"<tr><td>RAM</td><td>" + des[2] + "</td></tr>" +
						"<tr bgcolor='#eeeeee'><td>Memory</td><td>" + des[3] + "</td></tr>" +
						"<tr><td>PSU</td><td>Seasonic 620W</td></tr>" +
						"<tr bgcolor='#eeeeee'><td>Disc Drive</td><td>LG SATA 22X SUPER-MULTI DVD Burner</td></tr>" +
						"<tr><td>Warranty</td><td>15 Years</td></tr>" +
					"</table>";
			}
			if (item.Category.Equals("Notebook"))
			{
				outStr += "<table class='buyTable' style='width: 80%;'>" +
						"<tr><td rowspan='9'><img runat='server' src='" + item.Image + "' /></td>" +
						"<td colspan='2' style='text-align:center'><b>" + item.Category + " " + item.Type + " " + item.Name + "</b></td></tr>" +
						"<tr bgcolor='#eeeeee'><td>Proccessor</td><td>" + des[0] + "</td></tr>" +
						"<tr><td>Video</td><td>Intel® HD Graphics</td></tr>" +
						"<tr bgcolor='#eeeeee'><td>RAM</td><td>" + des[1] + "</td></tr>" +
						"<tr><td>Memory</td><td>" + des[2] + "</td></tr>" +
						"<tr bgcolor='#eeeeee'><td>Disc Drive</td><td>LG SATA 22X SUPER-MULTI DVD Burner</td></tr>" +
						"<tr><td>Varranty</td><td>15 Years</td></tr>" +
					"</table>";
			}
			if (item.Category.Equals("Tablet"))
			{
				outStr += "<table class='buyTable' style='width: 80%;'>" +
						"<tr><td rowspan='9'><img runat='server' src='" + item.Image + "' /></td>" +
						"<td colspan='2' style='text-align:center'><b>" + item.Category + " " + item.Type + " " + item.Name + "</b></td></tr>" +
						"<tr colspan='2'><td>Proccessor</td><td>" + item.Description + "</td></tr>" +
					"</table>";
			}

			return outStr;
		}

		public static string AddProduct(string category, string selectedImage, string txtType, string txtPrice,
													string txtName, string txtDescription, string txtQuant)
		{
			if (selectedImage == null || txtType == "" || txtPrice == "" || txtName == "" ||
											txtDescription == "" || txtQuant == "")
			{
				return "One of fields is empty";
			}
			string name = txtName;
			string des = txtDescription;
			//int price = ConvertToNumber(txtPrice.Text);
			int price = LogicClass.ConvertToNumber(txtPrice);
			if (price <= 0)
			{
				return "Price can't be negative or zero";
				
			}
			string date = DateTime.Now.ToString("d.M.yyyy");
			string type = txtType;
			string img = "Images/" + selectedImage;
			int quant = ConvertToNumber(txtQuant);
			if (quant < 0)
			{
				return "Quantity must be zero or more";

			}
			//string category = drpProductType.SelectedValue.ToString();
			Item pr = new Item(1, category, name, quant, img, des, date, price, type);
			ConnectionClass.AddProduct(pr);
			//lblResult.Text = "";
			return "Product was successfully added!";
		}

		// convert string to int
		// if string contains symbols that not numbers return -1
		public static int ConvertToNumber(string str)
		{
			str = str.Trim();
			int ans = 0;
			if (!int.TryParse(str, out ans))
			{
				return -1;
			}
			return ans;
		}


	}
}