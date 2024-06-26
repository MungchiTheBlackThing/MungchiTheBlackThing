#if !BESTHTTP_DISABLE_ALTERNATE_SSL && (!UNITY_WEBGL || UNITY_EDITOR)
#pragma warning disable
using System;
using System.IO;

namespace Best.HTTP.SecureProtocol.Org.BouncyCastle.Tls
{
    public interface DatagramSender
    {
        /// <exception cref="IOException"/>
        int GetSendLimit();

        /// <exception cref="IOException"/>
        void Send(byte[] buf, int off, int len);

#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER || UNITY_2021_2_OR_NEWER
        /// <exception cref="IOException"/>
        void Send(ReadOnlySpan<byte> buffer);
#endif
    }
}
#pragma warning restore
#endif
