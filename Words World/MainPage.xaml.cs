using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.IO.IsolatedStorage;
using Words_World.AonawareDictionaryService;

namespace dictionary
{
    public partial class MainPage : PhoneApplicationPage
    {

        DictServiceSoapClient dictsvcclient = null;

        private DictServiceSoapClient getDictServiceSoapClient()
        {
            if (null == dictsvcclient)
            {
                dictsvcclient = new DictServiceSoapClient();
            }
            return dictsvcclient;
        }
        public MainPage()
        {
            InitializeComponent();

        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {

        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            DictServiceSoapClient client = getDictServiceSoapClient();
            client.DictionaryListCompleted += new EventHandler<DictionaryListCompletedEventArgs>(ongetdictionarylistcompleted);
            client.DictionaryListAsync();
            base.OnNavigatedTo(e);


        }
        void ongetdictionarylistcompleted(object sender, DictionaryListCompletedEventArgs e)  // fetch the dictionary list and store it in the isolated storage settings 
        {
            IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
            Dictionary[] listofdictionaries;
            if (e.Error == null)  
            {
                listofdictionaries = e.Result;
                populatelistpicker(listofdictionaries, settings);
            }
            else if (settings.Contains("saveddictionarylist"))
            {
                listofdictionaries = settings["saveddictionarylist"] as Dictionary[];
                populatelistpicker(listofdictionaries, settings);

            }
            else
            {
                MessageBoxResult res = MessageBox.Show(" an error occured while retrieving dictionary list,do you want to try again?", "error", MessageBoxButton.OKCancel);

                if (MessageBoxResult.OK == res)
                {
                    getDictServiceSoapClient().DictionaryListAsync();
                }
            }

            settings.Save();
        }
        void populatelistpicker(Dictionary[] listofdictionaries, IsolatedStorageSettings settings)  //populate the list picker with list of fetched dictionaries
        {
            listpickerdictionarylist.Items.Clear();
            foreach (Dictionary dictionary in listofdictionaries)
            {
                listpickerdictionarylist.Items.Add(dictionary.Name);
            }
            settings["saveddictionarylist"] = listofdictionaries;
            string saveddictionaryname;                        //to select a particular dictionary
            if (settings.Contains("saveddictionary"))
            {
                saveddictionaryname = settings["saveddictionary"] as string;
            }
            else
                saveddictionaryname = "WordNet (r) 2.0";   //default dictionary name..
            foreach (string dictname in listpickerdictionarylist.Items)
            {
                if (dictname == saveddictionaryname)
                {
                    listpickerdictionarylist.SelectedItem = dictname;
                    break;
                }
            }
            settings["saveddictionary"] = listpickerdictionarylist.SelectedItem as string;
        }
        private void btngo_Click(object sender, RoutedEventArgs e)
        {
            txtblockwordmeaning.Text = "please wait...";
            IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
            if (txtboxinputword.Text.Trim().Length <= 0)
            {
                MessageBox.Show("please enter the word in the textbox and press find");
            }
            else
            {
                Dictionary[] listofdictionaries = settings["saveddictionarylist"] as Dictionary[];
                string selecteddictionary = listpickerdictionarylist.SelectedItem.ToString();
                string dictid = "ex";//default dictionary is wordnet( ex is dictionary id)
                foreach (Dictionary dict in listofdictionaries)
                {
                    if (dict.Name == selecteddictionary)
                    {
                        dictid = dict.Id;
                        break;
                    }
                }
                DictServiceSoapClient client = getDictServiceSoapClient();
                client.DefineInDictCompleted += new EventHandler<DefineInDictCompletedEventArgs>(ondefineindictcompleted);
                client.DefineInDictAsync(dictid, txtboxinputword.Text.Trim());    //define in dict accepts dictionary id and not name
            }
        }

        void ondefineindictcompleted(object sender, DefineInDictCompletedEventArgs e)     //to display the word definition to the user  
        {
            WordDefinition wd = e.Result;
            scrollviewer.ScrollToVerticalOffset(0.0f);
            if (wd == null || e.Error != null || wd.Definitions.Length == 0)
            {
                txtblockwordmeaning.Text = string.Format("no definitions were found for '{0}' in'{1}'", txtboxinputword.Text.Trim(), listpickerdictionarylist.SelectedItem.ToString().Trim());
            }
            else
            {
                foreach (Definition def in wd.Definitions)
                {
                    string str = def.WordDefinition;
                    str = str.Replace(" ", " ");  //some formatting
                    txtblockwordmeaning.Text = str;
                }
            }
        }
        protected override void OnNavigatingFrom(System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
            settings["saveddictionary"] = listpickerdictionarylist.SelectedItem as string;
            settings.Save();
            base.OnNavigatingFrom(e);
        }
        private void ontextboxinputwordgotfocus(object sender, RoutedEventArgs e)
        {
            TextBox txtbox = sender as TextBox;
            if (txtbox.Text.Trim().Length > 0)
            {
                txtbox.SelectionStart = 0; txtbox.SelectionLength = txtbox.Text.Length;
            }
        }

        private void PhoneApplicationPage_Loaded_1(object sender, RoutedEventArgs e)
        {

        }

        private void txtboxinputword_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void PhoneApplicationPage_Loaded_2(object sender, RoutedEventArgs e)
        {

        }

    }
}
