﻿using System.Windows;
using System.Linq;
using ConnectionClass;

namespace CybersportTournament
{
    /// <summary>
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow()
        {
            InitializeComponent();
        }

        private void RegistrationClick(object sender, RoutedEventArgs e)
        {
            if (Login.Text == "" || Password.Password == "" || FirstName.Text == "" || SecondName.Text == "" || Email.Text == "")
            {
                RegistrationEmptyFieldsErrorWindow refew = new RegistrationEmptyFieldsErrorWindow();
                refew.Show();
                return;
            }
            if (Connection.db.Users.Select(item => item.Login).Contains(Login.Text))
            {
                RegistrationExistingUserErrorWindow reuew = new RegistrationExistingUserErrorWindow();
                reuew.Show();
                return;
            }

            Users user = new Users()
            {
                Login = Login.Text,
                Password = Password.Password,
                FirstName = FirstName.Text,
                SecondName = SecondName.Text,
                Email = Email.Text
            };
            if (MiddleName.Text != "")
            {
                user.MiddleName = MiddleName.Text;
            }

            Connection.db.Users.Add(user);
            Connection.db.SaveChanges();
            RegitrationConfirmedWindow rcw = new RegitrationConfirmedWindow();
            rcw.Show();

            AuthorizationWindow aw = new AuthorizationWindow();
            aw.Show();
            this.Close();
        }

        private void BackClick(object sender, RoutedEventArgs e)
        {
            AuthorizationWindow aw = new AuthorizationWindow();
            aw.Show();
            this.Close();
        }
    }
}
