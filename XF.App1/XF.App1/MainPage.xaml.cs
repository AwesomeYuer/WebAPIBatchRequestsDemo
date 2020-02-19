using Microshaoft;
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
            var batchrequest = client
                                    .CreateBatchHttpRequestMessage
                                            (
                                                "api/asyncbatch"
                                                , new HttpRequestMessage(HttpMethod.Get, client.BaseAddress + "get1?id=11111")
                                                , new HttpRequestMessage(HttpMethod.Get, client.BaseAddress + "post1?id=2222")
                                            );
            var response = client.SendAsync(batchrequest).Result;
            var ss = response.Content.ReadAsStringAsync().Result;
            DisplayAlert($"{client.BaseAddress.ToString()}", ss, "close");
        }
    }
}
