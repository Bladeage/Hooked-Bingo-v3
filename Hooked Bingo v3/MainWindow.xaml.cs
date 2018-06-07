using System;
using System.Collections.Generic;
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
using Microsoft.Win32;
using System.Net;
using System.Net.NetworkInformation;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace Hooked_Bingo_v3
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Globals
        bool onlineStatus = false; //Online Check
        int maxLines = 0, generated = 0; //Schlagworte (Online, wie Offline)
        string original = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Hooked" + "\\e3-5x6.png"; //Pfad zur Originaldatei
        string kopie = "";

        public MainWindow()
        {
            InitializeComponent();
            CheckOnlineStatus(); //Initialprüfung
        }

        //Button Methoden
        private void GenerateButton_Click(object sender, RoutedEventArgs e)
        {
            //Bildnummer
            maxLines = 0;
            generated++;

            //Status Label
            StatusLabel1.Content = "[...]";
            StatusLabel1.Foreground = System.Windows.Media.Brushes.Gray;

            //Sicherheitspüfung. Fehlervorbeugung.
            CheckOnlineStatus();

            //List resetten
            List<string> linesList = new List<string>();

            #region Liste mit Online/Offline Inhalt befüllen
            #region Datei anlegen 
            //Prüfen ob AppData Ordner "Hooked" und benötigte TXT Liste bereits vorhanden sind
            try
            {
                if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Hooked\\"))
                {
                    Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Hooked\\");
                }
                if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Hooked\\Lines.txt"))
                {
                    StreamWriter datenSchreiber = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Hooked\\Lines.txt");
                    datenSchreiber.Close();
                }
                else
                {
                    StreamReader datenLeser = new StreamReader(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Hooked\\Lines.txt");
                    datenLeser.Close();
                }
            }
            catch (Exception ex)
            {

            }

            //BMP Online beziehen, falls Online und BMP non-existent
            if (!File.Exists(original) && (onlineStatus))
            {
                WebClient webClient = new WebClient();
                webClient.DownloadFile("https://i.imgur.com/z0Jr5bS.png", original);
            }
            else if (!File.Exists(original) && (!onlineStatus))
            {
                MessageBox.Show("Kann Input Datei nicht beziehen.");
            }

            #endregion
            //Stream instanziieren
            if (onlineStatus)
            {
                //Online Liste von Pastebin beziehen
                string url = @"https://pastebin.com/raw/JdFZT8mH";
                WebClient client = new WebClient();
                StreamReader datenLeser = new StreamReader(client.OpenRead(url));
                string line = "";

                //Lines der Website im String Array speichern.
                //Feststellen Anzahl Schlagworte
                int i = 0;
                while ((line = datenLeser.ReadLine()) != null)
                {
                    linesList.Add(line);
                    i++;
                }
                maxLines = i;

                //Lines in Datei schreiben
                try
                {
                    StreamWriter datenSchreiber = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Hooked\\Lines.txt"); for (i = 0; i < maxLines; i++)
                    {
                        if (i < maxLines - 1)
                        {
                            datenSchreiber.WriteLine(linesList[i]);
                        }
                        else
                        {
                            datenSchreiber.Write(linesList[i]);
                        }
                    }
                    datenSchreiber.Close();
                }
                catch (Exception ex)
                {

                }
            }
            else
            {
                //Vorhandene Liste beziehen, falls existiert
                if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Hooked\\Lines.txt"))
                {
                    StreamReader datenLeser = new StreamReader(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Hooked\\Lines.txt");
                    while (!datenLeser.EndOfStream)
                    {
                        linesList.Add(datenLeser.ReadLine());
                        maxLines++;
                    }
                    datenLeser.Close();
                }
                else
                {
                    //Offline Liste kreieren
                    linesList.Add("Robin starrt wortlos mehrere Minuten in die Kamera");
                    linesList.Add("Mats und Robin machen rum");
                    linesList.Add("Mats ist genervt von Robin");
                    linesList.Add("Mega Gag von Dani aus dem Nichts!");
                    linesList.Add("Robin dreht durch");
                    linesList.Add("\"Nani ? !\"");
                    linesList.Add("Robin hat zu viel Koffein intus");
                    linesList.Add("Kein Koffein mehr für dich Robin");
                    linesList.Add("Dark Souls Vergleiche");
                    linesList.Add("Something Something Yoko Taro");
                    linesList.Add("FFXV DLC Trailer");
                    linesList.Add("Robin verheddert sich mit dem Mikro-Kabel");
                    linesList.Add("Noctis for Smash");
                    linesList.Add("Mehr WiiU Ports für die Switch!");
                    linesList.Add("Ein Sportler bei EA auf der Bühne");
                    linesList.Add("FF7 Remake Trailer mit \"now in development\" Schriftzug");
                    linesList.Add("Shenmue 3 wird wieder verschoben");
                    linesList.Add("Leo singt");
                    linesList.Add("Werbung für ZeGermanguy");
                    linesList.Add("Irgendwas mit Kinderhänden");
                    linesList.Add("Alle Facepalmen wegen Robin");
                    linesList.Add("Anspielung auf David Cage");
                    linesList.Add("Eine Präsentation läuft nicht wie geplant");
                    linesList.Add("Jokerfeld");
                    linesList.Add("Ein Leak ist wahr");
                    linesList.Add("BattleRoyale");
                    linesList.Add("Mats wird mit Essen beworfen");
                    linesList.Add("Nik spricht Fonts/Kameratechnik an");
                    linesList.Add("Awkwarde Publikums Interaktionen");
                    linesList.Add("Unangenehm übermotivierter Spieleentwickler");
                    linesList.Add("EA hat die halbe Show voll mit Streamern");
                    linesList.Add("Ein Witzebuch wird zur Hand genommen");
                    linesList.Add("Live Musik on Stage");
                    linesList.Add("schlecht geskriptete Gameplay Sequenzen");
                    maxLines = 34;
                }
            }
            //Festlegen Schlagworte Label
            CountLabel1.Content = maxLines;
            #endregion

            #region Liste randomizen + kürzen auf max. 30 Schlagworte
            Random rnd = new Random(); //Zufallszahlen Generator
            //Liste auf max. 5x6= 30 Schlagworte kürzen
            while (linesList.Count() > 30)
            {
                linesList.RemoveAt(rnd.Next(0, linesList.Count()));
            }
            maxLines = 30;

            //Liste randomizen
            int n = maxLines;
            while (n > 1)
            {
                n--;
                int k = rnd.Next(n + 1);
                var value = linesList[k];
                linesList[k] = linesList[n];
                linesList[n] = value;
            }
            #endregion

            #region BMP laden, manipulieren, speichern

            //Bilddatei laden
            Bitmap bitmap = new Bitmap(original);
            Graphics g = Graphics.FromImage(bitmap);
            //Anpassung des zu zeichnenden Strings
            StringFormat strFormat = new StringFormat();
            strFormat.Alignment = StringAlignment.Center;
            strFormat.LineAlignment = StringAlignment.Near;

            //Zeichnen
            int x1 = 85;
            int zähler = 0; //sicherheit
            for (int iy = 1; iy <= 6; iy++)
            {
                int y1 = 250, x2 = 272, y2 = 156; //Ungefähre Koordinaten
                for (int ix = 1; ix <= 5; ix++)
                {
                    g.DrawString(linesList[zähler], new Font("Tahoma", 18), System.Drawing.Brushes.White, new RectangleF(x1, y1, x2, y2), strFormat);
                    y1 = y1 + y2;
                    zähler++;
                }
                x1 = x1 + x2;
            }

            //Speichern und ablegen
            kopie = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Hooked E3 Bingo.png"; //Pfad zur Kopie (temp)
            if (File.Exists(kopie))
            {
                File.Delete(kopie);
            }

            bitmap.Save(kopie, ImageFormat.Png);
            g.Dispose();
            bitmap.Dispose();


            //Status Label
            StatusLabel1.Foreground = System.Windows.Media.Brushes.Green;
            StatusLabel1.Content = "Bingo Karte auf dem Desktop erstellt";

            //Aufräumen
            GC.Collect();
            System.GC.WaitForPendingFinalizers();

            //Öffnen
            System.Diagnostics.Process.Start(kopie);
            #endregion
        }

        private void BeendenButton_Click(object sender, RoutedEventArgs e)
        {
            if (File.Exists(original))
            {
                File.Delete(original);
            }
            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Hooked\\Lines.txt"))
            {
                File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Hooked\\Lines.txt");
            }
            if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Hooked\\"))
            {
                Directory.Delete(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Hooked\\");
            }
            this.Close();
        }//Done

        //Links
        private void Hooked_Website_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.hookedmagazin.de/");
        }
        private void Hooked_Forum_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://forum.hookedmagazin.de/");
        }
        private void Hooked_Teamspeak_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("ts3server://ts.hookedmagazin.de?password=Hooked&addbookmark=Hooked | ... on Communication");
        }
        private void Hooked_Discord_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://discord.gg/K3weCzH");
        }
        private void Unterstuetzt_Hooked_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.hookedmagazin.de/support-hooked/");
        }
        private void Hooked_Twitch_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.twitch.tv/hookedlive");
        }

        //Logik Methoden
        private void CheckOnlineStatus()
        {
            //Online Status feststellen
            //ggf. StatusLabel ändern
            try
            {
                Ping pingSender = new Ping();
                PingReply reply = pingSender.Send("8.8.8.8", 5000);
                if (reply.Status == IPStatus.Success)
                {
                    onlineStatus = true;
                    StatusLabel.Foreground = System.Windows.Media.Brushes.Green;
                    StatusLabel.Content = "Online";
                }
            }
            catch (System.Net.NetworkInformation.PingException)
            {
                onlineStatus = false;
                StatusLabel.Foreground = System.Windows.Media.Brushes.Red;
                StatusLabel.Content = "Offline";
            }
        }//Done
    }
}
