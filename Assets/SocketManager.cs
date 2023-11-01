/*
using System;
using WebSocketSharp;

namespace Example
{
  public class SocketManager
  {
    public static void Main (string[] args)
    {
      using (var ws = new WebSocket ("localhost:8080")) {
        ws.OnMessage += (sender, e) =>
                          Console.WriteLine ("Laputa says: " + e.Data);

        ws.Connect ();
        ws.Send ("BALUS");
        Console.ReadKey (true);
      }
    }
  }
}
*/