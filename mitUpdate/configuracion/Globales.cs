using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mitUpdate.configuracion
{
    public class Globales
    {
        public static cpIntegracionEMV.clsCpIntegracionEMV cpIntegreEMV = new cpIntegracionEMV.clsCpIntegracionEMV();
        public static string ip { get; set; }

        public static string ipPub { get; set; }

        public static string ipPrep { get; set; }

        public static string ipvip { get; set; }

        public static string ipPoints2 { get; set; }

        public static string ipfe { get; set; }

        public static string ipLoginInstalador { get; set; }

        public static string ipFirmaPanel { get; set; }

        public static string ipKeyWeb { get; set; }

        public static string VersionApp { get; set; }

        public static string VersionPcPay { get; set; }

        public const string KEY_RC4 = "KEY AEROPCPAY KEY";

        public static string DecryptString(string valor, string llave = "", bool hexadecimal = true)
        {
            return DecryptRC4(valor, llave, hexadecimal);
        }

        public static string DecryptRC4(string data, string llave, bool hexadecimal = true)
        {
            string text = string.Empty;

            if (hexadecimal) text = HexStrToStr(data);
            text = EnDeCrypt(llave, text);

            return text;
        }
        private static string EnDeCrypt(string password, string text)
        {
            const int N = 256;
            int[] sbox;

            sbox = new int[N];
            int[] key = new int[N];
            int n = password.Length;
            for (int a = 0; a < N; a++)
            {
                key[a] = (int)password[a % n];
                sbox[a] = a;
            }

            int b = 0;
            for (int a = 0; a < N; a++)
            {
                b = (b + sbox[a] + key[a]) % N;
                int tempSwap = sbox[a];
                sbox[a] = sbox[b];
                sbox[b] = tempSwap;
            }

            int i = 0, j = 0, k = 0;
            StringBuilder cipher = new StringBuilder();
            for (int a = 0; a < text.Length; a++)
            {
                i = (i + 1) % N;
                j = (j + sbox[i]) % N;
                int tempSwap = sbox[i];
                sbox[i] = sbox[j];
                sbox[j] = tempSwap;

                k = sbox[(sbox[i] + sbox[j]) % N];
                int cipherBy = ((int)text[a]) ^ k;
                cipher.Append(Convert.ToChar(cipherBy));
            }

            return cipher.ToString();

        }
        private static string HexStrToStr(string hexStr)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hexStr.Length; i += 2)
            {
                int n = Convert.ToInt32(hexStr.Substring(i, 2), 16);
                sb.Append(Convert.ToChar(n));
            }
            return sb.ToString();
        }

        public static string ArchivoServidor { get; set; }

        public static string ArchivoInstalador { get; set; }

        public static string ArchivoActualizar { get; set; }
        internal static string TipoUpdate(string actual, string nuevo)
        {
            string[] v1 = actual.Split('.');
            string[] v2 = nuevo.Split('.');
            if (Convert.ToInt32(v1[0]) < Convert.ToInt32(v2[0]))
            {
                return "msi";
            }
            else if (Convert.ToInt32(v1[1]) < Convert.ToInt32(v2[1]))
            {
                return "msi";
            }
            else if (Convert.ToInt32(v1[2]) < Convert.ToInt32(v2[2]))
            {
                return "msi";
            }
            else
            {
                return "no";
            }
        }

        public static int TA { get; set; }
    }
}
