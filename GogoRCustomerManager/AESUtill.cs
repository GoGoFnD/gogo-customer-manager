using K4os.Compression.LZ4.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GogoRCustomerManager
{
    public class AESUtill
    {
        private string KEY = "ED525C20416EE775";
        public string AESDecrypt128(string encrypt)
        {
            if(encrypt == null || encrypt == "uypE49YCzB/Me5+fBKkd7A==")
            {
                return "0";
            }
            // 암호화된 문자열을 바이트 배열로 변환
            byte[] encryptBytes = Convert.FromBase64String(encrypt);
            // AES 인스턴스 생성
            try
            {
                using (Aes aes = Aes.Create())
                {
                    aes.Key = Encoding.UTF8.GetBytes(KEY);
                    aes.IV = Encoding.UTF8.GetBytes(KEY); // IV는 일반적으로 랜덤하게 생성된 값을 사용해야 합니다.

                    aes.Mode = CipherMode.CBC;
                    aes.Padding = PaddingMode.PKCS7;

                    // 메모리 스트림과 CryptoStream 설정
                    using (MemoryStream memoryStream = new MemoryStream(encryptBytes))
                    using (ICryptoTransform decryptor = aes.CreateDecryptor())
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        // 복호화된 바이트 읽기
                        byte[] plainBytes = new byte[encryptBytes.Length];
                        int plainCount = cryptoStream.Read(plainBytes, 0, plainBytes.Length);

                        // 문자열로 변환 후 반환
                        return Encoding.UTF8.GetString(plainBytes, 0, plainCount);
                    }
                }
            }
            catch
            {

                return "오류";
            }
            
        }
    }
}
