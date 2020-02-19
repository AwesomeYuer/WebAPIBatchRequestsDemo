using Microshaoft;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XF.App1
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            //var requestBaseAddress = serviceBaseAddress;
            var requestBaseAddress = "http://10.0.2.2:9000/";
            requestBaseAddress = "http://192.168.1.104:9000/";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(requestBaseAddress);
            var response = client
                                .SendBatchAsync
                                        (
                                            "api/asyncbatch"
                                            , new HttpRequestMessage(HttpMethod.Get, client.BaseAddress + "get1?id=11111")
                                            , new HttpRequestMessage(HttpMethod.Get, client.BaseAddress + "get2?id=2222")
                                        ).Result;
            var headers = JsonConvert.SerializeObject(response.Headers);
            Console.WriteLine($"{nameof(headers)}:{headers}");
            var ss = response.GetContentBodyStringsAsEnumerable();
            var i = 0;
            foreach (var s in ss)
            {
                DisplayAlert($"{client.BaseAddress.ToString()}:{i++}", s, "close");
            }

           
            
        }
    }
}
