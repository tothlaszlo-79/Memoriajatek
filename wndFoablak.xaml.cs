using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Application = System.Windows.Application;

namespace Memoriajatek
{
	/// <summary>
	/// 
	///                Memóriajáték
	///                
	/// A program a közismert, képeken alapuló memóriajáték 
	/// egy egyszerű változatát valósítja meg. 
	/// </summary>
	public partial class wndFőablak : Window
  {
    #region Adattagok definíciója

    /// <summary>
    /// A hátlapként alkalmazott kép állomány neve.
    /// </summary>
    string HátlapNév = "hatter.JPG";

    /// <summary>
    /// A játékban felhasznált mesefigurákat tartalmazó 
    /// kép állományok nevei.
    /// </summary>
    string[] KépNevek = { "grabowski.jpg", "kukori.jpg", 
                          "kuldonc.jpg", "vili.jpg"
                        };

    /// <summary>
    /// A hátlapkép referenciáját tároló változó.
    /// </summary>
    BitmapImage biHátlapKép;
    
    /// <summary>
    /// Tömb a nyolc kép objektum referenciáinak tárolására.
    /// </summary>
    BitmapImage[] biKépek = new BitmapImage[8];

    /// <summary>
    /// Tömb a mesefigurák megjelenítésére használt Image
    /// komponensek referenciáinak tárolására.
    /// </summary>
    Image[] imKépHelyek;


    /// <summary>
    /// Véletlenszámok előállítására szolgáló objektum.
    /// </summary>
    Random Véletlen = new Random();

    #endregion

    #region Konstruktor és segédmetódusok

    /// <summary>
    /// A főablak konstruktora. Létrehozza a képeket megjelenítő 
    /// komponensek tömbjét, és betölti a képeket a memóriába.
    /// </summary>
    public wndFőablak()
		{
      // Komponensek léterhozása és inicializálása.
			InitializeComponent();
      // Képeket megjelenítő komponensek tömbjének létrehozása.
      imKépHelyek = new Image[]{ im00, im01, im02, im03, im10, im11, im12, im13 };
      // Képek betöltése a memóriába.
      KépeketBetölt();
      // Minden komponens a hátlapképet mutatja.
      HátlapképpelKitölt();
		}
	
    /// <summary>
    /// Betölti memóriába a játékhoz használt képeket. A képek a 
    /// projekt gyökérkönyvtárában levő Kepek alkönyvtárában kell legyenek.
    /// </summary>
    private void KépeketBetölt()
    {
      try
      {
        // Pack Uri-k használata: http://msdn.microsoft.com/en-us/library/aa970069.aspx
        // Hátlapkép betöltése.
        biHátlapKép = new BitmapImage(new Uri(@"Kepek/"+HátlapNév, UriKind.Relative));
        // A négy mesefigura kép betöltése.
        for (int i = 0; i < 4; i++)
        {
          biKépek[i] = new BitmapImage(new Uri(@"Kepek/" + KépNevek[i], 
            UriKind.Relative));
          // A másodpéldányokat beazonosító referenciák.
          biKépek[i + 4] = biKépek[i];
        }
      }
      catch (Exception)
      {
        MessageBox.Show("A képek nem találhatók a megadott útvonalon!",
          "Hiba", MessageBoxButton.OK);
      }
    }

    /// <summary>
    /// Mind a nyolc helyen a hátlapképet jeleníti meg.
    /// </summary>
    private void HátlapképpelKitölt()
    {
      for (int i = 0; i < 8; i++)
      {
        imKépHelyek[i].Source = biHátlapKép;
        // Kép komponens frissítése.
        imKépHelyek[i].Refresh();
      }
    }

    /// <summary>
    /// A Kever menüpont hatására az induláskor automatikusan 
    /// betöltött négy pár képet véletlenszerű elrendezésben 
    /// megjeleníti.
    /// Várakozik két másodpercet, majd a képek helyett zöldre 
    /// festett téglalapokat jelenít meg (hatter.jpg). 
    /// </summary>
    /// <param name="sender">A menüpont objektum.</param>
    /// <param name="e"></param>
    private void miKever_Click(object sender, RoutedEventArgs e)
    {
      // Véletlen sorrend meghatározása.
      VéletlenSorrendbeRak();
      // Képek láthatóvá tétele.
      KépeketMegmutat();
      //MessageBox.Show("1");
      // Várakozás két másodpercig.
      Thread.Sleep(2000);
      // Háttérkép megjelenítése mind a nyolc helyen.
      HátlapképpelKitölt();
    }

    /// <summary>
    /// Meghatározza a képek véletlenszerű sorrendjét.
    /// </summary>
    private void VéletlenSorrendbeRak()
    {
      // Létrehozunk egy lista objektumot a képek referenciáinak tárolássára.
      List<BitmapImage> KépLista = new List<BitmapImage>();
      // Mind a 8 kép referenciáját elhelyezzük a listában.
      KépLista.AddRange(biKépek);
      // "Húzás a kalapból" véletlenszerűen kivesszük a nyolc referenciát.
      for (int i = 0; i < 8; i++)
      {
        // A maradék listából véletleszerűen kiválasztunk egy elemet.
        int Sorszám = Véletlen.Next(0, KépLista.Count);
        // Betesszük a tömb i. helyére
        biKépek[i] = KépLista[Sorszám];
        // Eltávolítjuk a listáról.
        KépLista.RemoveAt(Sorszám);
      }
    }

    /// <summary>
    /// Láthatóvá teszi a nyolc képet.
    /// </summary>
    private void KépeketMegmutat()
    {
      for (int i = 0; i < 8; i++)
      {
        imKépHelyek[i].Source = biKépek[i];
        // Kép komponens frissítése.
        imKépHelyek[i].Refresh();
      }
    }

    /// <summary>
    /// A Kérdez menüpont hatására egy új ablakot jelenít meg 
    /// Kérdés fejléccel. Az ablakban az előzőekben látható 
    /// négy képtípus valamelyike jelenik meg véletlenszerűen. 
    /// A felhasználó kiválasztja, hogy szerinte mely helyeken fordult elő a 
    /// főablakban a kép, majd kattint az OK gombon.
    /// Ekkor eltűnik a Kérdez ablak, és a válasz helyességétől 
    /// függően az alábbi két üzenetablak egyike jelenik meg, 
    /// majd újból láthatóvá válik a nyolc kép úgy, ahogy azt a 
    /// legelső ábrán láthatjuk.
    /// </summary>
    /// <param name="sender">A Kérdez menüpont objektum.</param>
    /// <param name="e"></param>
    private void miKérdez_Click(object sender, RoutedEventArgs e)
    {
      // Ablak objektum létrehozása.
      wndKérdés wndKérdés = new wndKérdés();
      // Kép sorszám kiválasztása véletlenszerűen.
      int Sorszám = Véletlen.Next(0, 8);
      // Keresett kép kiválasztása.
      BitmapImage KeresettKép = biKépek[Sorszám];
      // Kép komponenshez rendelése.
      wndKérdés.biKép = KeresettKép;
      // Megjeleníti a párbeszédablakot.
      wndKérdés.ShowDialog();
      // Lekérdezi, hogy mely pozíciókat választotta ki a 
      // felhasználó, majd előállítja az adott pozícióban található
      // képek referenciáit.
      BitmapImage biElső = biKépek[wndKérdés.ElsőSorszám];
      BitmapImage biMásodik = biKépek[wndKérdés.MásodikSorszám];
      // Megvizsgálja, hogy a keresett kép azonos-e a két megjelölt 
      // képpel.
      if (biElső == KeresettKép && biMásodik == KeresettKép)
        MessageBox.Show("Hurrá eltaláltad!");
      else
        MessageBox.Show("Hát ez most nem jött össze!");
      // Újból láthatóvá teszi a képeket.
      KépeketMegmutat();
    }
    #endregion

    #region Eseménykezelők

    /// <summary>
    /// Kilép az alkalmazásból.
    /// </summary>
    /// <param name="sender">A Kilép menüpont objektum.</param>
    /// <param name="e"></param>
    private void miKilép_Click(object sender, RoutedEventArgs e)
    {
      Application.Current.Shutdown();
    }

    /// <summary>
    /// Súgó párbeszédablak megjelenítése.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void miSúgó_Click(object sender, RoutedEventArgs e)
    {
      //// Létrehozzuk
      //var wnd = new wndSúgó();
      //wnd.ShowDialog();
    }

    #endregion
	}
}
