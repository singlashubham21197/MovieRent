using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace MovieRent
{
    class DatabaseManager
    {
        //Change connection to Your Own Connection

        SqlConnection Sqlconn = new SqlConnection(@"Data Source=LAPTOP-RAKIOMBV\SQLEXPRESS;Initial Catalog=VideoRental;Integrated Security=True");
        SqlCommand SqlStr = new SqlCommand();
        SqlDataReader SqlReader;
        string Sqlstmt;

        public DataTable ListCustomer(string Customers)
        {
            DataTable dt = new DataTable();
            SqlDataReader SqlReader;
            try
            {
                SqlStr.Connection = Sqlconn;
                Sqlstmt = "Select * from Customer where FirstName like '%' + @FirstName + '%'";

                using (SqlStr = new SqlCommand(Sqlstmt, Sqlconn))
                {
                    SqlStr.Parameters.AddWithValue("@FirstName", Customers);
                    Sqlconn.Open();
                    SqlReader = SqlStr.ExecuteReader();

                    if (SqlReader.HasRows)
                    {
                        dt.Load(SqlReader);
                    }
                    Sqlconn.Close();
                    return dt;
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Database Exeption" + ex.Message);
                Sqlconn.Close();
                return null;
            }

        }
        public DataTable ListMovies(string Movies)
        {
            DataTable dt = new DataTable();
            SqlDataReader SqlReader;
            try
            {
                SqlStr.Connection = Sqlconn;
                Sqlstmt = "Select * from Movies where Title like '%' + @Title + '%'";

                using (SqlStr = new SqlCommand(Sqlstmt, Sqlconn))
                {
                    SqlStr.Parameters.AddWithValue("@Title", Movies);
                    Sqlconn.Open();
                    SqlReader = SqlStr.ExecuteReader();

                    if (SqlReader.HasRows)
                    {
                        dt.Load(SqlReader);
                    }
                    Sqlconn.Close();
                    return dt;
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Database Exeption" + ex.Message);
                Sqlconn.Close();
                return null;
            }
        }
        public DataTable ListRentals(string Rentals)
        {
            DataTable dt = new DataTable();
            SqlDataReader SqlReader;
            try
            {
                SqlStr.Connection = Sqlconn;
                Sqlstmt = "Select R.RMID,C.FirstName,C.LastName,C.Address,M.Title,M.Rental_Cost,M.Copies,R.DateRented,R.DateReturned from RentedMovies R inner join Movies M on R.MovieIDFK = M.MovieID inner join Customer C on R.CustIDFK = c.CustID";

                using (SqlStr = new SqlCommand(Sqlstmt, Sqlconn))
                {
                    SqlStr.Parameters.AddWithValue("@RMID", Rentals);
                    Sqlconn.Open();
                    SqlReader = SqlStr.ExecuteReader();

                    if (SqlReader.HasRows)
                    {
                        dt.Load(SqlReader);
                    }
                    Sqlconn.Close();
                    return dt;
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Database Exeption" + ex.Message);
                Sqlconn.Close();
                return null;
            }

        }
        public DataTable ListOutRentals(string Rentals)
        {
            DataTable dt = new DataTable();
            SqlDataReader SqlReader;
            try
            {
                SqlStr.Connection = Sqlconn;
                Sqlstmt = "Select R.RMID,C.FirstName,C.LastName,C.Address,M.Title,M.Rental_Cost,M.Copies,R.DateRented,R.DateReturned from RentedMovies R inner join Movies M on R.MovieIDFK = M.MovieID inner join Customer C on R.CustIDFK = c.CustID where DateReturned is null";

                using (SqlStr = new SqlCommand(Sqlstmt, Sqlconn))
                {
                    SqlStr.Parameters.AddWithValue("@RMID", Rentals);
                    Sqlconn.Open();
                    SqlReader = SqlStr.ExecuteReader();

                    if (SqlReader.HasRows)
                    {
                        dt.Load(SqlReader);
                    }
                    Sqlconn.Close();
                    return dt;
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Database Exeption" + ex.Message);
                Sqlconn.Close();
                return null;
            }

        }

        public DataTable ListCustomerShort(string CustomersShort)
        {
            DataTable dt = new DataTable();
            SqlDataReader SqlReader;
            try
            {
                SqlStr.Connection = Sqlconn;
                Sqlstmt = "Select CustID,FirstName,LastName,Address from Customer where FirstName like '%' + @FirstName + '%'";

                using (SqlStr = new SqlCommand(Sqlstmt, Sqlconn))
                {
                    SqlStr.Parameters.AddWithValue("@FirstName", CustomersShort);
                    Sqlconn.Open();
                    SqlReader = SqlStr.ExecuteReader();

                    if (SqlReader.HasRows)
                    {
                        dt.Load(SqlReader);
                    }
                    Sqlconn.Close();
                    return dt;
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Database Exeption" + ex.Message);
                Sqlconn.Close();
                return null;
            }

        }
        public DataTable ListMoviesShort(string MoviesShort)
        {
            DataTable dt = new DataTable();
            SqlDataReader SqlReader;
            try
            {
                SqlStr.Connection = Sqlconn;
                Sqlstmt = "Select MovieID,Title,Rental_Cost,Copies from Movies where Title like '%' + @Title + '%'";

                using (SqlStr = new SqlCommand(Sqlstmt, Sqlconn))
                {
                    SqlStr.Parameters.AddWithValue("@Title", MoviesShort);
                    Sqlconn.Open();
                    SqlReader = SqlStr.ExecuteReader();

                    if (SqlReader.HasRows)
                    {
                        dt.Load(SqlReader);
                    }
                    Sqlconn.Close();
                    return dt;
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Database Exeption" + ex.Message);
                Sqlconn.Close();
                return null;
            }
        }
        public DataTable ListStatsMostRented(string MostRented)
        {
            DataTable dt = new DataTable();
            SqlDataReader SqlReader;
            try
            {
                SqlStr.Connection = Sqlconn;
                Sqlstmt = "Select Count(R.MovieIDFK) as NumOfTimesMovieRented,M.Title from RentedMovies R inner join Movies M on R.MovieIDFK = M.MovieID where MovieID = MovieIDFK group by Title order by NumOfTimesMovieRented desc";

                using (SqlStr = new SqlCommand(Sqlstmt, Sqlconn))
                {
                    Sqlconn.Open();
                    SqlReader = SqlStr.ExecuteReader();

                    if (SqlReader.HasRows)
                    {
                        dt.Load(SqlReader);
                    }
                    Sqlconn.Close();
                    return dt;
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Database Exeption" + ex.Message);
                Sqlconn.Close();
                return null;
            }
        }
        public DataTable ListStatsRentedTheMost(string RentedMost)
        {
            DataTable dt = new DataTable();
            SqlDataReader SqlReader;
            try
            {
                SqlStr.Connection = Sqlconn;
                Sqlstmt = "Select  Count(R.CustIDFK) as NumOfTimesCustomerRented,C.FirstName,C.LastName from RentedMovies R inner join Movies M on R.MovieIDFK = M.MovieID inner join Customer C on R.CustIDFK = c.CustID where CustID =CustIDFK group by FirstName,LastName order by NumOfTimesCustomerRented desc";

                using (SqlStr = new SqlCommand(Sqlstmt, Sqlconn))
                {
                    Sqlconn.Open();
                    SqlReader = SqlStr.ExecuteReader();

                    if (SqlReader.HasRows)
                    {
                        dt.Load(SqlReader);
                    }
                    Sqlconn.Close();
                    return dt;
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Database Exeption" + ex.Message);
                Sqlconn.Close();
                return null;
            }
        }
        public DataTable ListStatsNumOfMovies(string NumOfMovies)
        {
            DataTable dt = new DataTable();
            SqlDataReader SqlReader;
            try
            {
                SqlStr.Connection = Sqlconn;
                Sqlstmt = "select count(MovieID) as NumberOfMovies from Movies ";

                using (SqlStr = new SqlCommand(Sqlstmt, Sqlconn))
                {
                    Sqlconn.Open();
                    SqlReader = SqlStr.ExecuteReader();

                    if (SqlReader.HasRows)
                    {
                        dt.Load(SqlReader);
                    }
                    Sqlconn.Close();
                    return dt;
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Database Exeption" + ex.Message);
                Sqlconn.Close();
                return null;
            }
        }
        public DataTable ListStatsNumOfAccounts(string NumOfAccounts)
        {
            DataTable dt = new DataTable();
            SqlDataReader SqlReader;
            try
            {
                SqlStr.Connection = Sqlconn;
                Sqlstmt = "select count(CustID) as NumberOfAccounts from Customer";

                using (SqlStr = new SqlCommand(Sqlstmt, Sqlconn))
                {
                    Sqlconn.Open();
                    SqlReader = SqlStr.ExecuteReader();

                    if (SqlReader.HasRows)
                    {
                        dt.Load(SqlReader);
                    }
                    Sqlconn.Close();
                    return dt;
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Database Exeption" + ex.Message);
                Sqlconn.Close();
                return null;
            }
        }

        public bool AddCustomer(string FirstName, string LastName, string Address, string Phone)
        {
            bool CustomerAdded = false;
            try
            {
                SqlStr.Connection = Sqlconn;
                Sqlstmt = "Insert into Customer(FirstName,LastName, Address, Phone) values (@FirstName,@LastName,@Address,@Phone)";

                using (SqlStr = new SqlCommand(Sqlstmt, Sqlconn))
                {
                    SqlStr.Parameters.AddWithValue("@FirstName", FirstName);
                    SqlStr.Parameters.AddWithValue("@LastName", LastName);
                    SqlStr.Parameters.AddWithValue("@Address", Address);
                    SqlStr.Parameters.AddWithValue("@Phone", Phone);

                    SqlStr.CommandText = Sqlstmt;
                    Sqlconn.Open();
                    Int32 affectedRecords = SqlStr.ExecuteNonQuery();

                    if (affectedRecords > 0)
                    {
                        CustomerAdded = true;
                    }
                    Sqlconn.Close();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Exception " + ex.Message);
                Sqlconn.Close();

            }
            return CustomerAdded;
        }
        public bool DeleteCustomer(int CustID)
        {
            bool CustDeleted = false;
            try
            {
                SqlStr.Connection = Sqlconn;
                Sqlstmt = "Delete from Customer where CustID = @CustID";

                using (SqlStr = new SqlCommand(Sqlstmt, Sqlconn))
                {
                    SqlStr.Parameters.AddWithValue("@CustID", CustID);
                    Sqlconn.Open();
                    Int32 affectedRecords = SqlStr.ExecuteNonQuery();

                    if (affectedRecords > 0)
                    {
                        CustDeleted = true;
                    }
                    Sqlconn.Close();
                }
                return CustDeleted;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Database Exception" + ex.Message);
                Sqlconn.Close();
                return false;
            }
        }
        public bool EditCust(string FirstName, string LastName, string Address, string Phone, int CustID)
        {
            bool CustEdited = false;
            try
            {
                SqlStr.Connection = Sqlconn;
                Sqlstmt = "Update Customer set FirstName = @FirstName, LastName = @LastName, Address = @Address, Phone = @Phone where CustID=@CustId";

                using (SqlStr = new SqlCommand(Sqlstmt, Sqlconn))
                {
                    SqlStr.Parameters.AddWithValue("@FirstName", FirstName);
                    SqlStr.Parameters.AddWithValue("@LastName", LastName);
                    SqlStr.Parameters.AddWithValue("@Address", Address);
                    SqlStr.Parameters.AddWithValue("@Phone", Phone);
                    SqlStr.Parameters.AddWithValue("@CustID", CustID);
                    Sqlconn.Open();
                    Int32 affectedRecords = SqlStr.ExecuteNonQuery();

                    if (affectedRecords > 0)
                    {
                        CustEdited = true;
                    }
                    Sqlconn.Close();
                }
                Sqlconn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Exception" + ex.Message);
                Sqlconn.Close();

            }
            return CustEdited;
        }
        public bool AddMovie(string Rating, string Title, string Year, string Rental_Cost, string Copies, string Plot, string Genre)
        {
            bool MovieAdded = false;
            try
            {
                SqlStr.Connection = Sqlconn;
                Sqlstmt = "Insert into Movies(Rating,Title, Year, Rental_Cost, Copies, Plot, Genre) values (@Rating,@Title,@Year,@Rental_Cost,@Copies,@Plot,@Genre)";

                using (SqlStr = new SqlCommand(Sqlstmt, Sqlconn))
                {
                    SqlStr.Parameters.AddWithValue("@Rating", Rating);
                    SqlStr.Parameters.AddWithValue("@Title", Title);
                    SqlStr.Parameters.AddWithValue("@Year", Year);
                    SqlStr.Parameters.AddWithValue("@Rental_Cost", Rental_Cost);
                    SqlStr.Parameters.AddWithValue("@Copies", Copies);
                    SqlStr.Parameters.AddWithValue("@Plot", Plot);
                    SqlStr.Parameters.AddWithValue("@Genre", Genre);

                    SqlStr.CommandText = Sqlstmt;
                    Sqlconn.Open();
                    Int32 affectedRecords = SqlStr.ExecuteNonQuery();

                    if (affectedRecords > 0)
                    {
                        MovieAdded = true;
                    }
                    Sqlconn.Close();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Exception " + ex.Message);
                Sqlconn.Close();

            }
            return MovieAdded;
        }

        public bool DeleteMovie(int MovieID)
        {
            bool MovieDeleted = false;
            try
            {
                SqlStr.Connection = Sqlconn;
                Sqlstmt = "Delete from Movies where MovieID = @MovieID";

                using (SqlStr = new SqlCommand(Sqlstmt, Sqlconn))
                {
                    SqlStr.Parameters.AddWithValue("@MovieID", MovieID);
                    Sqlconn.Open();
                    Int32 affectedRecords = SqlStr.ExecuteNonQuery();

                    if (affectedRecords > 0)
                    {
                        MovieDeleted = true;
                    }
                    Sqlconn.Close();
                }
                return MovieDeleted;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Database Exception" + ex.Message);
                Sqlconn.Close();
                return false;
            }
        }
        public bool UpdateMovie(string Rating, string Title, string Year, string Rental_Cost, string Copies, string Genre, string Plot, int MovieID)
        {
            bool MovieUpdated = false;
            try
            {
                SqlStr.Connection = Sqlconn;
                Sqlstmt = "Update Movies set Rating = @Rating, Title = @Title, Year = @Year, Rental_Cost = @Rental_Cost, Copies = @Copies, Genre = @Genre, Plot = @Plot where MovieID= @MovieID";

                using (SqlStr = new SqlCommand(Sqlstmt, Sqlconn))
                {
                    SqlStr.Parameters.AddWithValue("@Rating", Rating);
                    SqlStr.Parameters.AddWithValue("@Title", Title);
                    SqlStr.Parameters.AddWithValue("@Year", Year);
                    SqlStr.Parameters.AddWithValue("@Rental_Cost", Rental_Cost);
                    SqlStr.Parameters.AddWithValue("@Copies", Copies);
                    SqlStr.Parameters.AddWithValue("@Genre", Genre);
                    SqlStr.Parameters.AddWithValue("@Plot", Plot);
                    SqlStr.Parameters.AddWithValue("@MovieID", MovieID);
                    Sqlconn.Open();
                    Int32 affectedRecords = SqlStr.ExecuteNonQuery();

                    if (affectedRecords > 0)
                    {
                        MovieUpdated = true;
                    }
                    Sqlconn.Close();
                }
                Sqlconn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Exception" + ex.Message);
                Sqlconn.Close();

            }
            return MovieUpdated;
        }

        public bool AddRental(int CustIDFK, int MovieIDFK, DateTime DateRented)
        {
            bool RentalAdded = false;
            try
            {
                SqlStr.Connection = Sqlconn;
                Sqlstmt = "Insert into RentedMovies(CustIDFK,MovieIDFK,DateRented) values (@CustIDFK,@MovieIDFK,@DateRented)";


                using (SqlStr = new SqlCommand(Sqlstmt, Sqlconn))
                {
                    SqlStr.Parameters.AddWithValue("@CustIDFK", CustIDFK);
                    SqlStr.Parameters.AddWithValue("@MovieIDFK", MovieIDFK);
                    SqlStr.Parameters.AddWithValue("@DateRented", DateRented);



                    SqlStr.CommandText = Sqlstmt;
                    Sqlconn.Open();
                    Int32 affectedRecords = SqlStr.ExecuteNonQuery();

                    if (affectedRecords > 0)
                    {
                        RentalAdded = true;
                    }
                    Sqlconn.Close();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Exception " + ex.Message);
                Sqlconn.Close();

            }
            return RentalAdded;
        }
        public bool UpdateRental(DateTime DateReturned, int RMID)
        {
            bool MovieReturned = false;
            try
            {
                SqlStr.Connection = Sqlconn;
                Sqlstmt = "Update RentedMovies set DateReturned = @DateReturned where RMID = @RMID";


                using (SqlStr = new SqlCommand(Sqlstmt, Sqlconn))
                {

                    SqlStr.Parameters.AddWithValue("@DateReturned", DateReturned);
                    SqlStr.Parameters.AddWithValue("@RMID", RMID);

                    Sqlconn.Open();
                    Int32 affectedRecords = SqlStr.ExecuteNonQuery();

                    if (affectedRecords > 0)
                    {
                        MovieReturned = true;
                    }
                    Sqlconn.Close();
                }
                Sqlconn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Exception" + ex.Message);
                Sqlconn.Close();

            }
            return MovieReturned;
        }
    }
}
