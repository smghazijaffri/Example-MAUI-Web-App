﻿using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.Data.SqlClient;

namespace Example.WebComponents.Data
{
    public class Login : Connection
    {
        public bool showLoginPopup = false;
        private SqlCommand? cmd;
        private SqlDataReader? dr;

        public void OpenLoginPopup()
        {
            showLoginPopup = true;
        }

        public void CloseLoginPopup()
        {
            showLoginPopup = false;
        }
        public async Task<bool> Access(string Username, string Password, IJSRuntime JSRuntime, NavigationManager navigationManager)
        {
            try
            {
                using (SqlConnection con = GetSqlConnection())
                {
                    con.Open();
                    using (cmd = new SqlCommand("SELECT * FROM [Users] WHERE Username = @Username AND Password = @Password", con))
                    {
                        cmd.Parameters.AddWithValue("@Username", Username);
                        cmd.Parameters.AddWithValue("@Password", Password);

                        using (dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                await JSRuntime.InvokeVoidAsync("alert", "Login successful!");
                                NavigateToPage(navigationManager);
                                return true;
                            }
                            else
                            {
                                await JSRuntime.InvokeVoidAsync("alert", "Invalid credentials.");
                                return false;
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                await JSRuntime.InvokeVoidAsync("alert", "An error occurred while accessing the database: " + ex.Message);
                return false;
            }
        }

        private void NavigateToPage(NavigationManager navigationManager)
        {
            CloseLoginPopup();
            navigationManager.NavigateTo("/purchaserequisition");
        }
    }
}