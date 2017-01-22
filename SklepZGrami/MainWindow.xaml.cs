using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SklepZGrami
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Pracownik asortyment = new Pracownik(); //lista produktow na której bedzie pracował pracownik
        
        private Klient klient = new Klient();   //lista produktow wyswietlana dla klienta
        private List<Gra> pomoc = new List<Gra>(); //lista pomocnicza
        
        public MainWindow()
        {
            InitializeComponent();
            //Odpowiednie ustawienie widzialnosci paneli i innych na rozpoczęcie programu
            labelHaslo.Visibility = Visibility.Hidden;
            passwordBoxPracownik.Visibility = Visibility.Hidden;
            buttonZaloguj.Visibility = Visibility.Hidden;
            panelStronaPowitalna.Visibility = Visibility.Visible;
            panelPracownika.Visibility = Visibility.Hidden;
            panelDodajGreWideo.Visibility = Visibility.Hidden;
            panelDodajGrePlanszowa.Visibility = Visibility.Hidden;
            panelKlienta.Visibility = Visibility.Hidden;

            comboBoxSposobyPlatnosci.Items.Add("przelew");
            comboBoxSposobyPlatnosci.Items.Add("przy odbiorze");
            comboBoxSposobyPlatnosci.Items.Add("karta kredytowa");

            //zaladowanie listy domyslnych produktow
            asortyment.DomyslneProdukty();
        }

        //Nacisniecie przycisku pracownik powoduje mozliwosc zalogowania sie
        private void buttonPracownik_Click(object sender, RoutedEventArgs e)
        {
            labelHaslo.Visibility = Visibility.Visible;
            passwordBoxPracownik.Visibility = Visibility.Visible;
            buttonZaloguj.Visibility = Visibility.Visible;
        }

        //Klikniecie zaloguj powoduje sprawdzenie hasla i odpowiednia reakcję
        private void buttonZaloguj_Click(object sender, RoutedEventArgs e)
        {
            //Jesli haslo jest poprawne zostaje wyswietlony panel pracownika
            if (passwordBoxPracownik.Password.ToString() == "tajnehaslo")
            {
                panelStronaPowitalna.Visibility = Visibility.Hidden;
                panelPracownika.Visibility = Visibility.Visible;
                panelKlienta.Visibility = Visibility.Hidden;
                panelDodajGreWideo.Visibility = Visibility.Hidden;
                panelDodajGrePlanszowa.Visibility = Visibility.Hidden;
                AktualizujListboxProduktowPracownik();
            }
            //Jesli haslo jest nie poprawne wyskakuje komunikat o bledzie i uzytkownik zostaje poproszony o wspisanie poprawnego hasla
            else
            {
                MessageBox.Show("Haslo jest nie poprawne, spróbuj ponownie.", "Niepoprawne hasło");
                passwordBoxPracownik.Clear();
            }
        }

        //Klikniecie tego przycisku powoduje wyswietlenie panelu pozwalajacego na dodanie gry wideo
        private void buttonGraWideo_Click(object sender, RoutedEventArgs e)
        {
            panelDodajGreWideo.Visibility = Visibility.Visible;
            panelDodajGrePlanszowa.Visibility = Visibility.Hidden;
            //upewniam sie, że pola do wpisywania są puste
            textBoxCenaGraWideo.Text = "";
            textBoxGatunekGraWideo.Text = "";
            textBoxNazwaGraWideo.Text = "";
            textBoxOgraniczenieGraWideo.Text = "";
            textBoxPlatforma.Text = "";

        }

        //Klikniecie tego przycisku powoduje dodanie nowego obiektu typu Gra do listy, o ile formularz nie zawiera błędów
        private void buttonDodajGreWideo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double cena = Convert.ToDouble(textBoxCenaGraWideo.Text);
                if (cena <= 0) throw new ArgumentOutOfRangeException();
                int ograniczenie = Convert.ToInt32(textBoxOgraniczenieGraWideo.Text);
                if (ograniczenie <= 0) throw new ArgumentOutOfRangeException();
                asortyment.DodajGreWideo(textBoxNazwaGraWideo.Text, cena, textBoxGatunekGraWideo.Text, ograniczenie, textBoxPlatforma.Text);
                AktualizujListboxProduktowPracownik(); //powoduje pojawienie się nazwy nowego obiektu na listboxie

                textBlockKomunikatPracownik.Text = "Pomyślnie dodano produkt";
            }
            
            catch (Exception Ex)
            {
                textBlockKomunikatPracownik.Text = Ex.Message;
            }
        }

        //Klikniecie tego przycisku powoduje wyswietlenie panelu pozwalajacego na dodanie gry planszowej
        private void buttonGraPlanszowa_Click(object sender, RoutedEventArgs e)
        {
            panelDodajGreWideo.Visibility = Visibility.Hidden;
            panelDodajGrePlanszowa.Visibility = Visibility.Visible;
            //upewniam sie, że pola do wpisywania są puste
            textBoxCenaGraPlansz.Text = "";
            textBoxGatunekGraPlansz.Text = "";
            textBoxMaxOsob.Text = "";
            textBoxMinOsob.Text = "";
            textBoxNazwaGraPlansz.Text = "";
            textBoxOgraniczenieGraPlansz.Text = "";
        }

        //Klikniecie tego przycisku powoduje dodanie nowego obiektu typu Gra do listy, o ile formularz nie zawiera błędów
        private void buttonDodajGrePlanszowa_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double cena = Convert.ToDouble(textBoxCenaGraPlansz.Text);
                if (cena <= 0) throw new ArgumentOutOfRangeException();
                int ograniczenie = Convert.ToInt32(textBoxOgraniczenieGraPlansz.Text);
                if (ograniczenie <= 0) throw new ArgumentOutOfRangeException();
                int minOsob = Convert.ToInt32(textBoxMinOsob.Text);
                if (minOsob <= 0) throw new ArgumentOutOfRangeException();
                int maxOsob = Convert.ToInt32(textBoxMaxOsob.Text);
                if (maxOsob <= 0) throw new ArgumentOutOfRangeException();
                asortyment.DodajGrePlanszowa(textBoxNazwaGraPlansz.Text, cena, textBoxGatunekGraPlansz.Text, ograniczenie, minOsob, maxOsob);
                AktualizujListboxProduktowPracownik(); //powoduje pojawienie się nazwy nowego obiektu na listboxie

                textBlockKomunikatPracownik.Text = "Pomyślnie dodano produkt";
            }

            catch (Exception Ex)
            {
                textBlockKomunikatPracownik.Text = Ex.Message;
            }
        }

        //Wcisniecie tego przycisku spowoduje przejscie do panelu klienta
        private void buttonKlient_Click(object sender, RoutedEventArgs e)
        {
            panelKlienta.Visibility = Visibility.Visible;
            panelStronaPowitalna.Visibility = Visibility.Hidden;
            panelPracownika.Visibility = Visibility.Hidden;
            pomoc = asortyment.ZwrocListe();    //kopiuje aktualna liste produktow z panelu pracownika na liste pomocnicza 
            klient.KopiowanieListyGier(pomoc);      //kopiuje z listy pomocniczej na liste produktow wyswietlanych dla klienta
            AktualizujListboxProduktowKlient();     //Powoduje wyswietlenie nazw z listy produktow na odpowiednim listboxie
            labelKosztTekst.Content = "0zł";
        }

        //Przycisniecie przycisku przywraca panel powitalny
        private void buttonWrocDoMenuPracownik_Click(object sender, RoutedEventArgs e)
        {
            panelStronaPowitalna.Visibility = Visibility.Visible;
            panelPracownika.Visibility = Visibility.Hidden;
            panelPracownika.Visibility = Visibility.Hidden;
            labelHaslo.Visibility = Visibility.Hidden;
            passwordBoxPracownik.Visibility = Visibility.Hidden;
            buttonZaloguj.Visibility = Visibility.Hidden;
            passwordBoxPracownik.Clear();

        }

        //Przycisniecie przycisku przywraca panel powitalny
        private void buttonPowrotDoMenuKlient_Click(object sender, RoutedEventArgs e)
        {
            panelStronaPowitalna.Visibility = Visibility.Visible;
            panelPracownika.Visibility = Visibility.Hidden;
            panelKlienta.Visibility = Visibility.Hidden;
            labelHaslo.Visibility = Visibility.Hidden;
            passwordBoxPracownik.Visibility = Visibility.Hidden;
            buttonZaloguj.Visibility = Visibility.Hidden;
            passwordBoxPracownik.Clear();
        }

        //Metoda slużąca do przenoszenia wybranych produktów do koszyka
        private void buttonPrzeniesDoKoszyka_Click(object sender, RoutedEventArgs e)
        {
            //jesli nie zaznaczono nic na listboxie zostanie wyswietlony komunikat o bledzie
            if (listBoxListaProduktowKlient.SelectedIndex == -1) MessageBox.Show("Nie wybrano produktu", "Błąd");
            //jesli wybrano produkt zostanie on przeniesiony do koszyka
            else if (listBoxListaProduktowKlient.SelectedIndex >= 0)
            {
                if (klient.PoliczProdukty() > 0)    //upewnia się ze istnieja produkty
                {
                    //dodaje do listy koszyk obiekt o wybranym indeksie z listy produktow
                    klient.DodajDoKoszyka(klient.GraDoPrzeniesienia(listBoxListaProduktowKlient.SelectedIndex)); 
                }
            }
            AktualizujKoszyk();     //aktualizuje listbox koszyka i wyswietlany koszt
            
        }

        //aktualizuje listbox koszyka i wyswietlany koszt
        public void AktualizujKoszyk()
        {
            listBoxKoszyk.Items.Clear();
            double koszt = 0;

            foreach (string nazwaGry in klient.PobierzNazwyDoKoszyka())
            {
                listBoxKoszyk.Items.Add(nazwaGry);
            }
            
            foreach (double cenaGry in klient.PobierzCenyDoKoszyka())
            {
                koszt += cenaGry;
            }

            labelKosztTekst.Content = koszt + "zł";
        }

        //usuwa wybrany produkt z koszyka
        private void buttonUsunZKoszyka_Click(object sender, RoutedEventArgs e)
        {
            //Sposob dzialania jest prawie taki sam jak przy dodawaniu, z ta roznica ze teraz usuwamy obiekt z listy
            if (listBoxKoszyk.SelectedIndex == -1) MessageBox.Show("Nie wybrano produktu do usunięcia z koszyka", "Błąd");
            else if (listBoxKoszyk.SelectedIndex >= 0)
            {
                if (klient.PoliczIloscWKoszyku() > 0)
                {
                    klient.UsunZKoszyka(klient.GraDoUsuniecia(listBoxKoszyk.SelectedIndex));
                }
            }
            AktualizujKoszyk();

        }

        //Wyswietla informacje na temat produktu, o ile zostal on zaznaczony
        private void buttonInfo_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxListaProduktowKlient.SelectedIndex == -1)
                MessageBox.Show("Nie wybrano produktu z listy, spróbuj ponownie", "Błąd");
            else if (listBoxListaProduktowKlient.SelectedIndex >= 0)
            {
                MessageBox.Show(klient.Informacje(listBoxListaProduktowKlient.SelectedIndex), "Informacje o produkcie");
            }
        }

        //Powoduje oproznienie koszyku
        private void buttonWyczyscKoszyk_Click(object sender, RoutedEventArgs e)
        {
            if (klient.PoliczIloscWKoszyku() == 0) MessageBox.Show("Koszyk jest już pusty", "Błąd");
            else if (klient.PoliczIloscWKoszyku() > 0)
                {
                    klient.WyczyscListeKoszyk();
                }
            
            AktualizujKoszyk();
        }


        private void buttonPlatnosc_Click_1(object sender, RoutedEventArgs e)
        {
            //jesli koszyk jest pusty, zostaje wyswietlony błąd
            if (listBoxKoszyk.Items.Count == 0) MessageBox.Show("Koszyk jest pusty, nie wybrano produktów do kupna.", "Błąd");
            //jesli nie wybrano sposobu platnosci rowniez blad
            else if (comboBoxSposobyPlatnosci.SelectedIndex == -1) MessageBox.Show("Nie wybrano sposobu płatności");
            //wyswietlenie komunikatu o kwocie zamowienia
            else
            {
                textBlockKomunikatZakup.Text = "Dokonano pomyślnego zamówienia. Kwota to zapłacenia wynosi "
                    + labelKosztTekst.Content + ". Wydrukowano paragon.";
                WydrukujParagon(); //Paragon w formie pliku txt
            }
        }

        //Pozwala na usuniecie wybranego produktu przez pracownika
        private void buttonUsunDodany_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxProdukty.SelectedIndex == -1) MessageBox.Show("Nie wybrano produktu do usunięcia");
            else if (listBoxProdukty.SelectedIndex >= 0)
            {
                asortyment.UsunDodany(listBoxProdukty.SelectedIndex);
                AktualizujListboxProduktowPracownik();
            }
        }

        //metoda sluzaca do aktualizowania zawartosci listboxa
        private void AktualizujListboxProduktowPracownik()
        {
            listBoxProdukty.Items.Clear();
            foreach (string nazwaGry in asortyment.PobierzNazwy())
                listBoxProdukty.Items.Add(nazwaGry);
        }

        //metoda sluzaca do aktualizowania zawartosci listboxa
        public void AktualizujListboxProduktowKlient()
        {
            listBoxListaProduktowKlient.Items.Clear();
            foreach (string nazwaGry in klient.PobierzNazwy())
                listBoxListaProduktowKlient.Items.Add(nazwaGry);
        }

        public void WydrukujParagon() //drukowanie paragonu
       {
          string nazwa = DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt";
          string platnosc = Environment.NewLine + "Wybrano sposób płatności: " + comboBoxSposobyPlatnosci.SelectedItem.ToString() +
                ", kwota do zapłacenia wynosi: " + labelKosztTekst.Content;
            string koniec = Environment.NewLine + "Dziękujemy za zakup i zapraszamy ponownie!";
           
           using (StreamWriter sw = new StreamWriter(nazwa))
           {
              sw.Write(klient.PodajInformacje()); //uzywa metody podajinformacje, aby podac informacje o produktach z koszyka
                sw.Write(platnosc);
                sw.Write(koniec);           
           }       
          
       }

        //Sortuje alfabetycznie liste produktów wyswietlanych dla klienta
        private void buttonSortujAlfabetycznie_Click(object sender, RoutedEventArgs e)
        {
            klient.SortowanieAlfabetycznie();
            AktualizujListboxProduktowKlient();

        }

        //Sortuje malejaco wg cen liste produktów wyswietlanych dla klienta
        private void buttonSortowanieCenMalejaco_Click(object sender, RoutedEventArgs e)
        {
            klient.SortujCenyMalejaco();
            AktualizujListboxProduktowKlient();
        }
        
        //Sortuje rosnaco wg cen liste produktów wyswietlanych dla klienta
        private void buttonSortowanieCenRosnaco_Click(object sender, RoutedEventArgs e)
        {
            klient.SortujCenyRosnaco();
            AktualizujListboxProduktowKlient();
        }

        //Sortuje alfabetycznie liste produktów wyswietlanych dla pracownika
        private void buttonSortujAlfabetyczniePracownik_Click(object sender, RoutedEventArgs e)
        {
            asortyment.SortowanieAlfabetycznie();
            AktualizujListboxProduktowPracownik();
        }

        //Sortuje rosnaco wg cen liste produktów wyswietlanych dla pracownika
        private void buttonCenyRosnacoPracownik_Click(object sender, RoutedEventArgs e)
        {
            asortyment.SortujCenyRosnaco();
            AktualizujListboxProduktowPracownik();
        }

        //Sortuje malejaco wg cen liste produktów wyswietlanych dla pracownika
        private void buttonCenyMalejacoPracownik_Click(object sender, RoutedEventArgs e)
        {
            asortyment.SortujCenyMalejaco();
            AktualizujListboxProduktowPracownik();
        }

        //wyswietla informacje o wybranym produkcie w panelu pracownika
        private void buttonInformacjePracownik_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxProdukty.SelectedIndex == -1)
                textBlockKomunikatPracownik.Text = "Nie wybrano produktu z listy";
            else if (listBoxProdukty.SelectedIndex >= 0)
            {
                textBlockKomunikatPracownik.Text = asortyment.Informacje(listBoxProdukty.SelectedIndex);
            }
        }

        //nacisniecie spowoduje wylaczenie programu
        private void buttonWyjscie_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
