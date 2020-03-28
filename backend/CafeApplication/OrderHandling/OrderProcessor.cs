﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderHandling {
    public class OrderProcessor {
        
        public static void Main(string[] args) {
            OrderProcessor o = new OrderProcessor();

            //Test example
            string user_id = "55554444";

            // Dictionary with item_id and quantity.
            Dictionary<int, int> items = new Dictionary<int, int>();

            items.Add(4, 1);
            items.Add(15, 3);






            //o.ProcessOrder(user_id, items);
            
        }

       /*
        * This method will take in the user_id of the User, and a dictionary of item_id as key and quantity 
        * as value. It will then create the appropriate Orders and use the order_id of that entry to create 
        * the data in the Order_Items table.
        */
        //public bool ProcessOrder(string user_id, Dictionary<int, int> items) {
        public bool ProcessOrder(Order o) {
            string user_id = o.userID.ToString();
            
            //Computer the total and insert it into the database
            double orderTotal = computeTotal(o.getItems(), .102);
            if (hasFunds(orderTotal, user_id)) {
                DBAccess.insertNewOrder(o.orderID, user_id, orderTotal, o.getDate());

                //Get the ID of the user's order they just placed
                //var table = DBAccess.getUserLatestOrder(user_id);
                //string orderID = table.Rows[0].ItemArray[0].ToString();

                //Insert each item in the order into the database
                foreach (KeyValuePair<int, string[]> entry in o.getItems()) {
                    DBAccess.insertOrderWithItem(o.orderID, entry.Key, Int32.Parse(entry.Value[1]));
                }
                return true;
            }
            else {
                Console.WriteLine("Could not complete order");
                Console.Read();
                return false;
            }

        }

        private double computeTotal(Dictionary<int, string[]> items, double taxRate) {
            double total = 0;

            // Calculating total:
            for (int i = 0; i < items.Count; i++) {
                string itemId = items.ElementAt(i).Key.ToString();
                int itemQuantity = Int32.Parse(items.ElementAt(i).Value[1]);
                total += getItemPrice(itemId) * itemQuantity;
            }
            total += total * taxRate;
            return Math.Round(total, 2);
        }

        private double getItemPrice(string itemID) {
            var dataTable = DBAccess.getItemPrice(itemID);

            try{
                if (dataTable.Rows.Count != 1)
                    return -1;
            

                return double.Parse(dataTable.Rows[0].ItemArray[0].ToString());
            }
            catch (Exception e) {
                Console.WriteLine("Database retrieval error" + e);
            }
            return -1;
        }

        private bool hasFunds(double orderTotal, string userID) {
            try {
                var table = DBAccess.getUserBalance(userID);
                double userBalance = double.Parse(table.Rows[0].ItemArray[0].ToString());

                if (orderTotal > userBalance)
                    return false;


                double newBalance = userBalance - orderTotal;
                DBAccess.updateBalance(userID, newBalance);
                return true;
            }catch(Exception e) {
                Console.WriteLine("Database retrieval error" + e);
            }
            return false;
        }
    }
}
