using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Memoriajatek
{
  /// <summary>
  /// Interaction logic for wndKerdes.xaml
  /// </summary>
  public partial class wndKérdés : Window
  {

    public wndKérdés()
    {
      InitializeComponent();
    }

    /// <summary>
    /// Tulajdonság a kép komponens tartalmának beállításához.
    /// </summary>
    public BitmapImage biKép { set { imKép.Source = value; } }

    /// <summary>
    /// Tulajdonság az első előfordulás helyének lekérdezéséhez.
    /// </summary>
    /// 
    public int ElsőSorszám 
    { 
      get 
      {
        // Végigmegyünk a GroupBox-ban található rács összes RadioButton típusú
        // gyermekén (tudva, hogy az első két gyermek Label típusú), és visszaadjuk
        // annak a sorszámát, amelyik kijelölt állapotban van.
        // Ha egyik se lenne kijelölve, akkor 0-t ad vissza.
        for (int i = 2; i < grElső.Children.Count; i++)
        {
          var rbAktuális = grElső.Children[i] as RadioButton;
          if (rbAktuális != null && rbAktuális.IsChecked == true)
            return i - 2;
        }
        return 0;
      } 
    }
    /// <summary>
    /// Tulajdonság a második előfordulás helyének lekérdezéséhez.
    /// </summary>
    public int MásodikSorszám { 
      get 
      {
        // Végigmegyünk a GroupBox-ban található rács összes RadioButton típusú
        // gyermekén (tudva, hogy az első két gyermek Label típusú), és visszaadjuk
        // annak a sorszámát, amelyik kijelölt állapotban van.
        // Ha egyik se lenne kijelölve, akkor 0-t ad vissza.
        for (int i = 2; i < grMásodik.Children.Count; i++)
        {
          var rbAktuális = grMásodik.Children[i] as RadioButton;
          if (rbAktuális != null && rbAktuális.IsChecked == true)
            return i - 2;
        }
        return 0;
      } 
    }
      

    private void btOK_Click(object sender, RoutedEventArgs e)
    {
      Close();
    }
  }
}
