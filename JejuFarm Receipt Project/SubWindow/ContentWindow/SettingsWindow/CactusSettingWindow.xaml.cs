using IniSettings;
using JejuFarm_Receipt_Project.Binding.ObjectViewModel;
using ProgramCore.ObjectModel;
using ProgramServices;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;

namespace JejuFarm_Receipt_Project.SubWindow.ContentWindow.SettingsWindow
{
    /// <summary>
    /// CactusSettingWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class CactusSettingWindow : System.Windows.Controls.UserControl
    {
        private int selectedIndex = -1;
        public CactusSettingWindow()
        {
            InitializeComponent();
            CactusListView.ItemsSource = CactusListBoxViewModel.GetInstance();
            LoadCactusListDB();
        }
        private bool able()
        {
            if (TitleText.Text == "" || TitleText.Text.Length < 2 || PriceText.Text == "" || !int.TryParse(PriceText.Text, out _))
                return false;
            return true;
        }

        private void LoadCactusListDB()
        {
            CacutsListBoxModel.Load(ProgramService.GetDB().LoadCactusList());
        }

        private void CactusListView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double remainingSpace = CactusListView.ActualWidth;
            if (remainingSpace > 0)
                for (int idx = 0; idx < 2; idx++)
                    (CactusListView.View as GridView).Columns[idx].Width = Math.Ceiling(remainingSpace / 2 - 30);
        }


        private void CactusListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                dynamic meta_data = sender as dynamic;
                TitleText.Text = meta_data.SelectedItem.Title;
                PriceText.Text = meta_data.SelectedItem.Price.ToString();
                selectedIndex = meta_data.SelectedIndex;
                ButtonText.Text = "수정/삭제";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (!able())
            {
                System.Windows.MessageBox.Show("항목과 가격을 정확히 입력해주세요.");
                return;
            }
            int idx = -1;

            if (CactusListView.SelectedIndex > -1)
            {
                idx = selectedIndex;
            }
            else
            {
                idx = ProgramService.GetDB().GetMaxUid();
            }

            if (ButtonText.Text == "추가")
            {
                if(idx == -1)
                    idx = CactusListView.SelectedIndex;

                if (!ProgramService.GetDB().InsertCactusData(new CactusListForm()
                {
                    Index = idx,
                    Title = TitleText.Text,
                    Price = Convert.ToInt32(PriceText.Text)
                }))
                {
                    System.Windows.MessageBox.Show("항목을 추가하지 못하였습니다.");
                }
            }
            else
            {
                if (!ProgramService.GetDB().UpdateCactusData(new CactusListForm()
                {
                    Index = idx,
                    Title = TitleText.Text,
                    Price = Convert.ToInt32(PriceText.Text)
                }))
                {
                    System.Windows.MessageBox.Show("항목을 수정하지 못하였습니다.");
                }
                else
                {
                    ButtonText.Text = "추가";

                }
            }
            LoadCactusListDB();
            CactusListView.ScrollIntoView(CactusListView.Items[idx]);
            TitleText.Text = "";
            PriceText.Text = "";
            CactusListView.SelectedIndex = -1;
            selectedIndex = -1;

        }

        private void Button_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (selectedIndex > -1)
            {
                if (System.Windows.Forms.MessageBox.Show("정말로 삭제하겠습니까?", "예/아니오", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {

                    if (ProgramService.GetDB().DeleteCactusData(selectedIndex))
                    {

                        ButtonText.Text = "추가";
                        TitleText.Text = "";
                        PriceText.Text = "";
                        CactusListView.SelectedIndex = -1;
                        selectedIndex = -1;
                        LoadCactusListDB();
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("항목을 삭제하지 못하였습니다.");
                    }
                }
            }
        }

        private void TitleText_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key.ToString() == "Return")
            {
                if (selectedIndex == -1)
                {
                    PriceText.Focus();
                }
                else
                {
                    Button_Click(null, null);
                }
            }
        }

        private void PriceText_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key.ToString() == "Return")
            {
                Button_Click(null, null);
                TitleText.Focus();
            }
        }

        private void CactusListView_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            dynamic meta_data = sender as dynamic;
            Console.WriteLine("UP : " + meta_data.SelectedIndex);
        }

        private void CactusListView_MouseRightUp(object sender, MouseButtonEventArgs e)
        {
            TitleText.Text = "";
            PriceText.Text = "";
            CactusListView.SelectedIndex = -1;
            selectedIndex = -1;
            ButtonText.Text = "추가";
        }
    }
}