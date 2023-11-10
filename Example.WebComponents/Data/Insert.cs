using Dapper;
using Microsoft.Data.SqlClient;
using System.Numerics;
using static Example.WebComponents.Pages.PRCreate;

namespace Example.WebComponents.Data
{
    public class Insert : Connection
    {
        string Customer_Name { get; set; }
        string Customer_Contact { get; set; }
        string Customer_Id { get; set; }
        public void insert(int prnum, string ven, string item, int quan, DateTime reqdat)
        {
            using (SqlConnection con = GetSqlConnection())
            {
                con.Open();
                SqlCommand cmd;
                string formattedDate = reqdat.ToString("yyyy-MM-dd");

                string sqlQuery = $"INSERT INTO PR1 (PRnumber, Vendor, Item, Quantity, Reqdate) VALUES ('{prnum}', '{ven}', '{item}', '{quan}', '{formattedDate}')";
                cmd = new SqlCommand(sqlQuery, con);
                cmd.ExecuteNonQuery();
                con.Close();

            }
        }
        public async Task InsertIntoCustomBuilt(string id, string name, string contact)
        {
            using (SqlConnection con = GetSqlConnection())
            {
                Customer_Name = name;
                Customer_Contact = contact;
                Customer_Id = id;
                con.Open();

                // Check if the record already exists
                string checkQuery = "SELECT COUNT(*) FROM Custom_Built WHERE Id = @Id";
                int existingRecordsCount = await con.ExecuteScalarAsync<int>(checkQuery, new { Id = id });

                string formattedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                if (existingRecordsCount > 0)
                {
                    // Record exists, update the Name and Contact
                    string updateQuery = "UPDATE Custom_Built SET Name = @Name, Contact = @Contact, Date = @Date WHERE Id = @Id";
                    await con.ExecuteAsync(updateQuery, new { Id = id, Name = name, Contact = contact, Date = formattedDate });
                }
                else
                {
                    // Record does not exist, insert the data
                    string insertQuery = "INSERT INTO Custom_Built (Id, Name, Contact, Date) VALUES (@Id, @Name, @Contact, @Date)";
                    await con.ExecuteAsync(insertQuery, new { Id = id, Name = name, Contact = contact, Date = formattedDate });
                }
            }
        }

        public async Task InsertIntoCustomBuilt(string combinedValue)
        {
            using (SqlConnection con = GetSqlConnection())
            {
                con.Open();

                // First, check if a record with the given 'name' exists
                string checkQuery = "SELECT COUNT(*) FROM Custom_Built WHERE (Name = @Name AND Contact = @Contact AND Id = @Id)";
                int count = await con.ExecuteScalarAsync<int>(checkQuery, new { Motherboard = combinedValue, Name = Customer_Name, Contact = Customer_Contact, Id = Customer_Id });

                if (count > 0)
                {
                    // If a record with the given 'name' exists, update it
                    string updateQuery = "UPDATE Custom_Built SET Motherboard = @Motherboard WHERE (Name = @Name AND Contact = @Contact AND Id = @Id)";
                    await con.ExecuteAsync(updateQuery, new { Motherboard = combinedValue, Name = Customer_Name, Contact = Customer_Contact, Id = Customer_Id });
                }
                else
                {
                    // If a record with the given 'name' doesn't exist, insert a new record
                    string insertQuery = "INSERT INTO Custom_Built (Motherboard) VALUES (@Motherboard)";
                    await con.ExecuteAsync(insertQuery, new { Motherboard = combinedValue });
                }
            }

        }

    }
}
