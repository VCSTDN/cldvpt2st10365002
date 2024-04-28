﻿namespace CloudDevPOE.ViewModels
{
	public class ProductSummary
	{
		public int ProductID { get; set; }
		public string ProductName { get; set; }
		public decimal ProductPrice { get; set; }
		public bool ProductAvailability { get; set; }
		public string ProductMainImageUrl { get; set; }
	}
}