using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MovieRent
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DatabaseManager objDB = new DatabaseManager();
        int count = 0;
        public MainWindow()
        {
            InitializeComponent();
            txtRentRentalCost.IsReadOnly = true;
            txtRentFirstName.IsReadOnly = true;
            txtRentLastName.IsReadOnly = true;
            txtRentAddress.IsReadOnly = true;
            txtRentTitle.IsReadOnly = true;
            txtDateRented.IsReadOnly = true;
            txtDateReturned.IsReadOnly = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                dgCustomers.ItemsSource = objDB.ListCustomer("%").DefaultView;
                dgMovies.ItemsSource = objDB.ListMovies("%").DefaultView;
                dgRentals.ItemsSource = objDB.ListRentals("%").DefaultView;
                dgCustomerShort.ItemsSource = objDB.ListCustomerShort("%").DefaultView;
                dgMovieShort.ItemsSource = objDB.ListMoviesShort("%").DefaultView;
                dgStats.ItemsSource = objDB.ListStatsMostRented("%").DefaultView;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }

        private void btnAddCust_Click(object sender, RoutedEventArgs e)
        {
            if (txtCustFirst.Text != "" && txtCustLast.Text != "" && txtAddress.Text != "" && txtPhone.Text != "")
            {
                bool CustomerAdded = objDB.AddCustomer(txtCustFirst.Text, txtCustLast.Text, txtAddress.Text, txtPhone.Text);

                if (CustomerAdded == true)
                {
                    MessageBox.Show("New Customer Added");
                    txtCustFirst.Clear();
                    txtCustLast.Clear();
                    txtAddress.Clear();
                    txtPhone.Clear();
                    dgCustomers.ItemsSource = objDB.ListCustomer("%").DefaultView;
                }
            }
            else
            {
                MessageBox.Show("Enter Customer Details");
            }

        }

        private void btnDeleteCust_Click(object sender, RoutedEventArgs e)
        {
            if (dgCustomers.SelectedIndex > -1)
            {
                MessageBoxResult dialogResult = MessageBox.Show("Are You Sure You Want To Delete The Customer?  ", "Customer ", MessageBoxButton.YesNoCancel);
                if (dialogResult.ToString() == "Yes")
                {
                    DataRowView row = (DataRowView)dgCustomers.SelectedItems[0];
                    Int32 CustID = Convert.ToInt32(row["CustID"]);

                    objDB.DeleteCustomer(CustID);
                    MessageBox.Show("Customer Deleted");
                    txtCustFirst.Clear();
                    txtCustLast.Clear();
                    txtAddress.Clear();
                    txtPhone.Clear();
                    dgCustomers.ItemsSource = objDB.ListCustomer("%").DefaultView;
                }
            }
            else
            {
                MessageBox.Show("Select Customer to delete");
            }
        }

        private void dgCustomers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dgCustomers.SelectedIndex > -1)
                {
                    DataRowView row = (DataRowView)dgCustomers.SelectedItems[0];
                    txtCustFirst.Text = Convert.ToString(row["FirstName"]);
                    txtCustLast.Text = Convert.ToString(row["LastName"]);
                    txtAddress.Text = Convert.ToString(row["Address"]);
                    txtPhone.Text = Convert.ToString(row["Phone"]);
                    txtCustID.Text = Convert.ToString(row["CustID"]);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("please do not click blank Row" + ex.Message);

            }

        }

        private void btnUpdateCust_Click(object sender, RoutedEventArgs e)
        {
            if (dgCustomers.SelectedIndex > -1)
            {
                MessageBoxResult dialogResult = MessageBox.Show("Are You Sure You Want To Edit The Customer?  ", "Customer ", MessageBoxButton.YesNoCancel);
                if (dialogResult.ToString() == "Yes")
                {
                    if (txtCustFirst.Text != "" && txtCustLast.Text != "" && txtAddress.Text != "" && txtPhone.Text != "")
                    {
                        DataRowView row = (DataRowView)dgCustomers.SelectedItems[0];
                        string Firstname = txtCustFirst.Text;
                        string LastName = txtCustLast.Text;
                        string Address = txtAddress.Text;
                        string Phone = txtPhone.Text;
                        int CustID = Convert.ToInt16(row["CustID"]);
                        objDB.EditCust(Firstname, LastName, Address, Phone, CustID);

                        bool CustomerEdited = objDB.EditCust(txtCustFirst.Text, txtCustLast.Text, txtAddress.Text, txtPhone.Text, Convert.ToInt32(CustID));

                        if (CustomerEdited == true)
                        {
                            MessageBox.Show("Customer Updated");
                            txtCustFirst.Text = Convert.ToString(row["FirstName"]);
                            txtCustLast.Text = Convert.ToString(row["LastName"]);
                            txtAddress.Text = Convert.ToString(row["Address"]);
                            txtPhone.Text = Convert.ToString(row["Phone"]);
                            dgCustomers.ItemsSource = objDB.ListCustomer("%").DefaultView;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Complete All fields");
                    }
                }
            }
            else
            {
                MessageBox.Show("Select a Customer to Update");
            }
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            dgCustomers.ItemsSource = objDB.ListCustomer(txtSearch.Text).DefaultView;
        }

        private void btnAddMovie_Click(object sender, RoutedEventArgs e)
        {

            if (txtMovieRating.Text != "" && txtMovieTitle.Text != "" && txtMovieYear.Text != "" && txtRentalCost.Text != "" && txtMovieCopies.Text != "" && txtMovieGenre.Text != "" && txtMoviePlot.Text != "")
            {

                bool MovieAdded = objDB.AddMovie(txtMovieRating.Text, txtMovieTitle.Text, txtMovieYear.Text, txtRentalCost.Text, txtMovieCopies.Text, txtMoviePlot.Text, txtMovieGenre.Text);

                if (MovieAdded == true)
                {
                    MessageBox.Show("New Movie Added");
                    txtMovieRating.Clear();
                    txtMovieTitle.Clear();
                    txtMovieYear.Clear();
                    txtRentalCost.Clear();
                    txtMovieCopies.Clear();
                    txtMovieGenre.Clear();
                    txtMoviePlot.Clear();
                    dgMovies.ItemsSource = objDB.ListMovies("%").DefaultView;


                }
            }
            else
            {
                MessageBox.Show("Enter Movie Details");
            }


        }

        private void btnDeleteMovie_Click(object sender, RoutedEventArgs e)
        {


            if (dgMovies.SelectedIndex > -1)
            {
                MessageBoxResult dialogResult = MessageBox.Show("Are You Sure You Want To Delete The Movie?  ", "Title ", MessageBoxButton.YesNoCancel);
                if (dialogResult.ToString() == "Yes")
                {
                    DataRowView row = (DataRowView)dgMovies.SelectedItems[0];
                    Int32 MovieID = Convert.ToInt32(row["MovieID"]);

                    objDB.DeleteMovie(MovieID);
                    MessageBox.Show("Movie Deleted");

                    txtMovieRating.Clear();
                    txtMovieTitle.Clear();
                    txtMovieYear.Clear();
                    txtRentalCost.Clear();
                    txtMovieCopies.Clear();
                    txtMovieGenre.Clear();
                    txtMoviePlot.Clear();
                    dgMovies.ItemsSource = objDB.ListMovies("%").DefaultView;
                    dgMovieShort.ItemsSource = objDB.ListMoviesShort("%").DefaultView;
                }
            }

            else
            {
                MessageBox.Show("Select a Movie to delete");
            }

        }

        private void txtSearchMovie_TextChanged(object sender, TextChangedEventArgs e)
        {
            dgMovies.ItemsSource = objDB.ListMovies(txtSearchMovie.Text).DefaultView;
        }

        private void dgMovies_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dgMovies.SelectedIndex > -1)
                {
                    DataRowView row = (DataRowView)dgMovies.SelectedItems[0];
                    txtMovieID.Text = Convert.ToString(row["MovieID"]);
                    txtMovieRating.Text = Convert.ToString(row["Rating"]);
                    txtMovieTitle.Text = Convert.ToString(row["Title"]);
                    txtMovieYear.Text = Convert.ToString(row["Year"]);
                    txtRentalCost.Text = Convert.ToString(row["Rental_Cost"]);
                    txtMovieCopies.Text = Convert.ToString(row["Copies"]);
                    txtMoviePlot.Text = Convert.ToString(row["Plot"]);
                    txtMovieGenre.Text = Convert.ToString(row["Genre"]);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("please do not click blank Row" + ex.Message);

            }
        }

        private void btnUpdateMovie_Click(object sender, RoutedEventArgs e)
        {
            if (dgMovies.SelectedIndex > -1)
            {
                MessageBoxResult dialogResult = MessageBox.Show("Are You Sure You Want To Update The Movie?  ", "Title ", MessageBoxButton.YesNoCancel);
                if (dialogResult.ToString() == "Yes")
                {
                    if (txtMovieRating.Text != "" && txtMovieTitle.Text != "" && txtMovieYear.Text != "" && txtRentalCost.Text != "" && txtMovieCopies.Text != "" && txtMovieGenre.Text != "" && txtMoviePlot.Text != "")
                    {
                        DataRowView row = (DataRowView)dgMovies.SelectedItems[0];
                        string Rating = txtMovieRating.Text;
                        string Title = txtMovieTitle.Text;
                        string Year = txtMovieYear.Text;
                        string Rental_Cost = txtRentalCost.Text;
                        string Copies = txtMovieCopies.Text;
                        string Genre = txtMovieGenre.Text;
                        string Plot = txtMoviePlot.Text;
                        int MovieID = Convert.ToInt16(row["MovieID"]);
                        objDB.UpdateMovie(Rating, Title, Year, Rental_Cost, Copies, Genre, Plot, MovieID);

                        bool MovieUpdated = objDB.UpdateMovie(txtMovieRating.Text, txtMovieTitle.Text, txtMovieYear.Text, txtRentalCost.Text, txtMovieCopies.Text, txtMovieGenre.Text, txtMoviePlot.Text, Convert.ToInt32(MovieID));

                        if (MovieUpdated == true)
                        {
                            MessageBox.Show("Movie Updated");
                            txtMovieRating.Text = Convert.ToString(row["Rating"]);
                            txtMovieTitle.Text = Convert.ToString(row["Title"]);
                            txtMovieYear.Text = Convert.ToString(row["Year"]);
                            txtRentalCost.Text = Convert.ToString(row["Rental_Cost"]);
                            txtMovieCopies.Text = Convert.ToString(row["Copies"]);
                            txtMoviePlot.Text = Convert.ToString(row["Plot"]);
                            txtMovieGenre.Text = Convert.ToString(row["Genre"]);
                            dgMovies.ItemsSource = objDB.ListMovies("%").DefaultView;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Complete All fields");
                    }
                }
            }
            else
            {
                MessageBox.Show("Select a Movie to Update");
            }
        }

        private void txtSearchCustShort_TextChanged(object sender, TextChangedEventArgs e)
        {
            dgCustomerShort.ItemsSource = objDB.ListCustomerShort(txtSearchCustShort.Text).DefaultView;
        }

        private void txtSearchMovieShort_TextChanged(object sender, TextChangedEventArgs e)
        {
            dgMovieShort.ItemsSource = objDB.ListMoviesShort(txtSearchMovieShort.Text).DefaultView;
        }

        private void btnIssue_Click(object sender, RoutedEventArgs e)
        {


            if (dgMovieShort.SelectedIndex > -1 && dgCustomerShort.SelectedIndex > -1)
            {



                if (txtRentFirstName.Text != "" && txtRentLastName.Text != "" && txtRentAddress.Text != "" && txtRentTitle.Text != "" && txtRentRentalCost.Text != "" && txtDateRented.Text != "")

                {
                    bool RentalAdded = objDB.AddRental(Convert.ToInt32(txtRentCustID.Text), Convert.ToInt32(txtRentMovieID.Text), Convert.ToDateTime(txtDateRented.Text));




                    if (RentalAdded == true)
                    {
                        MessageBox.Show("Movie Rented");
                        txtRentFirstName.Clear();
                        txtRentLastName.Clear();
                        txtRentAddress.Clear();
                        txtRentTitle.Clear();
                        txtRentRentalCost.Clear();
                        txtDateRented.Clear();
                        txtSearchCustShort.Clear();
                        txtSearchMovieShort.Clear();

                        dgRentals.ItemsSource = objDB.ListRentals("%").DefaultView;

                    }

                }
            }

            else
            {
                MessageBox.Show("Select Customer and Movie");
            }
            dgMovieShort.SelectedIndex = -1;
            dgCustomerShort.SelectedIndex = -1;
        }

        private void dgCustomerShort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime DateRented = DateTime.Now;

            try
            {
                if (dgCustomerShort.SelectedIndex > -1)
                {
                    DataRowView row = (DataRowView)dgCustomerShort.SelectedItems[0];
                    txtRentFirstName.Text = Convert.ToString(row["FirstName"]);
                    txtRentLastName.Text = Convert.ToString(row["LastName"]);
                    txtRentAddress.Text = Convert.ToString(row["Address"]);
                    txtDateRented.Text = Convert.ToString(DateRented);
                    txtRentCustID.Text = Convert.ToString(row["CustID"]);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void dgMovieShort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dgMovieShort.SelectedIndex > -1)
                {
                    DataRowView row = (DataRowView)dgMovieShort.SelectedItems[0];
                    txtRentTitle.Text = Convert.ToString(row["Title"]);
                    txtRentRentalCost.Text = Convert.ToString(row["Rental_Cost"]);
                    txtRentMovieID.Text = Convert.ToString(row["MovieID"]);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            if (dgRentals.SelectedIndex > -1)
            {
                MessageBoxResult dialogResult = MessageBox.Show("Are You Sure You Want To Return The Movie?  ", "Movie ", MessageBoxButton.YesNoCancel);
                if (dialogResult.ToString() == "Yes")
                {
                    if (txtRentReturn.Text != "")
                    {
                        if (txtDateReturned.Text != "")
                        {
                            MessageBox.Show("Movie Already Returned");
                            dgRentals.SelectedIndex = -1;
                        }
                        else
                        {
                            DataRowView row = (DataRowView)dgRentals.SelectedItems[0];
                            DateTime Return = Convert.ToDateTime(txtRentReturn.Text);
                            int RMID = Convert.ToInt16(row["RMID"]);
                            objDB.UpdateRental(Return, RMID);

                            bool RentalReturned = objDB.UpdateRental(Convert.ToDateTime(txtRentReturn.Text), Convert.ToInt16(row["RMID"]));

                            if (RentalReturned == true)
                            {
                                MessageBox.Show("Movie Returned");
                                txtRentReturn.Text = Convert.ToString(row["DateReturned"]);
                                dgRentals.ItemsSource = objDB.ListRentals("%").DefaultView;

                            }

                        }

                    }
                }
            }



            else
            {
                MessageBox.Show("select a movie to return");
            }

        }

        private void dgRentals_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime DateReturned = DateTime.Now;
            DateTime RentDateReturned = DateTime.Now;
            try
            {
                if (dgRentals.SelectedIndex > -1)
                {
                    DataRowView row = (DataRowView)dgRentals.SelectedItems[0];
                    txtRentFirstName.Text = Convert.ToString(row["FirstName"]);
                    txtRentLastName.Text = Convert.ToString(row["LastName"]);
                    txtRentAddress.Text = Convert.ToString(row["Address"]);
                    txtDateRented.Text = Convert.ToString(row["DateRented"]);
                    txtRentTitle.Text = Convert.ToString(row["Title"]);
                    txtRentRentalCost.Text = Convert.ToString(row["Rental_Cost"]);
                    txtRentCopies.Text = Convert.ToString(row["Copies"]);
                    txtRentReturn.Text = Convert.ToString(RentDateReturned);
                    txtDateReturned.Text = Convert.ToString(row["DateReturned"]); ;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void radioButton_Checked(object sender, RoutedEventArgs e)
        {
            dgRentals.ItemsSource = objDB.ListRentals("%").DefaultView;
        }

        private void radioButton1_Checked(object sender, RoutedEventArgs e)
        {
            dgRentals.ItemsSource = objDB.ListOutRentals("%").DefaultView;
        }

        private void rbNumberOfMovies_Checked(object sender, RoutedEventArgs e)
        {
            dgStats.ItemsSource = objDB.ListStatsNumOfMovies("%").DefaultView;
        }

        private void rbNumberofAccounts_Checked(object sender, RoutedEventArgs e)
        {
            dgStats.ItemsSource = objDB.ListStatsNumOfAccounts("%").DefaultView;
        }

        private void rbRentedTheMost_Checked(object sender, RoutedEventArgs e)
        {
            dgStats.ItemsSource = objDB.ListStatsRentedTheMost("%").DefaultView;
        }

        private void rbMostRentedMovie_Checked(object sender, RoutedEventArgs e)
        {
            dgStats.ItemsSource = objDB.ListStatsMostRented("%").DefaultView;
        }

        private void txtMovieCopies_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
                e.Handled = true;
        }

        private void txtRentalCost_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
                e.Handled = true;
        }

        private void txtMovieYear_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
                e.Handled = true;
        }

        private void txtMovieGenre_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsLetter(e.Text, e.Text.Length - 1))
                e.Handled = true;
        }

        private void txtMovieRating_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.Text, e.Text.Length - 1))
                e.Handled = true;
        }

        private void txtCustFirst_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsLetter(e.Text, e.Text.Length - 1))
                e.Handled = true;
        }

        private void txtCustLast_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsLetter(e.Text, e.Text.Length - 1))
                e.Handled = true;
        }

        private void txtAddress_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.Text, e.Text.Length - 1))
                e.Handled = true;
        }

        private void txtPhone_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
                e.Handled = true;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
