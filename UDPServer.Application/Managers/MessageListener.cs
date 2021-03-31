using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UDPServer.Application.Helper;
using UDPServer.Persistence.Models;
using UDPServer.Persistence.Repositories;

namespace UDPServer.Application.Managers
{
    public class MessageListener : IDisposable
    {
        private const string PortPath = "Port";

        private readonly IPEndPoint _listenOn;
        private readonly UdpClient _client;
        private readonly QueueProcessor<Message> _messageQueue;
        private readonly MessageRepository _messageRepository;
        private readonly SenderRepository _senderRepository;

        private bool _listening = false;
        private bool _isDisposed = false;


        public MessageListener(MessageRepository messageRepository, SenderRepository senderRepository)
        {
            var portStr = ConfigurationManager.AppSettings.Get(PortPath);
            if (!int.TryParse(portStr, out var port))
                throw new Exception(nameof(port));

            _messageRepository = messageRepository;
            _senderRepository = senderRepository;

            _listenOn = new IPEndPoint(IPAddress.Any, port);
            _client = new UdpClient(_listenOn);
            _messageQueue = new QueueProcessor<Message>(ProcessMessage);

        }

        public void Start()
        {
            if (_isDisposed || _listening)
                return;

            _listening = true;
            _client.BeginReceive(OnReceive, null);
        }

        public void Stop()
        {
            _listening = false;
        }

        private void OnReceive(IAsyncResult asyncResult)
        {
            try
            {
                if (_isDisposed)
                    return;

                var remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);
                var buffer = _client.EndReceive(asyncResult, ref remoteEndPoint);
                string text = Encoding.ASCII.GetString(buffer);

                _messageQueue.OnQueueItemReceived(new Message
                {
                    Text = text,
                    CreatedAt = DateTime.UtcNow,
                    Sender = new Sender
                    {
                        IpAddress = $"{remoteEndPoint.Address}{remoteEndPoint.Port}"
                    }
                });

                if (_listening)
                    _client.BeginReceive(OnReceive, null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async Task ProcessMessage(Message message)
        {
            var sender = await _senderRepository.FindOneAsync(x => x.IpAddress == message.Sender.IpAddress, CancellationToken.None);
            if(sender == null)
            {
                sender = await _senderRepository.InsertOneAsync(message.Sender, CancellationToken.None);
            }
            message.SenderId = sender.Id;
            await _messageRepository.InsertOneAsync(message, CancellationToken.None);
        }

        #region IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_isDisposed)
                {
                    return;
                }

                _isDisposed = true;
                _client?.Dispose();
            }
        }

        #endregion IDisposable
    }
}
