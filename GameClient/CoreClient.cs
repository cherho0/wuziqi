using DotNetty.Codecs;
using DotNetty.Handlers.Logging;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using System;
using System.Net;
using System.Threading.Tasks;

namespace GameClient
{
    public class CoreClient
    {
        public IChannel Client;
        Bootstrap bootstrap;
        MultithreadEventLoopGroup group;

        public async Task ConnectAsync()
        {
            Client = await bootstrap.
                    ConnectAsync(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9999));
        }

        public void RunClientAsync()
        {
            // ExampleHelper.SetConsoleLogger();

            group = new MultithreadEventLoopGroup();

            string targetHost = null;


            bootstrap = new Bootstrap();
            bootstrap
                .Group(group)
                .Channel<TcpSocketChannel>()
                .Option(ChannelOption.TcpNodelay, true)
                .Handler(new ActionChannelInitializer<ISocketChannel>(channel =>
                {
                    IChannelPipeline pipeline = channel.Pipeline;


                    pipeline.AddLast(new LoggingHandler());
                    pipeline.AddLast("framing-enc", new LengthFieldPrepender(2));
                    pipeline.AddLast("framing-dec", new LengthFieldBasedFrameDecoder(ushort.MaxValue, 0, 2, 0, 2));

                    pipeline.AddLast("echo", new EchoClientHandler());
                }));
        }

        public async Task CloseAsync()
        {
            try
            {
                await Client.CloseAsync();

            }
            finally
            {
                await group.ShutdownGracefullyAsync(TimeSpan.FromMilliseconds(100), TimeSpan.FromSeconds(1));

            }
        }
    }
}
