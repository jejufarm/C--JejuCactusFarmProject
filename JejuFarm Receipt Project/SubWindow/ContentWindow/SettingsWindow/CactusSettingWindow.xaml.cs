using IniSettings;
using JejuFarm_Receipt_Project.Binding.ObjectViewModel;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;

namespace JejuFarm_Receipt_Project.SubWindow.ContentWindow.SettingsWindow
{
    /// <summary>
    /// CactusSettingWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class CactusSettingWindow : System.Windows.Controls.UserControl
    {
        private INISetting ini;
        private int selectedIndex = -1;
        public CactusSettingWindow()
        {
            InitializeComponent();
            CactusListView.ItemsSource = CactusListBoxViewModel.GetInstance();
            ini = new INISetting();
            CacutsListBoxModel.Load(ini.LoadCactusList());
        }
        private bool able()
        {
            if (TitleText.Text == "" || TitleText.Text.Length < 2 || PriceText.Text == "" || !int.TryParse(PriceText.Text, out _))
                return false;
            return true;
        }

        private void CactusListView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double remainingSpace = CactusListView.ActualWidth;
            if (remainingSpace > 0)
                for (int idx = 0; idx < 2; idx++)
                    (CactusListView.View as GridView).Columns[idx].Width = Math.Ceiling(remainingSpace / 2);
        }


        private void CactusListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            dynamic meta_data = sender as dynamic;
            TitleText.Text = meta_data.SelectedItem.Title;
            PriceText.Text = meta_data.SelectedItem.Price.ToString();
            selectedIndex = meta_data.SelectedIndex;
            ButtonText.Text = "수정/삭제";
        }

        private void CactusListView_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            CactusListView.SelectedItem = null;
            TitleText.Text = "";
            PriceText.Text = "";
            selectedIndex = -1;
            ButtonText.Text = "추가";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (selectedIndex > -1)
            {
                if (able())
                {
                    if (CacutsListBoxModel.UpdateItem(selectedIndex, TitleText.Text, Convert.ToInt32(PriceText.Text)))
                    {
                        var item = CacutsListBoxModel.GetInstance()[selectedIndex];
                        ini.WriteProerty("CactusList", item.Key, item.Title + " " + item.Price);
                        CactusListView.SelectedItem = null;
                        TitleText.Text = "";
                        PriceText.Text = "";
                        selectedIndex = -1;
                        ButtonText.Text = "추가";
                    }
                }
                else
                {
                    System.Windows.MessageBox.Show("제대로 입력해주세요.");
                }
            }
            else
            {
                if (able())
                {
                    int key = CacutsListBoxModel.InsertItem(TitleText.Text, Convert.ToInt32(PriceText.Text));
                    ini.WriteProerty("CactusList", "Cactus" + key, TitleText.Text + " " + Convert.ToInt32(PriceText.Text));
                    CactusListView.SelectedItem = null;
                    TitleText.Text = "";
                    PriceText.Text = "";
                    selectedIndex = -1;
                }
                else
                {
                    System.Windows.MessageBox.Show("제대로 입력해주세요.");
                }
            }
        }

        private void Button_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (System.Windows.Forms.MessageBox.Show("정말로 삭제하겠습니까?", "예/아니오", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int key = CacutsListBoxModel.DeleteItem(selectedIndex);
                ini.WriteProerty("CactusList", "Cactus" + key, null);
                CactusListView.SelectedItem = null;
                TitleText.Text = "";
                PriceText.Text = "";
                selectedIndex = -1;
                ButtonText.Text = "추가";
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
            if(e.Key.ToString() == "Return")
            {
                Button_Click(null, null);
                TitleText.Focus();
            }
        }

    }
}
