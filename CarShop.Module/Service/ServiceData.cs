using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using CarShop.Module.ServiceReference;
using WcfCoreMtomEncoder;
using System.ServiceModel.Channels;

namespace CarShop.Module.Gus
{
    public class ServiceData
    {
        public static async Task<string> PobierzDane(string nip)
        {   
            // WSHttpBinding myBinding = new WSHttpBinding();
            // myBinding.Security.Mode = SecurityMode.Transport;
            // myBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.None;
            // myBinding.TextEncoding = Encoding.Unicode;              // WSMessageEncoding.Mtom;

            //  BasicHttpBinding b = new BasicHttpBinding();
            //  b.Security.Mode = BasicHttpSecurityMode.Transport;
            //  b.Security.Transport.ClientCredentialType = HttpClientCredentialType.None;
            //  b.TextEncoding = Encoding.Unicode;

            string result;
            var encoding = new MtomMessageEncoderBindingElement(new TextMessageEncodingBindingElement());
            var transport = new HttpsTransportBindingElement();

            var customBinding = new CustomBinding(encoding, transport);
            // bir.Endpoint.Binding = customBinding;

            EndpointAddress ea = new EndpointAddress("https://wyszukiwarkaregontest.stat.gov.pl/wsBIR/UslugaBIRzewnPubl.svc");
            UslugaBIRzewnPublClient cc = new UslugaBIRzewnPublClient(customBinding, ea);
            await cc.OpenAsync();
            
            ZalogujResponse r = await cc.ZalogujAsync("abcde12345abcde12345");            
            using (OperationContextScope scope = new OperationContextScope(cc.InnerChannel))
            {
                System.ServiceModel.Channels.HttpRequestMessageProperty requestMessage = new System.ServiceModel.Channels.HttpRequestMessageProperty();
                requestMessage.Headers.Add("sid", r.ZalogujResult);
                OperationContext.Current.OutgoingMessageProperties[System.ServiceModel.Channels.HttpRequestMessageProperty.Name] = requestMessage;
                // DANE SZUKAJ 1
                ParametryWyszukiwania objParametryGR1 = new ParametryWyszukiwania();
                objParametryGR1.Nip = nip;                
                DaneSzukajPodmiotyResponse dane = await cc.DaneSzukajPodmiotyAsync(objParametryGR1);
                // File.WriteAllText(Directory.GetCurrentDirectory() + "\\wynik.xml", dane.DaneSzukajPodmiotyResult);
                result = dane.DaneSzukajPodmiotyResult;
                WylogujResponse w = await cc.WylogujAsync(r.ZalogujResult);
                cc.Close();                
                return result;
            }
        }
    }
}
