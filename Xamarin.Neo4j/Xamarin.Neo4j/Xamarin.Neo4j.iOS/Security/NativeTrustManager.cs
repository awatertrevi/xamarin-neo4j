//
// NativeTrustManager.cs
//
// Trevi Awater
// 14-03-2022
//
// © Xamarin.Neo4j.iOS
//

using System;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using Neo4j.Driver;
using Security;

namespace Xamarin.Neo4j
{
    public class NativeTrustManager : TrustManager
    {
        public override bool ValidateServerCertificate(Uri uri, X509Certificate2 certificate, X509Chain chain,
            SslPolicyErrors sslPolicyErrors)
        {
            using (var cert = new SecCertificate(certificate))
            using (var policy = SecPolicy.CreateSslPolicy(true, uri.Host))
            using (var trustHandler = new SecTrust(cert, policy))
            {
                if (!trustHandler.Evaluate(out var error))
                    throw new Exception(error.ToString());

                return true;
            }
        }
    }
}
