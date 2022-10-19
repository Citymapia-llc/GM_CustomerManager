using System.Security.Cryptography;
using System.Text;

namespace GM.Store.Server.Helper
{
    public interface IEncodePasswordHelper
    {
        string EncodePassword(string pass, string salt);
    }
        public class EncodePasswordHelper:IEncodePasswordHelper
    {

        private readonly ILogger<EncodePasswordHelper> _log;
        public EncodePasswordHelper(ILogger<EncodePasswordHelper> log)
        {
            _log = log;
        }
        public string EncodePassword(string pass, string salt) //encrypt password    
        {
            try
            {
                byte[] bytes = Encoding.Unicode.GetBytes(pass);
                byte[] src = Encoding.Unicode.GetBytes(salt);
                byte[] dst = new byte[src.Length + bytes.Length];
                System.Buffer.BlockCopy(src, 0, dst, 0, src.Length);
                System.Buffer.BlockCopy(bytes, 0, dst, src.Length, bytes.Length);
                HashAlgorithm sha = SHA1.Create();
                byte[] inArray = sha.ComputeHash(dst);
                return EncodePasswordMd5(Convert.ToBase64String(inArray));
                //using (HashAlgorithm algorithm = HashAlgorithm.Create("SHA1"))
                //{
                //    if (algorithm != null)
                //    {
                //        byte[] inArray = algorithm.ComputeHash(dst);
                //        //return Convert.ToBase64String(inArray);    
                //        return EncodePasswordMd5(Convert.ToBase64String(inArray));
                //    }
                //}
                //return null;
            }
            catch (Exception ex)
            {
                _log.LogError(ex,ex.Message);
                throw ex;
            }
        }
        public string EncodePasswordMd5(string pass) //Encrypt using MD5    
        {
            try
            {
                Byte[] originalBytes;
                Byte[] encodedBytes;
                MD5 md5;
                //Instantiate MD5CryptoServiceProvider, get bytes for original password and compute hash (encoded password)    
                md5 = new MD5CryptoServiceProvider();
                originalBytes = ASCIIEncoding.Default.GetBytes(pass);
                encodedBytes = md5.ComputeHash(originalBytes);
                //Convert encoded bytes back to a 'readable' string    
                return BitConverter.ToString(encodedBytes);
            }
            catch (Exception ex)
            {
                _log.LogError(ex, ex.Message);
                throw ex;
            }
        }
    }
}
