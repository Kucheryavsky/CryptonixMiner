using System;
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

namespace CryptonixMiner
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string set = "setx GPU_FORCE_64BIT_PTR 0 " + Environment.NewLine +
            "setx GPU_MAX_HEAP_SIZE 100 " + Environment.NewLine + 
            "setx GPU_USE_SYNC_OBJECTS 1 " + Environment.NewLine + 
            "setx GPU_MAX_ALLOC_PERCENT 100 " + Environment.NewLine +
            "setx GPU_SINGLE_ALLOC_PERCENT 100 " + Environment.NewLine +
            "EthDcrMiner64.exe";
        List<string> pools = new List<string>();
        List<string> DualPools = new List<string>();
        Dictionary<string, string> CoinsEthermine = new Dictionary<string, string>();
        Dictionary<string, string> CoinsMPH = new Dictionary<string, string>();
        Dictionary<string, string> DualCoinsMPH = new Dictionary<string, string>();
        Dictionary<string, string> CoinsSuprnova = new Dictionary<string, string>();
        Dictionary<string, string> DualCoinsSuprnova = new Dictionary<string, string>();

        

        public MainWindow()
        {
            InitializeComponent();
        }

       public void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //MainPools
            pools.Add("Ethermine.org");
            pools.Add("Suprnova.cc");
            pools.Add("MiningPoolHub.com");
            pool.ItemsSource = pools;

            //DualPools
            DualPools.Add("Suprnova.cc");
            DualPools.Add("MiningPoolHub.com");
            DualPool.ItemsSource = DualPools;

            //MiningPoolHub Coins
            CoinsMPH.Add("Ethereum", "europe.ethash-hub.miningpoolhub.com:20535");
            CoinsMPH.Add("Ethereum Classic", "europe.ethash-hub.miningpoolhub.com:20555");
            CoinsMPH.Add("Musicoin", "europe.ethash-hub.miningpoolhub.com:20585");
            CoinsMPH.Add("Expanse", "europe.ethash-hub.miningpoolhub.com:20565");

            //MiningPoolHub Dual
            DualCoinsMPH.Add("SiaCoin", "stratum+tcp://hub.miningpoolhub.com:20550");

            //Suprnova Coins
            CoinsSuprnova.Add("Ethereum", "eth.suprnova.cc:5005");
            CoinsSuprnova.Add("UbiqCoin", "ubiq.suprnova.cc:3030");

            //Suprnova Dual
            DualCoinsSuprnova.Add("Decred", "dcr.suprnova.cc:3252");

            //Ethermine Coins
            CoinsEthermine.Add("Ethereum", "eu1.ethermine.org:4444");
            CoinsEthermine.Add("Ethereum Classic", "eu1-etc.ethermine.org:4444");


        }


        private void pool_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
          

            if (pool.SelectedItem == "MiningPoolHub.com")

            {
                MainCoin.ItemsSource = CoinsMPH;
                walog.Text = "Login:";
                MainCoin.IsEnabled = true;
            }
            else
            {
                if (pool.SelectedItem == "Suprnova.cc")
                {
                    MainCoin.ItemsSource = CoinsSuprnova;
                    walog.Text = "Login:";
                    MainCoin.IsEnabled = true;
                }
                else
                {
                        
                        walog.Text = "Wallet:";
                    MainCoin.ItemsSource = CoinsEthermine;
                    MainCoin.IsEnabled = true;
                }
            }
        }

        public void Hashrate()
        {

        }

        private void DualPool_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DualPool.SelectedItem == "MiningPoolHub.com")

            {
                DualCoin.ItemsSource = DualCoinsMPH;
               DualCoin.IsEnabled = true;
            }
            else
            {
                DualCoin.ItemsSource = DualCoinsSuprnova;
                DualCoin.IsEnabled = true;
            }
        }

        public void StartSoloMine()
        {
            
                string x;

                x = " -epool " + MainCoin.SelectedValue + " -ewal " + login.Text + "." + MinerName.Text + " -epsw " + Password.Text;
                string settings = set + x;
                string path;


                path = Environment.CurrentDirectory + "\\CryptonixSoloMiner.bat";
                System.IO.File.WriteAllText(path, settings);
                System.Diagnostics.Process myProcess = new System.Diagnostics.Process();
                myProcess.StartInfo.FileName = "CryptonixSoloMiner.bat";
                myProcess.StartInfo.Arguments = path;
            /*  myProcess.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                myProcess.StartInfo.CreateNoWindow = true;*/
                myProcess.Start();



        }

        public void StartDualMine()
        {
            
                string dc;
                if (DualCoin.SelectedItem == "SiaCoin")
                {
                    dc = "sc";
                }
                else
                {
                    dc = "dcr";
                }
                string x;
                x = " -epool " + MainCoin.SelectedValue + " -ewal " + login.Text + "." + MinerName.Text + " -epsw " + Password.Text + " -dpool " + DualCoin.SelectedValue + " -dwal " + DualLogin.Text + "." + DualMinerName.Text + " -dpsw " + Password.Text + " -dcoin " + dc + " -dcri " + dcri.Text;
                string settings = set + x;
                string path;
                path = Environment.CurrentDirectory + "\\CryptonixDualMiner.bat";
                System.IO.File.WriteAllText(path, settings);
                System.Diagnostics.Process myProcess = new System.Diagnostics.Process();
                myProcess.StartInfo.FileName = "CryptonixDualMiner.bat";
                myProcess.StartInfo.Arguments = path;
            /*  myProcess.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                myProcess.StartInfo.CreateNoWindow = true;*/
                myProcess.Start();
            
                


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (pool.SelectedValue != null)
            {
                if (login.Text != "")
                {
                    if (MinerName.Text != "")
                    {
                        if (MainCoin.SelectedValue != null)
                        {
                            if(DualPool.SelectedValue != null)
                            {
                                if (DualLogin.Text != "")
                                {
                                    if (DualMinerName.Text != "")
                                    {
                                        if (DualCoin.SelectedValue != null)
                                        {
                                            StartDualMine();
                                        }
                                        else
                                        {
                                            MessageBox.Show("Select Coin for Dual Mining!");                                           
                                        }
                                    }
                                    else { DualMinerName.Text = "CryptonixMiner"; }
                                }
                                else { MessageBox.Show("Set information about your Login for dual mining!"); }
                            }
                            else { StartSoloMine(); }
                        }
                        else { MessageBox.Show("Select Main Coin!"); }
                    }
                    else { MinerName.Text = "CryptonixMiner"; }
                }
                else { MessageBox.Show("Set information about your " + walog.Text + "!"); }

            }
            else { MessageBox.Show("Set pool!"); }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.paypal.me/three8three");
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            string btc = "Bitcoin: 1FePVVuWopLdedjKfWAAg52fJ17TZDAbUM";
            string eth = "Ethereum: 0x58d9011648a2ac2c857d923f7b73145ea65be8d4";
            MessageBox.Show(btc + Environment.NewLine + eth);

            


        }

     
    }


    
            

}
