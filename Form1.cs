using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Phone
{
    public partial class Form1 : Form
    {
        SqlConnection connectionString = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["ConnectionString"]);

        private Timer timer;
        private int volumeNum = 10;
        private Button currentButton;
        private List<Button> buttons;

        private bool isInListBoxMode = false;

        public Form1()
        {
            InitializeComponent();
            InitializeTextBoxes();
            InitializeTimer();
            InitializeButtons();
        }

        #region Initialize
        private void InitializeTextBoxes()
        {
            UpdateDateTime();
        }

        private void InitializeTimer()
        {
            this.timer = new Timer();
            this.timer.Interval = 1000; // 1000 ms = 1 saniye
            this.timer.Tick += new EventHandler(Timer_Tick);
            this.timer.Start();
        }

        private void InitializeButtons()
        {
            buttons = new List<Button> { rehber, music, chat, camera, translate, calculator };

            currentButton = rehber; // İlk başta odaklanılan buton
            if (currentButton != null)
                currentButton.Focus();

            // ListBox kontrolü yapılmadan önce, hangi modda olduğumuzu kontrol edin
            up.Click += (sender, e) =>
            {
                if (isInListBoxMode)
                {
                    if (currentButton == rehber)
                        NavigateListBoxRehber(Direction.Up);
                    else if (currentButton == music)
                        NavigateListBoxMusic(Direction.Up);
                }
                else
                    Navigate(Direction.Up);
            };

            down.Click += (sender, e) =>
            {
                if (isInListBoxMode)
                {
                    if (currentButton == rehber)
                        NavigateListBoxRehber(Direction.Down);
                    else if (currentButton == music)
                        NavigateListBoxMusic(Direction.Down);
                }
                else
                    Navigate(Direction.Down);
            };

            left.Click += (sender, e) => Navigate(Direction.Left);
            right.Click += (sender, e) => Navigate(Direction.Right);
        }
        #endregion

        #region Load
        private void Form1_Load(object sender, EventArgs e)
        {
            volumeText.Text = volumeNum.ToString();
            buttons = new List<Button> { rehber, music, chat, camera, translate, calculator };

            currentButton = rehber; // İlk başta odaklanılan buton
            if (currentButton != null)
            {
                currentButton.Focus();
            }
        }
        #endregion

        #region Timer
        private void Timer_Tick(object sender, EventArgs e)
        {
            UpdateDateTime();
        }

        private void UpdateDateTime()
        {
            this.dateTextBox.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy");
            this.timeTextBox.Text = DateTime.Now.ToString("HH:mm:ss");
        }
        #endregion

        #region Volume
        private void volumeDown_Click(object sender, EventArgs e)
        {
            if (volumeNum == 0)
                MessageBox.Show("Sessizde.");
            else
                volumeNum--;
            volumeText.Text = volumeNum.ToString();
        }

        private void volumeUp_Click(object sender, EventArgs e)
        {
            if (volumeNum == 10)
                MessageBox.Show("Ses Düzeyi Maximumda.");
            else
                volumeNum++;
            volumeText.Text = volumeNum.ToString();
        }

        private void mute_Click(object sender, EventArgs e)
        {
            volumeNum = 0;
            volumeText.Text = volumeNum.ToString();
        }
        #endregion

        #region Unvisible
        private void Unvisible()
        {
            symbol.Visible = false;
            rehber.Visible = false;
            music.Visible = false;
            chat.Visible = false;
            camera.Visible = false;
            translate.Visible = false;
            calculator.Visible = false;
        }
        #endregion

        #region Visible
        private void visible()
        {
            symbol.Visible = true;
            rehber.Visible = true;
            music.Visible = true;
            chat.Visible = true;
            camera.Visible = true;
            translate.Visible = true;
            calculator.Visible = true;
        }
        #endregion

        #region Yönler
        private enum Direction
        {
            Up,
            Down,
            Left,
            Right
        }
        #endregion

        #region HomeButton
        private void home_Click(object sender, EventArgs e)
        {
            rehberListBox.Visible = false;
            numberText.Visible = false;
            musicListBox.Visible = false;
            telefonText.Visible = false;
            numberLabel.Visible = false;
            
            call.Enabled = false;
            uncall.Enabled = false;

            play.Visible = false;
            pause.Visible = false;

            visible();

            home.Enabled = false;

            isInListBoxMode = false; //Ana ekrana döndük
        }
        #endregion

        #region Uncall
        private void uncall_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Çağrı sonlandırıldı.");
            arananKisi.Visible = false;
        }
        #endregion

        #region Call
        private void call_Click(object sender, EventArgs e)
        {
            arananKisi.Visible = true;
            if (currentButton != null)
            {
                // Mevcut butona bağlı olarak uygun işlemi gerçekleştir
                if (currentButton == rehber && rehberListBox.SelectedIndex >= 0)
                {
                    string selectedItem = rehberListBox.SelectedItem.ToString();
                    string[] parts = selectedItem.Split('\t');
                    if (parts.Length < 2)
                    {
                        MessageBox.Show("Seçilen öğe geçersiz. Lütfen geçerli bir öğe seçin.");
                        return;
                    }

                    string name = parts[0];
                    string surname = parts[1];
                    string phoneNumber = numberText.Text;

                    // Telefon numarası ve seçili öğe bilgilerini içeren MessageBox göster
                    arananKisi.Text = $"{name} {surname} \n{phoneNumber} aranıyor...";
                }
                else
                {
                    MessageBox.Show("Rehber'de geçerli bir öğe seçilmedi.");
                }
            }
            else
            {
                MessageBox.Show("Mevcut Buton Boş");
            }
        }
        #endregion

        #region Navigate
        private void Navigate(Direction direction)
        {
            if (isInListBoxMode)
            {
                if (currentButton == rehber)
                {
                    NavigateListBoxRehber(direction);
                }
                else if (currentButton == music)
                {
                    NavigateListBoxMusic(direction);
                }
                return;
            }

            if (buttons == null || buttons.Count == 0)
                return; // Eğer buton listesi boşsa hiçbir şey yapma

            int currentIndex = buttons.IndexOf(currentButton);

            if (currentIndex == -1) // Eğer buton listede değilse
            {
                currentButton = buttons[0]; // Varsayılan olarak ilk butonu seç
                currentButton.Focus();
                return;
            }

            Button newButton = currentButton;

            switch (direction)
            {
                case Direction.Up:
                    // Yukarı hareket ettirme mantığı
                    if (currentButton == camera)
                        newButton = rehber;
                    else if (currentButton == translate)
                        newButton = music;
                    else if (currentButton == calculator)
                        newButton = chat; 
                    break;

                case Direction.Down:
                    // Aşağı hareket ettirme mantığı
                    if (currentButton == rehber)
                        newButton = camera;
                    else if (currentButton == music)
                        newButton = translate;
                    else if (currentButton == chat)
                        newButton = calculator;
                    break;

                case Direction.Left:
                    // Sola hareket ettirme mantığı
                    if (currentButton == music)
                        newButton = rehber;
                    else if (currentButton == chat)
                        newButton = music; 
                    else if (currentButton == translate)
                        newButton = camera; 
                    else if (currentButton == calculator)
                        newButton = translate;
                    break;

                case Direction.Right:
                    // Sağa hareket ettirme mantığı
                    if (currentButton == rehber)
                        newButton = music;
                    else if (currentButton == music)
                        newButton = chat; 
                    else if (currentButton == camera)
                        newButton = translate;
                    else if (currentButton == translate)
                        newButton = calculator; 
                    break;
            }

            currentButton = newButton;
            currentButton.Focus();

            if (currentButton == rehber)
                rehberListBox.Focus();
            else if(currentButton == music)
                musicListBox.Focus();
        }
        #endregion

        #region NavigateListBoxMusic
        private void NavigateListBoxMusic(Direction direction)
        {
            if (musicListBox.Items.Count == 0)
                return;

            int currentIndex = musicListBox.SelectedIndex;

            switch (direction)
            {
                case Direction.Up:
                    if (currentIndex > 0)
                        musicListBox.SelectedIndex = currentIndex - 1;
                    break;
                case Direction.Down:
                    if (currentIndex < musicListBox.Items.Count - 1)
                        musicListBox.SelectedIndex = currentIndex + 1;
                    break;
            }
        }
        #endregion

        #region NavigateListBoxRehber
        private void NavigateListBoxRehber(Direction direction)
        {
            if (rehberListBox.Items.Count == 0)
                return;

            int currentIndex = rehberListBox.SelectedIndex;

            switch (direction)
            {
                case Direction.Up:
                    if (currentIndex > 0)
                        rehberListBox.SelectedIndex = currentIndex - 1;
                    break;
                case Direction.Down:
                    if (currentIndex < rehberListBox.Items.Count - 1)
                        rehberListBox.SelectedIndex = currentIndex + 1;
                    break;
            }

            UpdatePhoneNumber();
        }
        #endregion

        #region UpdatePhoneNumber
        private void UpdatePhoneNumber()
        {
            if (rehberListBox.Items.Count > 0 && rehberListBox.SelectedIndex >= 0)
            {
                string selectedItem = rehberListBox.SelectedItem.ToString();
                string[] parts = selectedItem.Split('\t');

                if (parts.Length < 2)
                {
                    MessageBox.Show("Seçilen öğe geçersiz. Lütfen geçerli bir öğe seçin.");
                    return;
                }

                string name = parts[0];
                string surname = parts[1];

                try
                {
                    if (connectionString.State != ConnectionState.Open)
                        connectionString.Open();

                    string query = @"SELECT PhoneNumber FROM rehberTB WHERE Name = @Name AND Surname = @Surname";
                    SqlCommand cmd = new SqlCommand(query, connectionString);
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Surname", surname);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        numberText.Text = reader["PhoneNumber"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("Telefon numarası bulunamadı.");
                        numberText.Text = ""; // Telefon numarası bulunamazsa boş bırak
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Telefon numarası getirilirken hata oluştu: " + ex.Message);
                }
                finally
                {
                    if (connectionString.State != ConnectionState.Closed)
                        connectionString.Close();
                }
            }
            else
            {
                MessageBox.Show("Lütfen bir öğe seçin.");
            }
        }
        #endregion

        #region OkeyButton_Click
        private void OkeyButton_Click(object sender, EventArgs e)
        {
            if (currentButton != null)
            {
                if (currentButton == rehber)
                {
                    Rehber();
                    rehberListBox.Focus();
                    rehberListBox.SelectedIndex = 0; // İlk öğeye odaklan
                    isInListBoxMode = true;
                }
                else if (currentButton == music)
                {
                    Music();
                    musicListBox.Focus();
                    musicListBox.SelectedIndex = 0;
                    isInListBoxMode = true;
                }
                else if (currentButton == chat)
                {
                    Chat();
                    isInListBoxMode = false;
                }
                else if (currentButton == camera)
                {
                    Camera();
                    isInListBoxMode = false;
                }
                else if (currentButton == translate)
                {
                    Translate();
                    isInListBoxMode = false;
                }
                else if (currentButton == calculator)
                {
                    Calculator();
                    isInListBoxMode = false;
                }
                else
                    MessageBox.Show("Bilinmeyen buton");
            }
            else
                MessageBox.Show("Mevcut Buton Boş");
        }
        #endregion

        #region appFuncitons

        #region RehberApp
        private void Rehber()
        {
            home.Enabled = true;
            call.Enabled = true;
            uncall.Enabled = true;

            home.Visible = true;

            Unvisible();
            rehberListBox.Visible = true;
            numberText.Visible = true;
            numberLabel.Visible = true;

            #region sql-->listbox
            try
            {
                if (connectionString.State != ConnectionState.Open)
                    connectionString.Open();

                string query = @"SELECT Name,Surname FROM rehberTB";

                using (SqlCommand command = new SqlCommand(query, connectionString))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        rehberListBox.Items.Clear();

                        while (reader.Read())
                        {
                            // Her satırı ListBox'a ekle
                            string row = "";
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                row += reader.GetValue(i).ToString() + "\t";
                            }
                            rehberListBox.Items.Add(row.Trim());
                        }

                        if (rehberListBox.Items.Count > 0)
                        {
                            rehberListBox.SelectedIndex = 0; // İlk öğeye odaklan
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Rehber verileri çekilirken hata oluştu: " + ex.Message);
            }
            finally
            {
                if (connectionString.State != ConnectionState.Closed)
                    connectionString.Close();
            }
            #endregion
        }
        #endregion

        #region musicApp
        private void Music()
        {
            home.Enabled = true;
            play.Visible = true;
            pause.Visible = true;

            Unvisible();
            musicListBox.Visible = true;

            #region sql-->listbox
            try
            {
                if (connectionString.State != ConnectionState.Open)
                    connectionString.Open();

                string query = @"SELECT * FROM musicTB";

                using (SqlCommand command = new SqlCommand(query, connectionString))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        musicListBox.Items.Clear();

                        while (reader.Read())
                        {
                            // Her satırı ListBox'a ekle
                            string name = reader["Name"].ToString();
                            string surname = reader["Surname"].ToString();
                            string song = reader["Song"].ToString();
                            string row = $"{name} {surname} - {song}";
                            musicListBox.Items.Add(row);

                            // İlk öğeye odaklan
                            if (musicListBox.Items.Count > 0)
                                musicListBox.SelectedIndex = 0; 
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Müzik verileri çekilirken hata oluştu: " + ex.Message);
            }
            finally
            {
                if (connectionString.State != ConnectionState.Closed)
                    connectionString.Close();
            }
            #endregion

        }
        #endregion

        #region ChatApp
        private void Chat()
        {
            home.Enabled = true;

            Unvisible();

        }
        #endregion

        #region CameraApp
        private void Camera()
        {
            home.Enabled = true;

            Unvisible();
            telefonText.Visible = true;
        }
        #endregion

        #region TranslateApp
        private void Translate()
        {
            home.Enabled = true;

            Unvisible();

        }
        #endregion

        #region CalculatorApp
        private void Calculator()
        {
            home.Enabled = true;

            Unvisible();

        }
        #endregion

        #endregion

    }
}

//Okey butonuna basıldığında MusicPlayer() çalışsın