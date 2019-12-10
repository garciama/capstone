﻿using System;
using System.Diagnostics;
using MySql.Data.MySqlClient;
using System.Data;
using Database;

public static class DBAccess {

    private static string connString = DBCredentials.GetCredentials();

    private static MySqlConnection connection = new MySqlConnection(connString);

    //All items for main menu
    public static DataTable getAllItems() {
        string sql = "SELECT * FROM Item";
        return issueQuery(sql);
    }

    //Only drinks filter
    public static DataTable getAllDrinks() {
        string sql = "SELECT * FROM Item WHERE is_drink = true";
        return issueQuery(sql);
    }

    public static DataTable getItemPrice(string itemID) {
        string sql = "SELECT price FROM Item WHERE item_id = @itemID";
        try {
            connection.Open();
            using (MySqlCommand command = new MySqlCommand(sql, connection)) {
                command.CommandTimeout = 1000;
                command.Parameters.AddWithValue("@itemID", itemID);
                var dataReader = command.ExecuteReader();
                var dataTable = new DataTable();
                dataTable.Load(dataReader);
                connection.Close();
                return dataTable;
            }
        }
        catch (Exception e) {
            Debug.WriteLine("Error in item price query: " + e.Message);
            return null;
        }
    }

    public static void insertNewOrder(string userID, double total) {
        string sql = "INSERT INTO Orders(user_id, total) " +
                                 "VALUES(@userID, @total)";
        try {
            connection.Open();
            using (MySqlCommand command = new MySqlCommand(sql, connection)) {
                command.CommandTimeout = 1000;
                command.Parameters.AddWithValue("@userID", userID);
                command.Parameters.AddWithValue("@total", total);

                var dataReader = command.ExecuteReader();
                connection.Close();
            }
        }
        catch (Exception e) {
            Debug.WriteLine("Error database inserting order: " + e.Message);
        }
    }

    public static void insertOrderWithItem(string orderID, int itemID, int itemQuantity) {
            string sql = "INSERT INTO Order_Items(order_id, item_id, quantity) " +
                         "VALUES(@orderID, @itemID, @itemQuantity)";

        try {
            connection.Open();
            using (MySqlCommand command = new MySqlCommand(sql, connection)) {
                command.CommandTimeout = 1000;
                command.Parameters.AddWithValue("@orderID", orderID);
                command.Parameters.AddWithValue("@itemID", itemID);
                command.Parameters.AddWithValue("@itemQuantity", itemQuantity);

                var dataReader = command.ExecuteReader();
                connection.Close();
            }
        }
        catch (Exception e) {
            Debug.WriteLine("Error in database query: " + e.Message);
        }
   
    }

    public static DataTable getUserLatestOrder(string userID) {
        string sql = "SELECT order_id " +
                         "FROM Orders " +
                         "WHERE user_id = @userID " +
                         "ORDER BY order_id DESC LIMIT 1";
        try {
            connection.Open();
            using (MySqlCommand command = new MySqlCommand(sql, connection)) {
                command.CommandTimeout = 1000;
                command.Parameters.AddWithValue("@userID", userID);
                var dataReader = command.ExecuteReader();
                var dataTable = new DataTable();
                dataTable.Load(dataReader);
                connection.Close();
                return dataTable;
            }
        }
        catch (Exception e) {
            Debug.WriteLine("Error in latest order query: " + e.Message);
            return null;
        }
    }

    public static DataTable getUserBalance(string userID) {
        string sql = "SELECT balance FROM User WHERE user_id = @userID";

        try {
            connection.Open();
            using (MySqlCommand command = new MySqlCommand(sql, connection)) {
                command.CommandTimeout = 1000;
                command.Parameters.AddWithValue("@userID", userID);
                var dataReader = command.ExecuteReader();
                var dataTable = new DataTable();
                dataTable.Load(dataReader);
                connection.Close();
                return dataTable;
            }
        }
        catch (Exception e) {
            Debug.WriteLine("Error in database user balance query: " + e.Message);
            return null;
        }
    }

    public static void updateBalance(string userID, double newBalance) {
        string sql = "UPDATE User " + 
                     "SET balance = @newBalance " + 
                     "WHERE user_id = @userID";
        try {
            connection.Open();
            using (MySqlCommand command = new MySqlCommand(sql, connection)) {
                command.CommandTimeout = 10000;
                command.Parameters.AddWithValue("@newBalance", newBalance);
                command.Parameters.AddWithValue("@userID", userID);
                command.ExecuteReader();
                connection.Close();
            }
        }
        catch (Exception e) {
            Debug.WriteLine("Error in database query: " + e.Message);

        }
    }

    private static DataTable issueQuery(string sql) {
        try {
            connection.Open();
            using (MySqlCommand command = new MySqlCommand(sql, connection)) {
                command.CommandTimeout = 1000;
                var dataReader = command.ExecuteReader();
                var dataTable = new DataTable();
                dataTable.Load(dataReader);
                connection.Close();
                return dataTable;
            }
        }
        catch (Exception e) {
            Debug.WriteLine("Error in database query: " + e.Message);
            return null;
        }
    }

}
